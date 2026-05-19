using Microsoft.AspNetCore.Identity;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services
{
    public class PasswordService(
        IStudentRepo<StudentAuthModel> repo,
        IPasswordHasher<StudentAuthModel> hasher) : IPasswordService
    {
        public async Task<bool> ValidatePasswordAsync(int id, string password)
        {
            var student = await repo.GetByIdAsync(id);
            if (student == null) return false;

            var result = hasher.VerifyHashedPassword(student, student.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        }

        public async Task UpdatePasswordAsync(int id, string newPassword)
        {
            var student = await repo.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Student with id {id} not found");

            var newHash = hasher.HashPassword(student, newPassword);
            await repo.UpdatePasswordHashAsync(id, newHash);
        }
    }
}