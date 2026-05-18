using PersonalAccount.Models.Students;

namespace PersonalAccount.Repositories
{
    public interface IStudentRepo<T> where T : StudentModel
    {
        public Task<T?> GetByEmailAsync(string email);
    }
}
