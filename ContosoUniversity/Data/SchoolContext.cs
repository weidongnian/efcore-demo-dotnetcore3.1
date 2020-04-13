using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data {
    public class SchoolContext : DbContext {
        public SchoolContext (DbContextOptions<SchoolContext> options) : base (options) { }

        /// <summary>
        /// 实体集通常对应数据库表,由于实体集包含多个实体，因此 DBSet 属性应为复数名称
        /// </summary>
        /// <value></value>
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Departments
        /// </summary>
        /// <value></value>
        public DbSet<Department> Departments { get; set; }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            //建立实体与表的对应
            modelBuilder.Entity<Course> ().ToTable ("Course");
            modelBuilder.Entity<Enrollment> ().ToTable ("Enrollment");
            modelBuilder.Entity<Student> ().ToTable ("Student");

            //禁用学院和部门的级联删除，不然导师删除后可以会将学院也删除了
            modelBuilder.Entity<Department> ()/*.Property<byte[]> ("RowVersion")
                .IsRowVersion ()*/.ToTable ("Department")
                .HasOne (d => d.Administrator)
                .WithMany ()
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor> ().ToTable ("Instructor");
            modelBuilder.Entity<OfficeAssignment> ().ToTable ("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment> ().ToTable ("CourseAssignment");

            //组合组件
            modelBuilder.Entity<CourseAssignment> ()
                .HasKey (c => new { c.CourseID, c.InstructorID });
        }
    }
}