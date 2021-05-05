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
                .AsEnumerable();

        public Club GetById(int clubId)
            => _context.Clubs.AsQueryable()
                .Include(c => c.Owner)
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

            var club = new Club(inputModel.Name, owner, inputModel.Website, inputModel.Contact);

            await _context.AddAsync(club);
            await _context.SaveChangesAsync();

            return club;
        }
    }
}