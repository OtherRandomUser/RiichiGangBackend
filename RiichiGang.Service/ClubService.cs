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
    public class ClubService
    {
        private ApplicationDbContext _context;

        public ClubService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Club> GetAll()
            => _context.Clubs.AsQueryable()
                .Include(c => c.Owner)
                .Include(c => c.Members)
                .Include(c => c.Rulesets)
                .AsEnumerable();

        public Club GetById(int clubId)
            => _context.Clubs.AsQueryable()
                .Include(c => c.Owner)
                .Include(c => c.Members)
                .Include(c => c.Rulesets)
                .SingleOrDefault(c => c.Id == clubId);

        public async Task<Club> AddClubAsync(ClubInputModel inputModel, User owner)
        {
            if (inputModel is null)
                throw new ArgumentNullException("O input model não deve ser nulo");

            if (owner is null)
                throw new ArgumentNullException("O usuário dono do clube não deve ser nulo");

            if (inputModel.Name is null)
                throw new ArgumentNullException("O Nome do clube não deve ser nulo");

            if (_context.Clubs.AsQueryable().Any(c => c.Name.ToUpper() == inputModel.Name.ToUpper()))
                throw new ArgumentException($"Clube \"{inputModel.Name}\" já cadastrado");

            var club = new Club(inputModel.Name, owner, inputModel.Website, inputModel.Contact, inputModel.Localization);

            await _context.AddAsync(club);

            await _context.AddAsync(Ruleset.WRC(club));
            await _context.AddAsync(Ruleset.ML(club));

            await _context.SaveChangesAsync();

            return club;
        }

        public async Task<Club> UpdateClubAsync(Club club, ClubInputModel inputModel)
        {
            if (club is null)
                throw new ArgumentNullException("Clube não pode ser nulo");

            if (!string.IsNullOrWhiteSpace(inputModel.Name))
            {
                if (_context.Clubs.AsQueryable().Any(c => c.Name == inputModel.Name))
                    throw new ArgumentException($"Nome de clube \"{inputModel.Name}\" já cadastrado");

                club.SetName(inputModel.Name);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Website))
            {
                club.SetWebsite(inputModel.Website);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Contact))
            {
                club.SetContact(inputModel.Contact);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Localization))
            {
                club.SetLocalization(inputModel.Localization);
            }

            _context.Update(club);
            await _context.SaveChangesAsync();

            return club;
        }

        public async Task DeleteClubAsync(Club club)
        {
            _context.Remove(club);
            await _context.SaveChangesAsync();
        }

        public async Task AskInviteAsync(User user, Club club)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (club is null)
                throw new ArgumentNullException(nameof(club));

            if (club.Members.Any(m => m.UserId == user.Id))
                throw new ArgumentException($"{user.Username} já é um membro do clube {club.Name}");

            var membership = new Membership(user, club);
            await _context.AddAsync(membership);

            var notification = new Notification($"pediu para participar do seu clube \"{club.Name}\"", club.Owner, user, membership);
            await _context.AddAsync(notification);

            await _context.SaveChangesAsync();
        }

        public async Task QuitAsync(User user, Club club)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (club is null)
                throw new ArgumentNullException(nameof(club));

            var membership = club.Members.SingleOrDefault(m => m.UserId == user.Id);

            if (membership is null)
                throw new ArgumentException($"{user.Username} não é um membro do clube {club.Name}");

            _context.Remove(membership);
            await _context.SaveChangesAsync();
        }

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