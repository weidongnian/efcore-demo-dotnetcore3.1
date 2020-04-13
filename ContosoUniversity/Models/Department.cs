using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 并发字段
        /// </summary>
        /// <value></value>
        [Timestamp]
        public byte[] RowVersion { get; set; }


        /// <summary>
        /// 最多一个管理员
        /// </summary>
        /// <value></value>
        public int? InstructorID { get; set; }

        /// <summary>
        /// 可有一个管理员
        /// </summary>
        /// <value></value>
        public Instructor Administrator { get; set; }

        /// <summary>
        /// 一个系有多个课程
        /// </summary>
        /// <value></value>
        public ICollection<Course> Courses { get; set; }
    }
}