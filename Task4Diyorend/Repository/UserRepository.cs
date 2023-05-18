using Microsoft.EntityFrameworkCore;
using Task4Diyorend.Data;
using Task4Diyorend.Models;
using Task4Diyorend.Repository.IRepository;

namespace Task4Diyorend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) 
        {
            _context = context;
        }
        public bool Delete(AppUser appUser)
        {
            _context.Users.Remove(appUser);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser appUser)
        {
            _context.Users.Update(appUser);
            return Save();
        }
    }
}
