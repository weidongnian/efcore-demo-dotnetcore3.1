using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        /// <summary>
        /// 外键的主键，在数据模型中显式包含 FK 可使更新更简单和更高效
        /// </summary>
        /// <value></value>
        public int DepartmentID { get; set; }

        /// <summary>
        /// 外键实体
        /// </summary>
        /// <value></value>
        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}