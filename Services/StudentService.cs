using PersonalAccount.Models;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services
{
    public class StudentService(IStudentRepo<StudentModel> repo) : IStudentService
    {
        public async Task<StudentModel?> GetByIdAsync(int id)
            => await repo.GetByIdAsync(id);

        public async Task UpdateByIdAsync(int id, StudentEditViewModel model)
        {
            var student = await repo.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Student with id {id} not found");

            student.FullName = model.FullName;
            student.GroupName = model.GroupName;
            student.PhotoUrl = string.IsNullOrWhiteSpace(model.PhotoUrl)
                ? null
                : new Uri(model.PhotoUrl);

            await repo.UpdateByIdAsync(id, student);
        }
    }
}