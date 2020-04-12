using System;

namespace ContosoUniversity.Models {
    /// <summary>
    /// 视图模型，Dto
    /// </summary>
    public class StudentVM {
        public StudentVM (int iD, string lastName, string firstMidName, DateTime enrollmentDate) {
            this.ID = iD;
            this.LastName = lastName;
            this.FirstMidName = firstMidName;
            this.EnrollmentDate = enrollmentDate;
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}