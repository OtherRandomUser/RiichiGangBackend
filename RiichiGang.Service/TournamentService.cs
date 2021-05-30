using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiichiGang.Data;
using RiichiGang.Domain;
using RiichiGang.Service.InputModel;

namespace RiichiGang.Service
{
    public class TournamentService
    {
        private ApplicationDbContext _context;

        public TournamentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tournament> GetAll()
            => _context.Tournaments.AsQueryable()
                .Include(t => t.Players)
                .AsEnumerable();

        public Tournament GetById(int id)
            => _context.Tournaments.AsQueryable()
                .Include(t => t.Ruleset)
                .Include(t => t.Players)
                .Include(t => t.Brackets)
                    .ThenInclude(b => b.Players)
                .Include(t => t.Brackets)
                    .ThenInclude(b => b.Series)
                        .ThenInclude(s => s.Games)
                .SingleOrDefault(t => t.Id == id);

        public async Task<Tournament> AddTournamentAsync(
            TournamentInputModel inputModel,
            Ruleset ruleset,
            Club club)
        {
            if (ruleset is null)
                throw new ArgumentNullException("O ruleset não pode ser nulo");

            if (club is null)
                throw new ArgumentNullException("O clube não pode ser nulo");

            if (ruleset.ClubId != club.Id)
                throw new ArgumentException("Ruleset e clube não batem");

            var tournament = new Tournament(
                inputModel.Name,
                inputModel.Description,
                ruleset,
                club,
                inputModel.StartDate)
            {
                AllowNonMembers = inputModel.AllowNonMembers,
                RequirePermission = inputModel.RequirePermission
            };

            await _context.AddAsync(tournament);
            var lastSeq = -1;

            foreach (var im in inputModel.Brackets.OrderBy(b => b.Sequence))
            {
                if (lastSeq == im.Sequence)
                    throw new ArgumentException("Existem sequências repetidas");

                lastSeq = im.Sequence;

                var bracket = new Bracket(
                    tournament,
                    im.Name,
                    im.Sequence,
                    Enum.Parse<WinCondition>(im.WinCondition),
                    im.NumberOfAdvancing,
                    im.NumberOfSeries,
                    im.GamesPerSeries);

                await _context.AddAsync(bracket);
            }

            await _context.SaveChangesAsync();
            return tournament;
        }
    }
}
