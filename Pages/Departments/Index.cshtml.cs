using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeManagment.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Department> Departments { get; set; } = new();

        public async Task OnGetAsync()
        {
            Departments = await _context.Departments.ToListAsync();

 
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var departs = await _context.Departments.FindAsync(id);

            if (departs != null)
            {
                _context.Departments.Remove(departs);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Department deleted successfully!";
            }
            return RedirectToPage("Index");
        }
    }
}
