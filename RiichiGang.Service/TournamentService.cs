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

            if (!inputModel.Brackets.Any())
                throw new ArgumentException("Um torneio deve ter ao menos uma chave");

            var brackets = inputModel.Brackets.OrderBy(b => b.Sequence);
            if (brackets.Last().WinCondition != WinCondition.None.ToString())
                throw new ArgumentException("A última chave não deve conter uma condição de vitória");

            var lastSeq = -1;
            foreach (var im in brackets)
            {
                if (lastSeq == im.Sequence)
                    throw new ArgumentException("Existem sequências repetidas");

                lastSeq = im.Sequence;

                var winCondition = Enum.Parse<WinCondition>(im.WinCondition);

                if (winCondition == WinCondition.First || winCondition == WinCondition.FirstAndSecond)
                    im.NumberOfSeries = 1;

                var bracket = new Bracket(
                    tournament,
                    im.Name,
                    im.Sequence,
                    winCondition,
                    im.NumberOfAdvancing,
                    im.NumberOfSeries,
                    im.GamesPerSeries);

                await _context.AddAsync(bracket);
            }

            await _context.SaveChangesAsync();
            return tournament;
        }

        public async Task<Tournament> UpdateTournamentAsync(Tournament tournament, TournamentInputModel inputModel, Ruleset ruleset)
        {
            if (tournament is null)
                throw new ArgumentNullException("O clube não pode ser nulo");

            if (tournament.Status != TournamentStatus.Scheduled)
                throw new Exception("Um torneio já iniciado não pode ser atualizado");

            if (ruleset != null)
            {
                if (ruleset.ClubId != tournament.ClubId)
                    throw new ArgumentException("Ruleset e clube não batem");

                tournament.SetRuleset(ruleset);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Name))
                tournament.SetName(inputModel.Name);

            if (!string.IsNullOrWhiteSpace(inputModel.Description))
                tournament.SetDescription(inputModel.Description);

            tournament.StartDate = inputModel.StartDate;
            tournament.AllowNonMembers = inputModel.AllowNonMembers;
            tournament.RequirePermission = inputModel.RequirePermission;

            _context.Update(tournament);
            await _context.SaveChangesAsync();

            return tournament;
        }

        public async Task DeleteTournamentAsync(Tournament tournament)
        {
            _context.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task AskInviteAsync(Tournament tournament, User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            if (tournament.Status != TournamentStatus.Scheduled)
                throw new Exception("Não é possível participar de um torneio já iniciado");

            if (tournament.Players.Any(p => p.UserId == user.Id))
                throw new ArgumentException($"{user.Username} já pediu para participar no torneio \"{tournament.Name}\"");

            if (!tournament.RequirePermission || tournament.Club.OwnerId == user.Id)
            {
                var player = new TournamentPlayer(user, tournament, TournamentPlayerStatus.Confirmed);
                await _context.AddAsync(player);
            }
            else
            {
                var player = new TournamentPlayer(user, tournament, TournamentPlayerStatus.Pending);
                await _context.AddAsync(player);

                var notification = new Notification($"pediu para participar no torneio \"{tournament.Name}\"", tournament.Club.Owner, user, null, player);
                await _context.AddAsync(notification);
            }

            await _context.SaveChangesAsync();
        }

        public Task QuitAsync(Tournament tournament, User user)
        {
            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var player = tournament.Players.SingleOrDefault(p => p.UserId == user.Id);

            if (player is null)
                throw new ArgumentException($"{user.Username} não está inscrito no torneio \"{tournament.Id}\"");

            _context.Remove(player);
            return _context.SaveChangesAsync();
        }

        public async Task InitTournamentAsync(Tournament tournament)
        {
            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            if (tournament.Status != TournamentStatus.Scheduled)
                throw new ArgumentException("torneio já inicializado");

            if (!tournament.Players.Any())
                throw new ArgumentException("Torneio não conta com nenhum jogador");

            var players = tournament.Players.Count();
            var brackets = tournament.Brackets.OrderBy(b => b.Sequence);

            foreach (var bracket in brackets)
            {
                if (players % 4 > 0)
                    throw new ArgumentException($"número de jogadores insuficiente para a chave \"{bracket.Name}\"");

                switch (bracket.WinCondition)
                {
                case WinCondition.First:
                    players = players / 4;
                    break;

                case WinCondition.FirstAndSecond:
                    players = players / 2;
                    break;

                case WinCondition.TopX:
                    if (players < bracket.NumberOfAdvancing)
                        throw new ArgumentException($"número de jogadores insuficiente para a chave \"{bracket.Name}\"");

                    players = bracket.NumberOfAdvancing;
                    break;

                case WinCondition.None:
                    break;
                }
            }

            var firstBracket = brackets.First();
            var bracketPlayers = new List<BracketPlayer>();

            foreach (var player in tournament.Players)
            {
                var bracketPlayer = new BracketPlayer(player, firstBracket);
                bracketPlayers.Add(bracketPlayer);
            }

            await _context.AddRangeAsync(bracketPlayers);

            for (var i = 0; i < firstBracket.NumberOfSeries; i++)
            {
                var shuffled = bracketPlayers.OrderBy(_ => Guid.NewGuid()).AsEnumerable();

                while (shuffled.Any())
                {
                    var seriesPlayers = shuffled.Take(4).ToList();
                    shuffled = shuffled.Skip(4); // might be straight up wrong

                    var series = new Series(
                        firstBracket,
                        seriesPlayers[0],
                        seriesPlayers[1],
                        seriesPlayers[2],
                        seriesPlayers[3]);

                    await _context.AddAsync(series);

                    for (var j = 0; j < firstBracket.GamesPerSeries; j++)
                    {
                        var game = new Game(series);
                        await _context.AddAsync(game);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
