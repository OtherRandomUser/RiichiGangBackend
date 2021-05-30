using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiichiGang.Data;
using RiichiGang.Domain;
using RiichiGang.Service.InputModel;

namespace RiichiGang.Service
{
    public class RulesetService
    {
        private ApplicationDbContext _context;

        public RulesetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ruleset GetById(int id)
            => _context.Rulesets.AsQueryable()
                .Include(r => r.Club)
                .SingleOrDefault(r => r.Id == id);

        public async Task<Ruleset> AddRulesetAsync(RulesetInputModel inputModel, Club club)
        {
            if (inputModel is null)
                throw new ArgumentNullException("O input model não deve ser nulo");

            if (club is null)
                throw new ArgumentNullException("O clube dono do ruleset não deve ser nulo");

            if (inputModel.Name is null)
                throw new ArgumentNullException("O Nome do clube não deve ser nulo");

            if (_context.Rulesets.AsQueryable()
                .Any(r =>
                    r.Name.ToUpper() == inputModel.Name.ToUpper()
                    && r.ClubId == club.Id))
                throw new ArgumentException($"Clube \"{inputModel.Name}\" já possui um rulset chamado \"{inputModel.Name}\"");;

            var ruleset = new Ruleset(club, inputModel.Name)
            {
                Mochiten = inputModel.Mochiten,
                Genten = inputModel.Genten,
                UmaFirst = inputModel.UmaFirst,
                UmaSecond = inputModel.UmaSecond,
                UmaThird = inputModel.UmaThird,
                UmaFourth = inputModel.UmaFourth,
                Oka = inputModel.Oka,
                Atozuke = inputModel.Atozuke,
                Kuitan = inputModel.Kuitan,
                Kuikae = Enum.Parse<Domain.Kuikae>(inputModel.Kuikae),
                UraDora = inputModel.UraDora,
                KanDora = inputModel.KanDora,
                KanUraDora = inputModel.KanUraDora,
                AkaDora = inputModel.AkaDora,
                AgariYame = inputModel.AgariYame,
                TenpaiYame = inputModel.TenpaiYame,
                Tobi = inputModel.Tobi,
            };

            await _context.AddAsync(ruleset);
            await _context.SaveChangesAsync();

            return ruleset;
        }

        public async Task<Ruleset> UpdateRulesetAsync(RulesetInputModel inputModel, Ruleset ruleset)
        {
            if (inputModel is null)
                throw new ArgumentNullException("O input model não deve ser nulo");

            if (ruleset is null)
                throw new ArgumentNullException("O ruleset não deve ser nulo");

            if (!string.IsNullOrWhiteSpace(inputModel.Name))
            {
                if (_context.Rulesets.AsQueryable()
                    .Any(r =>
                        r.Name == inputModel.Name
                        && r.ClubId == ruleset.ClubId))
                    throw new ArgumentException($"Clube \"{inputModel.Name}\" já possui um rulset chamado \"{inputModel.Name}\"");;

                ruleset.SetName(inputModel.Name);
            }

            ruleset.Mochiten = inputModel.Mochiten;
            ruleset.Genten = inputModel.Genten;
            ruleset.UmaFirst = inputModel.UmaFirst;
            ruleset.UmaSecond = inputModel.UmaSecond;
            ruleset.UmaThird = inputModel.UmaThird;
            ruleset.UmaFourth = inputModel.UmaFourth;
            ruleset.Oka = inputModel.Oka;
            ruleset.Atozuke = inputModel.Atozuke;
            ruleset.Kuitan = inputModel.Kuitan;

            if (!string.IsNullOrWhiteSpace(inputModel.Kuikae))
                ruleset.Kuikae = Enum.Parse<Domain.Kuikae>(inputModel.Kuikae);

            ruleset.UraDora = inputModel.UraDora;
            ruleset.KanDora = inputModel.KanDora;
            ruleset.KanUraDora = inputModel.KanUraDora;
            ruleset.AkaDora = inputModel.AkaDora;
            ruleset.AgariYame = inputModel.AgariYame;
            ruleset.TenpaiYame = inputModel.TenpaiYame;
            ruleset.Tobi = inputModel.Tobi;

            _context.Update(ruleset);
            await _context.SaveChangesAsync();

            return ruleset;
        }

        public async Task DeleteRulesetAsync(Ruleset ruleset)
        {
            _context.Remove(ruleset);
            await _context.SaveChangesAsync();
        }
    }
}