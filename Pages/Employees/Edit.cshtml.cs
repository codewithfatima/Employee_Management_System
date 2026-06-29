using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        public SelectList DepartmentList { get; set; }

        [BindProperty]
        public Employee Employee { get; set; }

        public EditModel(AppDbContext context)
        {
            _context = context;
        }
    

        public async Task<IActionResult> OnGet(int id)
        {
            Employee = await _context.Employees.FindAsync(id);
            if(Employee == null) return NotFound();
            DepartmentList = new SelectList(_context.Departments, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var employeeFromDb = await _context.Employees.FindAsync(Employee.Id);

            if(employeeFromDb == null) return NotFound();

            employeeFromDb.FirstName = Employee.FirstName;
            employeeFromDb.LastName = Employee.LastName;    
            employeeFromDb.Email = Employee.Email;
            employeeFromDb.Phone = Employee.Phone;
            employeeFromDb.Salary = Employee.Salary;
            employeeFromDb.DepartmentId = Employee.DepartmentId;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }

    
    }
}
