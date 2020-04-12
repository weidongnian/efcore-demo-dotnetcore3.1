using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students {
    public class DetailsModel : PageModel {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel (ContosoUniversity.Data.SchoolContext context) {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync (int? id) {
            if (id == null) {
                return NotFound ();
            }

            //Student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            //Include 和 ThenInclude 方法使上下文加载 Student.Enrollments 导航属性，
            //并在每个注册中加载 Enrollment.Course 导航属性。
            Student = await _context.Students
                .Include (s => s.Enrollments)
                .ThenInclude (e => e.Course)
                .AsNoTracking ()//AsNoTracking 方法将会提升性能
                .FirstOrDefaultAsync (m => m.ID == id);

            if (Student == null) {
                return NotFound ();
            }

            return Page ();
        }
    }
}