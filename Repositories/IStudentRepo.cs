using PersonalAccount.Models;

namespace PersonalAccount.Repositories
{
    public interface IStudentRepo<T> where T : StudentProfileModel
    {
        public Task<T?> GetByEmailAsync(string email);
        public Task<T?> GetByIdAsync(int id);
    }
}
