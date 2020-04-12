using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Students {
    public class CreateModel : PageModel {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel (ContosoUniversity.Data.SchoolContext context) {
            _context = context;
        }

        public IActionResult OnGet () {
            return Page ();
        }

        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public StudentVM StudentVM { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync () {
            if (!ModelState.IsValid) {
                return Page ();
            }

            // _context.Students.Add(Student);
            // await _context.SaveChangesAsync();

            //var emptyStudent = new Student ();

            //使用 PageModel 中 PageContext 属性的已发布的表单值
            //过多发布
            // if (await TryUpdateModelAsync<Student> (
            //         emptyStudent,
            //         "student", // Prefix for form value.
            //         //仅更新列出的属性
            //         s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)) {

            //     _context.Students.Add (emptyStudent);

            //     await _context.SaveChangesAsync ();

            //     return RedirectToPage ("./Index");
            // }

            var entry = _context.Add (new Student ());
            //SetValues 方法通过从另一个 PropertyValues 对象读取值来设置此对象的值。 SetValues 使用属性名称匹配。 
            //视图模型类型不需要与模型类型相关，它只需要具有匹配的属性。
            entry.CurrentValues.SetValues (StudentVM);

            await _context.SaveChangesAsync ();

            return RedirectToPage ("./Index");
        }
    }
}