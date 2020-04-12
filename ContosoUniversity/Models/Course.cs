   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations.Schema;

   namespace ContosoUniversity.Models {
       /// <summary>
       /// 课程表
       /// </summary>
       public class Course {
           /// <summary>
           /// 主键，DatabaseGenerated 特性指定主键，而无需靠数据库生成
           /// </summary>
           /// <value></value>
           [DatabaseGenerated (DatabaseGeneratedOption.None)]
           public int CourseID { get; set; }
           public string Title { get; set; }
           public int Credits { get; set; }

           public ICollection<Enrollment> Enrollments { get; set; }
       }
   }