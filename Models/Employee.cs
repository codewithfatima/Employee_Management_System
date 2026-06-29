using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace EmployeeManagment.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        //foreignkey
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        //for phto upload
        public string? PhotoPath { get; set; }
    }
}
