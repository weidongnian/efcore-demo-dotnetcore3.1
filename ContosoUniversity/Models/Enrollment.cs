using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models {
    public enum Grade {
        A,
        B,
        C,
        D,
        F
    }

    public class Enrollment {
        public int EnrollmentID { get; set; }

        [DisplayFormat (NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        /// <summary>
        /// 注册记录面向一门课程，因此存在 CourseID FK 属性和 Course 导航属性
        /// </summary>
        /// <value></value>
        public int CourseID { get; set; }
        public Course Course { get; set; }

        /// <summary>
        /// 一份注册记录针对一名学生，因此存在 StudentID FK 属性和 Student 导航属性
        /// </summary>
        /// <value></value>
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}