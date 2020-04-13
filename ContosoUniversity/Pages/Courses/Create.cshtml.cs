using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Courses {
    public class CreateModel : DepartmentNamePageModel {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel (ContosoUniversity.Data.SchoolContext context) {
            _context = context;
        }

        public IActionResult OnGet () {
            PopulateDepartmentsDropDownList (_context);
            //ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");
            return Page ();
        }

        [BindProperty]
        public Course Course { get; set; }

        /***
         派生自 DepartmentNamePageModel。
         使用 TryUpdateModelAsync 防止过多发布。
         删除 ViewData["DepartmentID"]。 基类中的 DepartmentNameSL 是强类型模型，将用于 Razor 页面。 建议使用强类型而非弱类型。 有关详细信息，请参阅弱类型数据（ViewData 和 ViewBag）。
         */

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync () {
            var emptyCourse = new Course ();

            if (await TryUpdateModelAsync<Course> (emptyCourse,
                    "course",
                    s => s.CourseID, s => s.DepartmentID, s => s.Title, s => s.Credits)) {
                _context.Courses.Add (emptyCourse);
                await _context.SaveChangesAsync ();

                return RedirectToPage ("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList (_context, emptyCourse.DepartmentID);

            return Page ();
        }
    }
}