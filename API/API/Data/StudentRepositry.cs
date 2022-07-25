using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StudentRepositry : IStudentRepositry
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public StudentRepositry(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            return await _context.Students
                          .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                          .ToListAsync();
        }

        public async Task<bool> AddStudent(StudentDto studentDto)
        {
            _context.Students.Add(_mapper.Map<StudentDto, Student>(studentDto));
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
