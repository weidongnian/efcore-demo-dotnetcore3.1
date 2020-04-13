在mac下使用vscode，基于dotnetcore3.1 webapp创建的项目,数据层使用efcore+sqlite

0 安装 dotnet sdk 3.1

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

19，完善注册功能 Pages/Students/Details.cshtml.cs
    
    //Student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
    //Include 和 ThenInclude 方法使上下文加载 Student.Enrollments 导航属性，
    //并在每个注册中加载 Enrollment.Course 导航属性。

    Student = await _context.Students
    .Include (s => s.Enrollments)
    .ThenInclude (e => e.Course)
    .AsNoTracking ()//AsNoTracking 方法将会提升性能
    .FirstOrDefaultAsync (m => m.ID == id);

20，修改Pages/Students/Details.cshtml页

        <dt class="col-sm-2">
            @Html.DisplayNameFor(model => model.Student.Enrollments)
        </dt>
        <dd class="col-sm-10">
            <table class="table">
                <tr>
                    <th>Course Title</th>
                    <th>Grade</th>
                </tr>
                @foreach (var item in Model.Student.Enrollments)
                {
                    <tr>
                        <td>
                            @Html.DisplayFor(modelItem => item.Course.Title)
                        </td>
                        <td>
                            @Html.DisplayFor(modelItem => item.Grade)
                        </td>
                    </tr>
                }
            </table>
        </dd>

