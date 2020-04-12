在mac下使用vscode，基于dotnetcore3.1 webapp创建的项目,数据层使用efcore+sqlite

0 安装 dotnet sdk 3.0

1，安装efcore cli

    dotnet tool intall --global dotnet-ef

2，创建目录和解决方案,创建 webapp

    mkdir EFCoreStudy

    dotnet new sln -n EFCore

    dotnet new webapp -o ContosoUniversity --no-https

3，将项目加到解决方案里

    dotnet sln add ContosoUniversity/ContosoUniversity.csproj

4，修改布局面 _layout.cshtml

    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>@ViewData["Title"] - Contoso University</title>
        <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
        <link rel="stylesheet" href="~/css/site.css" />
    </head>
    <body>
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="container">
                    <a class="navbar-brand" asp-area="" asp-page="/Index">Contoso University</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-page="/About">About</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-page="/Students/Index">Students</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-page="/Courses/Index">Courses</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-page="/Instructors/Index">Instructors</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-page="/Departments/Index">Departments</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        <div class="container">
            <main role="main" class="pb-3">
                @RenderBody()
            </main>
        </div>

        <footer class="border-top footer text-muted">
            <div class="container">
                &copy; 2019 - Contoso University - <a asp-area="" asp-page="/Privacy">Privacy</a>
            </div>
        </footer>

        <script src="~/lib/jquery/dist/jquery.js"></script>
        <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.js"></script>
        <script src="~/js/site.js" asp-append-version="true"></script>

        @RenderSection("Scripts", required: false)
    </body>
    </html>

5，修改主页 index.cshtml

    @page
    @model IndexModel
    @{
        ViewData["Title"] = "Home page";
    }

    <div class="row mb-auto">
        <div class="col-md-4">
            <div class="row no-gutters border mb-4">
                <div class="col p-4 mb-4 ">
                    <p class="card-text">
                        Contoso University is a sample application that
                        demonstrates how to use Entity Framework Core in an
                        ASP.NET Core Razor Pages web app.
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="row no-gutters border mb-4">
                <div class="col p-4 d-flex flex-column position-static">
                    <p class="card-text mb-auto">
                        You can build the application by following the steps in a series of tutorials.
                    </p>
                    <p>
                        <a href="https://docs.microsoft.com/aspnet/core/data/ef-rp/intro" class="stretched-link">See the tutorial</a>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="row no-gutters border mb-4">
                <div class="col p-4 d-flex flex-column">
                    <p class="card-text mb-auto">
                        You can download the completed project from GitHub.
                    </p>
                    <p>
                        <a href="https://github.com/dotnet/AspNetCore.Docs/tree/master/aspnetcore/data/ef-rp/intro/samples" class="stretched-link">See project source code</a>
                    </p>
                </div>
            </div>
        </div>
    </div>

6，运行

    dotnet run

