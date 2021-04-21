using RiichiGang.Data;

namespace RiichiGang.Service
{
    public class ClubService
    {
        private ApplicationDbContext _context;

        public ClubService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}