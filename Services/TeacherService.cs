using Microsoft.EntityFrameworkCore;
using TeacherManagementApi.Data;
using TeacherManagementApi.DTOs;
using TeacherManagementApi.Models;

namespace TeacherManagementApi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            return await _context.Teachers
                .Select(t => new TeacherDto
                {
                    Id = t.Id,
                    FullName = $"{t.FirstName} {t.LastName}",
                    Email = t.Email,
                    Subject = t.Subject
                })
                .ToListAsync();
        }

        public async Task<TeacherDto?> GetByIdAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return null;

            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = $"{teacher.FirstName} {teacher.LastName}",
                Email = teacher.Email,
                Subject = teacher.Subject
            };
        }

        public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto)
        {
            var teacher = new Teacher
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Subject = dto.Subject
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = $"{teacher.FirstName} {teacher.LastName}",
                Email = teacher.Email,
                Subject = teacher.Subject
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTeacherDto dto)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.Email = dto.Email;
            teacher.Subject = dto.Subject;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}