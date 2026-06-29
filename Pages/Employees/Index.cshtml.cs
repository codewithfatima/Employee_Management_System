using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;  //dependecy injection
        public Employee Employee { get; set; } = new();

        //contsrutor
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        //list of employees
        public List<Employee> Employees { get; set; } = new();
        // Instead of List<Department>, use the full namespace path:
        public List<EmployeeManagement.Models.Department> Departments { get; set; } = new();
        public List<Department> AllDepartments { get; set; } = new();

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);

            if(emp != null)
                {
                    _context.Employees.Remove(emp);
                   await  _context.SaveChangesAsync();

                }

            return RedirectToPage("Index");

        }


        public async Task OnGetAsync(string searchTerm, int? searchDept)
        {
            AllDepartments = await _context.Departments.ToListAsync();

            var query = _context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(e=>e.FirstName.Contains(searchTerm) || 
                                       e.Email.Contains(searchTerm) || 
                                       e.Phone.Contains(searchTerm));
            }

            if (searchDept.HasValue)
            {
                query = query.Where(e=>e.DepartmentId == searchDept.Value);
            }
            Employees = await query.ToListAsync();
        }




    }
}
