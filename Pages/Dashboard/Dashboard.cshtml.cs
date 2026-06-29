using EmployeeManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Pages.Dashboard
{
    public class Dashboard : PageModel
    {
        private readonly AppDbContext _context;

        public Dashboard(AppDbContext context)
        {
            _context = context;
        }

        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public decimal HighestSalary { get; set; }
        public double AverageSalary { get; set; }
        public async Task OnGet()
        {
            TotalEmployees = await _context.Employees.CountAsync();
            TotalDepartments = await _context.Employees.CountAsync();

            if(await _context.Employees.AnyAsync())
            {
                HighestSalary = await _context.Employees.MaxAsync(e=> e.Salary);
                AverageSalary = await _context.Employees.AverageAsync(e=>(double)e.Salary);
            }
        }

        public async Task<IQueryable> OnGetGroupBy(string DepartmentId)
        {
            var employees  = await _context.Employees.GroupBy(e=>e.DepartmentId);

        }
    }
}
