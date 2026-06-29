using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManagment.Models;
using EmployeeManagement.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EmployeeManagment.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;  //dependecy injection

        //contsrutor
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        // 1. Declare the property
        public SelectList DepartmentList { get; set; } 

        public void OnGet()
        {
            // Fetch departments and put them into ViewBag
            DepartmentList = new SelectList(_context.Departments, "Id", "Name");
        }

        [BindProperty]
        public Employee Employee { get; set; }
        public async Task <IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, re-populate the dropdown before returning to page
                DepartmentList = new SelectList(_context.Departments, "Id", "Name");
                return Page();
            }

            _context.Employees.Add(Employee);
            Console.WriteLine(Employee.Phone);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