21，更新“创建”页 并 dotnet run

    var emptyStudent = new Student();

    if (await TryUpdateModelAsync<Student>(
        emptyStudent,
        "student",   // Prefix for form value.
        s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
    {
        _context.Students.Add(emptyStudent);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

22，创建 Student 视图模型

    public class StudentVM
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

23，修改Student/Create.cshtml.cs

    [BindProperty]
    public StudentVM StudentVM { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entry = _context.Add(new Student());
        entry.CurrentValues.SetValues(StudentVM);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

24，更新Pages/Students/Edit.cshtml.cs页

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Student = await _context.Students.FindAsync(id);

        if (Student == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var studentToUpdate = await _context.Students.FindAsync(id);

        if (studentToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Student>(
            studentToUpdate,
            "student",
            s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        return Page();
    }

25，更新“删除”页 Pages/Students/Delete.cshtml.cs 

    using ContosoUniversity.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    namespace ContosoUniversity.Pages.Students
    {
        public class DeleteModel : PageModel
        {
            private readonly ContosoUniversity.Data.SchoolContext _context;

            public DeleteModel(ContosoUniversity.Data.SchoolContext context)
            {
                _context = context;
            }

            [BindProperty]
            public Student Student { get; set; }
            public string ErrorMessage { get; set; }

            public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Student = await _context.Students
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);

                if (Student == null)
                {
                    return NotFound();
                }

                if (saveChangesError.GetValueOrDefault())
                {
                    ErrorMessage = "Delete failed. Try again";
                }

                return Page();
            }

            public async Task<IActionResult> OnPostAsync(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var student = await _context.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    return RedirectToAction("./Delete",
                                        new { id, saveChangesError = true });
                }
            }
        }
    }

26，向“删除”Razor 页面添加错误消息 (Pages/Students/Delete.cshtml) 

<p class="text-danger">@Model.ErrorMessage</p>

27，添加排序 Pages/Students/Index.cshtml.cs

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Student> Students { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Student> studentsIQ = from s in _context.Students
                                            select s;

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            Students = await studentsIQ.AsNoTracking().ToListAsync();
        }

28，向“学生索引”页添加列标题超链接，使用以下代码替换 Students/Index.cshtml 中的代码。

    @page
    @model ContosoUniversity.Pages.Students.IndexModel

    @{
        ViewData["Title"] = "Students";
    }

    <h2>Students</h2>
    <p>
        <a asp-page="Create">Create New</a>
    </p>

    <table class="table">
        <thead>
            <tr>
                <th>
                    <a asp-page="./Index" asp-route-sortOrder="@Model.NameSort">
                        @Html.DisplayNameFor(model => model.Students[0].LastName)
                    </a>
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.Students[0].FirstMidName)
                </th>
                <th>
                    <a asp-page="./Index" asp-route-sortOrder="@Model.DateSort">
                        @Html.DisplayNameFor(model => model.Students[0].EnrollmentDate)
                    </a>
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            @foreach (var item in Model.Students)
            {
                <tr>
                    <td>
                        @Html.DisplayFor(modelItem => item.LastName)
                    </td>
                    <td>
                        @Html.DisplayFor(modelItem => item.FirstMidName)
                    </td>
                    <td>
                        @Html.DisplayFor(modelItem => item.EnrollmentDate)
                    </td>
                    <td>
                        <a asp-page="./Edit" asp-route-id="@item.ID">Edit</a> |
                        <a asp-page="./Details" asp-route-id="@item.ID">Details</a> |
                        <a asp-page="./Delete" asp-route-id="@item.ID">Delete</a>
                    </td>
                </tr>
            }
        </tbody>
    </table>

29，使用以下代码替换 Students/Index.cshtml.cs 中的代码，以添加筛选

    using ContosoUniversity.Data;
    using ContosoUniversity.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace ContosoUniversity.Pages.Students
    {
        public class IndexModel : PageModel
        {
            private readonly SchoolContext _context;

            public IndexModel(SchoolContext context)
            {
                _context = context;
            }

            public string NameSort { get; set; }
            public string DateSort { get; set; }
            public string CurrentFilter { get; set; }
            public string CurrentSort { get; set; }

            public IList<Student> Students { get; set; }

            public async Task OnGetAsync(string sortOrder, string searchString)
            {
                NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";

                CurrentFilter = searchString;
                
                IQueryable<Student> studentsIQ = from s in _context.Students
                                                select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                        || s.FirstMidName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                        break;
                    case "Date":
                        studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                        break;
                    case "date_desc":
                        studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                        break;
                    default:
                        studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                        break;
                }

                Students = await studentsIQ.AsNoTracking().ToListAsync();
            }
        }
    }

30，更新 Razor 页面，替换 Pages/Students/Index.cshtml 中的代码，以创建“搜索”按钮和各种 chrome

31，添加分页本部分将创建一个 PaginatedList 类来支持分页。 PaginatedList 类使用 Skip 和 Take 语句在服务器上筛选数据，而不是检索所有表格行

32，创建 PaginatedList 类

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    namespace ContosoUniversity
    {
        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);

                this.AddRange(items);
            }

            public bool HasPreviousPage
            {
                get
                {
                    return (PageIndex > 1);
                }
            }

            public bool HasNextPage
            {
                get
                {
                    return (PageIndex < TotalPages);
                }
            }

            public static async Task<PaginatedList<T>> CreateAsync(
                IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = await source.CountAsync();
                var items = await source.Skip(
                    (pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
        }
    }

32，删除数据库,操作之前先按dotnet-ef工具，dotnet tool install --global dotnet-ef 

    dotnet ef database drop --force

32，创建初始数据迁移

    dotnet ef migrations add IniticalCreate

    dotnet ef database update

    但在window的 visual studio 下请如下执行:

    Add-Migrations IniticalCreate

    Update-Database

33，新增更多实体，通过指定格式设置、验证和数据库映射规则来自定义数据模型

    ![Image 新增更多实体](https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/complex-data-model/_static/diagram.png?view=aspnetcore-3.1)

34，ef core 会针对不可为空的FK和多对多关系，启用级联删除,某一情况下要禁用级联删除

    modelBuilder.Entity<Department>()
    .HasOne(d => d.Administrator)
    .WithMany()
    .OnDelete(DeleteBehavior.Restrict)

35，多对多关系,一个学生会有多个注册课程信息

    ![Image 学生会与课程的关联](https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/complex-data-model/_static/student-course.png?view=aspnetcore-3.1)

    ![Image 实体关系图](https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/complex-data-model/_static/diagram.png?view=aspnetcore-3.1)

36，EF Core 可采用多种方式将相关数据加载到实体的导航属性中：预先加载、显式加载和延迟加载

    https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/read-related-data?view=aspnetcore-3.1&tabs=visual-studio

37，搭建“课程”页的基架

    dotnet aspnet-codegenerator razorpage -m Course -dc SchoolContext -udl -outDir Pages/Courses --referenceScriptLibraries

38，悲观并发和开放式并发(存储优先方案，sql server中表有一个数据类型rowversion)

        /// <summary>
        /// 并发字段
        /// </summary>
        /// <value></value>
        [Timestamp]
        public byte[] RowVersion { get; set; }

40，保存学院方法,并检查是否并发冲突

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var departmentToUpdate = await _context.Departments
                .Include(i => i.Administrator)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (departmentToUpdate == null)
            {
                return HandleDeletedDepartment();
            }

            _context.Entry(departmentToUpdate)
                .Property("RowVersion").OriginalValue = Department.RowVersion;

            if (await TryUpdateModelAsync<Department>(
                departmentToUpdate,
                "Department",
                s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save. " +
                            "The department was deleted by another user.");
                        return Page();
                    }

                    var dbValues = (Department)databaseEntry.ToObject();
                    await setDbErrorMessage(dbValues, clientValues, _context);

                    // Save the current RowVersion so next postback
                    // matches unless an new concurrency issue happens.
                    Department.RowVersion = (byte[])dbValues.RowVersion;
                    // Clear the model error for the next postback.
                    ModelState.Remove("Department.RowVersion");
                }
            }

            InstructorNameSL = new SelectList(_context.Instructors,
                "ID", "FullName", departmentToUpdate.InstructorID);

            return Page();
        }

        private async Task setDbErrorMessage(Department dbValues,
                Department clientValues, SchoolContext context)
        {

            if (dbValues.Name != clientValues.Name)
            {
                ModelState.AddModelError("Department.Name",
                    $"Current value: {dbValues.Name}");
            }
            if (dbValues.Budget != clientValues.Budget)
            {
                ModelState.AddModelError("Department.Budget",
                    $"Current value: {dbValues.Budget:c}");
            }
            if (dbValues.StartDate != clientValues.StartDate)
            {
                ModelState.AddModelError("Department.StartDate",
                    $"Current value: {dbValues.StartDate:d}");
            }
            if (dbValues.InstructorID != clientValues.InstructorID)
            {
                Instructor dbInstructor = await _context.Instructors
                   .FindAsync(dbValues.InstructorID);
                ModelState.AddModelError("Department.InstructorID",
                    $"Current value: {dbInstructor?.FullName}");
            }

            ModelState.AddModelError(string.Empty,
                "The record you attempted to edit "
              + "was modified by another user after you. The "
              + "edit operation was canceled and the current values in the database "
              + "have been displayed. If you still want to edit this record, click "
              + "the Save button again.");
        }