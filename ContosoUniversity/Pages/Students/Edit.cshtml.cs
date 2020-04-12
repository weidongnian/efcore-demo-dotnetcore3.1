using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students {
    public class EditModel : PageModel {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel (ContosoUniversity.Data.SchoolContext context) {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync (int? id) {
            if (id == null) {
                return NotFound ();
            }

            //Student = await _context.Students.FirstOrDefaultAsync (m => m.ID == id);
            //已将 FirstOrDefaultAsync 替换为 FindAsync。 不需要包含相关数据时，FindAsync 效率更高。
            Student = await _context.Students.FindAsync(id);

            if (Student == null) {
                return NotFound ();
            }

            return Page ();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync (int id) {
            var studentToUpdate = await _context.Students.FindAsync (id);

            if (studentToUpdate == null) {
                return NotFound ();
            }

            if (await TryUpdateModelAsync<Student> (
                    studentToUpdate,
                    "student",
                    s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)) {
                await _context.SaveChangesAsync ();
                return RedirectToPage ("./Index");
            }

            return RedirectToPage ("./Index");
        }

        private bool StudentExists (int id) {
            return _context.Students.Any (e => e.ID == id);
        }
    }
}