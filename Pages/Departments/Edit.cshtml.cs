using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Intrinsics.Arm;

namespace EmployeeManagment.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await _context.Departments.FindAsync(id);
            if (Department == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var depart = await _context.Departments.FindAsync(Department.Id);
            if (depart == null) return NotFound();
           
            depart.Name = Department.Name;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Department Edit successfully!";

            return RedirectToPage("/Departments/Index");
        }

       
    }
}