7，进入项目，创建Modles目录，编写实体类

    cd ContosoUniversity

    mkdir Models

    //Student

    using System;
    using System.Collections.Generic;

    namespace ContosoUniversity.Models
    {
        public class Student
        {
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

    namespace ContosoUniversity.Models
    {
        public enum Grade
        {
            A, B, C, D, F
        }

        /// <summary>
        /// 报名表
        /// </summary>
        public class Enrollment
        {
            public int EnrollmentID { get; set; }
            public int CourseID { get; set; }
            public int StudentID { get; set; }
            public Grade? Grade { get; set; }

            public Course Course { get; set; }
            public Student Student { get; set; }
        }
    }

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ContosoUniversity.Models
    {
        /// <summary>
        /// 课程表
        /// </summary>
        public class Course
        {
            /// <summary>
            /// 主键，DatabaseGenerated 特性指定主键，而无需靠数据库生成
            /// </summary>
            /// <value></value>
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int CourseID { get; set; }
            public string Title { get; set; }
            public int Credits { get; set; }

            public ICollection<Enrollment> Enrollments { get; set; }
        }
    }

8，添加 nuget 包

    dotnet add package Microsoft.EntityFrameworkCore.SQLite
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    dotnet add package Microsoft.Extensions.Logging.Debug

9，运行以下命令安装 aspnet-codegenerator 基架工具

    dotnet tool install --global dotnet-aspnet-codegenerator

10.1 在 Windows 上 运行以下命令，搭建“学生”页的基架。

    dotnet aspnet-codegenerator razorpage -m Student -dc ContosoUniversity.Data.SchoolContext -udl -outDir Pages/Students --referenceScriptLibraries

10.2 在 macOS 或 Linux 上 运行以下命令，搭建“学生”页的基架。

    dotnet aspnet-codegenerator razorpage -m Student -dc ContosoUniversity.Data.SchoolContext -udl -outDir Pages/Students --referenceScriptLibraries

11，如果对上述步骤有疑问，请生成项目并重试基架搭建步骤。
基架流程：
在“Pages/Students”文件夹中创建 Razor 页面 ：
Create.cshtml 和 Create.cshtml.cs
Delete.cshtml 和 Delete.cshtml.cs
Details.cshtml 和 Details.cshtml.cs
Edit.cshtml 和 Edit.cshtml.cs
Index.cshtml 和 Index.cshtml.cs
创建 Data/SchoolContext.cs 。
将上下文添加到 Startup.cs 中的依赖项注入 。
将数据库连接字符串添加到 appsettings.json 。

12，修改数据库链接 Data Source=CU.db

13，完善SchoolContext代码
    using Microsoft.EntityFrameworkCore;
    using ContosoUniversity.Models;

    namespace ContosoUniversity.Data
    {
        public class SchoolContext : DbContext
        {
            public SchoolContext (DbContextOptions<SchoolContext> options)
                : base(options)
            {
            }

            /// <summary>
            /// 实体集通常对应数据库表,由于实体集包含多个实体，因此 DBSet 属性应为复数名称
            /// </summary>
            /// <value></value>
            public DbSet<Student> Students { get; set; }
            public DbSet<Enrollment> Enrollments { get; set; }
            public DbSet<Course> Courses { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //建立实体与表的对应
                modelBuilder.Entity<Course>().ToTable("Course");
                modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
                modelBuilder.Entity<Student>().ToTable("Student");
            }
        }
    }

14，修改Startup.cs文件，确保使用sqllite数据库

    services.AddDbContext<SchoolContext> (options =>
    options.UseSqlite (Configuration.GetConnectionString ("SchoolContext")));

15，如果没有数据库，请更新 Program.cs 以创建数据库
    如果有上下文的数据库，则 EnsureCreated 方法不执行任何操作。 如果没有数据库，则它将创建数据库和架构。 EnsureCreated 启用以下工作流来处理数据模型更改：
    删除数据库。 任何现有数据丢失。
    更改数据模型。 例如，添加 EmailAddress 字段。
    运行应用。
    EnsureCreated 创建具有新架构的数据库

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SchoolContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

16，运行并测试数据新增修改

    dotnet run

17，设定数据库种子，创建 Data/DbInitializer.cs
    停止运行，删除数据库，修改Program.cs

    // context.Database.EnsureCreated();
    DbInitializer.Initialize(context);

18，异步代码
    Web 服务器的可用线程是有限的，而在高负载情况下的可能所有线程都被占用。 当发生这种情况的时候，服务器就无法处理新请求，直到线程被释放。 使用同步代码时，可能会出现多个线程被占用但不能执行任何操作的情况，因为它们正在等待 I/O 完成。 使用异步代码时，当进程正在等待 I/O 完成，服务器可以将其线程释放用于处理其他请求。 因此，使用异步代码可以更有效地利用服务器资源，并且服务器可以无延迟地处理更多流量。

    but EF Core 上下文并非线程安全：请勿尝试并行执行多个操作!!

    public async Task OnGetAsync()
    {
        Students = await _context.Students.ToListAsync();
    }
