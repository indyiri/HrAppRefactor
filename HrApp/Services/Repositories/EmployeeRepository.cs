using HrApp.Data;
using HrApp.Models;
using HrApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrApp.Services.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Employee employee)
        {
            if (!await EmployeeExists(employee.EmployeeId))
                throw new InvalidOperationException($"Employee with id {employee.EmployeeId} does not exist.");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetById(int? id)
        {
            if (id == null)
                return null;

            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task Update(Employee employee)
        {
            if (!await EmployeeExists(employee.EmployeeId))
                throw new InvalidOperationException($"Employee with id {employee.EmployeeId} does not exist.");

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }
    }
}
