using Task4Diyorend.Models;

namespace Task4Diyorend.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetByIdAsync(string id);
        bool Update(AppUser appUser);
        bool Delete(AppUser appUser);
        bool Save();
    }
}
