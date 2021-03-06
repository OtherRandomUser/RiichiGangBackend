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
    public class UserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetById(int id)
            => _context.Users.AsQueryable()
                .Include(u => u.OwnedClubs)
                    .ThenInclude(c => c.Members)
                .Include(u => u.Memberships)
                    .ThenInclude(m => m.Club)
                        .ThenInclude(c => c.Members)
                .Include(u => u.Tournaments)
                    .ThenInclude(t => t.Tournament)
                .Include(u => u.Notifications)
                    .ThenInclude(n => n.Creator)
                .SingleOrDefault(u => u.Id == id);

        public User GetByUsername(string username)
            => _context.Users.AsQueryable()
                .Include(u => u.OwnedClubs)
                    .ThenInclude(c => c.Members)
                .Include(u => u.Memberships)
                    .ThenInclude(m => m.Club)
                        .ThenInclude(c => c.Members)
                .Include(u => u.Tournaments)
                    .ThenInclude(t => t.Tournament)
                .Include(u => u.Notifications)
                    .ThenInclude(n => n.Creator)
                .SingleOrDefault(u => u.Username == username);

        public User GetByEmail(string email)
            => _context.Users.AsQueryable()
                .Include(u => u.OwnedClubs)
                    .ThenInclude(c => c.Members)
                .Include(u => u.Memberships)
                    .ThenInclude(m => m.Club)
                        .ThenInclude(c => c.Members)
                .Include(u => u.Tournaments)
                    .ThenInclude(t => t.Tournament)
                .Include(u => u.Notifications)
                    .ThenInclude(n => n.Creator)
                .SingleOrDefault(u => u.Email == email);

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
            => _context.Users.AsQueryable()
                .Include(u => u.OwnedClubs)
                    .ThenInclude(c => c.Members)
                .Include(u => u.Memberships)
                    .ThenInclude(m => m.Club)
                        .ThenInclude(c => c.Members)
                .Include(u => u.Tournaments)
                    .ThenInclude(t => t.Tournament)
                .Include(u => u.Notifications)
                    .ThenInclude(n => n.Creator)
                .Where(predicate)
                .AsEnumerable();

        public async Task<User> AddUserAsync(UserInputModel inputModel)
        {
            if (inputModel.Password != inputModel.PasswordConfirmation)
                throw new ArgumentException("As senhas n??o batem");

            if (_context.Users.AsQueryable().Any(u => u.Email == inputModel.Email))
                throw new ArgumentException($"Email \"{inputModel.Email}\" j?? cadastrado");

            if (_context.Users.AsQueryable().Any(u => u.Username == inputModel.Username))
                throw new ArgumentException($"Nome de usu??rio \"{inputModel.Username}\" j?? cadastrado");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(inputModel.Password);
            var user = new User(
                inputModel.Username,
                inputModel.Email,
                passwordHash);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(User user, UserInputModel inputModel)
        {
            if (user is null)
                throw new ArgumentNullException("Usu??rio n??o pode ser nulo");

            if (!string.IsNullOrWhiteSpace(inputModel.Username))
            {
                if (_context.Users.AsQueryable().Any(u => u.Username == inputModel.Username))
                    throw new ArgumentException($"Nome de usu??rio \"{inputModel.Username}\" j?? cadastrado");

                user.SetUsername(inputModel.Username);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Email))
            {
                if (_context.Users.AsQueryable().Any(u => u.Email == inputModel.Email))
                    throw new ArgumentException($"Email \"{inputModel.Email}\" j?? cadastrado");

                user.SetEmail(inputModel.Email);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Email))
            {
                if (_context.Users.AsQueryable().Any(u => u.Email == inputModel.Email))
                    throw new ArgumentException($"Email \"{inputModel.Email}\" j?? cadastrado");

                user.SetEmail(inputModel.Email);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Password))
            {
                if (inputModel.Password != inputModel.PasswordConfirmation)
                    throw new ArgumentException("As senhas n??o batem");

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(inputModel.Password);
                user.SetPasswordHash(passwordHash);
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public Task ConfirmMembershipAsync(User user, int membershipId)
        {
            var membership = _context.Memberships
                .FirstOrDefault(m => m.UserId == user.Id && m.Id == membershipId);

            if (membership is null)
                throw new ArgumentNullException("Afilia????o n??o encontrada");

            membership.Status = MembershipStatus.Confirmed;
            _context.Update(membership);
            return _context.SaveChangesAsync();
        }

        public Task ConfirmTournamentEntryAsync(User user, int tournamentPlayerId)
        {
            var player = _context.TournamentPlayers
                .FirstOrDefault(p => p.UserId == user.Id && p.Id == tournamentPlayerId);

            if (player is null)
                throw new ArgumentNullException("Registro n??o encontrado");

            player.Status = TournamentPlayerStatus.Confirmed;
            _context.Update(player);
            return _context.SaveChangesAsync();
        }

        public Task DenyMembershipAsync(User user, int membershipId)
        {
            var membership = _context.Memberships
                .FirstOrDefault(m => m.UserId == user.Id && m.Id == membershipId);

            if (membership is null)
                throw new ArgumentNullException("Afilia????o n??o encontrada");

            membership.Status = MembershipStatus.Denied;
            _context.Update(membership);
            return _context.SaveChangesAsync();
        }

        public Task DenyTournamententryAsync(User user, int tournamentPlayerId)
        {
            var player = _context.TournamentPlayers
                .FirstOrDefault(p => p.UserId == user.Id && p.Id == tournamentPlayerId);

            if (player is null)
                throw new ArgumentNullException("Afilia????o n??o encontrada");

            player.Status = TournamentPlayerStatus.Denied;
            _context.Update(player);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> DeleteNotificationAsync(Notification notification)
        {
            if (notification is null)
                throw new ArgumentNullException("Notifica????o n??o pode ser nula");

            var userId = notification.UserId;
            _context.Remove(notification);
            await _context.SaveChangesAsync();

            var user = GetById(userId);
            return user.Notifications;
        }
    }
}