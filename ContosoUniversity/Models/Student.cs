   using System.Collections.Generic;
   using System;

   namespace ContosoUniversity.Models {
       public class Student {
           public int ID { get; set; }
           public string LastName { get; set; }
           public string FirstMidName { get; set; }
           public DateTime EnrollmentDate { get; set; }

           /// <summary>
           /// 导航属性，外键表，一对多
           /// </summary>
           /// <value></value>
           public ICollection<Enrollment> Enrollments { get; set; }
       }
   }