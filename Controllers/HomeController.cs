using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Teacherlog.Data;
using Teacherlog.Models;
using Teacherlog.ViewModels;

namespace Teacherlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //всі індекси
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexClas()
        {
            var clases = _dbContext.Clases
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .Select(c => new ClasViewModel(c))
                .ToList();
            var courses = _dbContext.Courses
                .Include(c => c.Clases)
                .ToList();
            var teachers = _dbContext.Teachers
                .Include(c => c.Clases)
                .ToList();
            ViewBag.Courses = courses;
            ViewBag.Teachers = teachers;
            return View(clases);
        }
        public IActionResult IndexComment()
        {
            var comments = _dbContext.Comments
                .Include(c => c.Student)
                .Include(c => c.Teacher)
                .Select(c => new CommentViewModel(c))
                .ToList();
            var students = _dbContext.Students
                .Include(c => c.Comments)
                .ToList();
            var teachers = _dbContext.Teachers
                .Include(c => c.Comments)
                .ToList();
            ViewBag.Students = students;
            ViewBag.Teachers = teachers;
            return View(comments);
        }
        public IActionResult IndexCourse()
        {
            IEnumerable<CourseViewModel> courses = _dbContext.Courses
                .Select(c => new CourseViewModel(c))
                .ToList();

            return View(courses);
        }
        public IActionResult IndexGrade()
        {
            var grades = _dbContext.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Select(g => new GradeViewModel(g))
                .ToList();
            var students = _dbContext.Students
                .Include(s =>s.Grades)
                .ToList();
            var courses = _dbContext.Courses
                .Include(s => s.Grades)
                .ToList();
            ViewBag.Students = students;
            return View(grades);
        }

        public IActionResult IndexStudent()
        {
            var students = _dbContext.Students
                .Include(s => s.Course)
                .Select(s => new StudentViewModel(s))
                .ToList();
            var courses = _dbContext.Courses
                .Include(s => s.Students)
                .ToList();
            ViewBag.Courses = courses;
            return View(students);
        }
        public IActionResult IndexTeacher()
        {
            IEnumerable<TeacherViewModel> teachers = _dbContext.Teachers
                .Select(t => new TeacherViewModel(t))
                .ToList();

            return View(teachers);
        }


        // створення
        public IActionResult CreateClas()
        {
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateClas(Clas clas)
        {
            // Перевірка наявності класу з таким часом та кімнатою
            var existingClas = _dbContext.Clases.FirstOrDefault(
                t => t.Time == clas.Time && t.Room == clas.Room);

            if (existingClas != null)
            {
                ModelState.AddModelError(string.Empty, "Клас з таким часом та кімнатою вже існує");
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Teachers = _dbContext.Teachers.ToList();
                return View(clas);
            }
            if (ModelState.IsValid)
            {
                // Якщо перевірки успішні, створюємо новий клас і зберігаємо його
                _dbContext.Clases.Add(clas);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Якщо модель не є валідною, повертаємо її з повідомленнями про помилки
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            return View(clas);
        }
        public IActionResult CreateComment()
        {
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Comments.Add(comment);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            return View(comment);
        }
        public IActionResult CreateCourse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            var existingCourse = _dbContext.Courses.FirstOrDefault(p => p.CourseName == course.CourseName);

            if (existingCourse != null)
            {
                ModelState.AddModelError("CourseName", "Курс з такою назвою вже існує");
                return View(course);
            }

            if (ModelState.IsValid)
            {
                _dbContext.Courses.Add(course);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        public IActionResult CreateGrate()
        {
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateGrate(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Grades.Add(grade);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View(grade);
        }
        public IActionResult CreateStudent()
        {
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            var existingStudent = _dbContext.Students.FirstOrDefault(t => t.FirstName == student.FirstName && t.LastName == student.LastName);

            if (existingStudent != null)
            {
                ModelState.AddModelError(string.Empty, "Модель з такою назвою вже існує");
                return View(student);
            }
            if (ModelState.IsValid)
            {
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View(student);
        }
        public IActionResult CreateTeacher()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            var existingTeacher = _dbContext.Teachers.FirstOrDefault(t => t.FirstName == teacher.FirstName && t.LastName == teacher.LastName);

            if (existingTeacher != null)
            {
                ModelState.AddModelError(string.Empty, "Модель з такою назвою вже існує");
                return View(teacher);
            }

            if (ModelState.IsValid)
            {
                _dbContext.Teachers.Add(teacher);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(teacher);
        }
        //Видалення
        public IActionResult DeleteClas()
        {
            ViewBag.Clases = _dbContext.Clases.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult DeleteClass(ClasViewModel clasViewModel)
        {
            var clas = new Clas(clasViewModel);
            try
            {
                _dbContext.Clases.Remove(clas);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(clas);
            }
        }
        public IActionResult DeleteComment()
        {
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            ViewBag.Comments = _dbContext.Comments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult DeleteComment(CommentViewModel commentViewModel)
        {
            var comment = new Comment(commentViewModel);
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteCourse()
        {
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult DeleteCourse(CourseViewModel courseViewModel)
        {
            var courseId = courseViewModel.Id;

            // Проверка наличия связанных записей в таблице Students
            if (_dbContext.Students.Any(s => s.CourseId == courseId))
            {
                // Если есть связанные записи, добавьте сообщение об ошибке в ModelState
                ModelState.AddModelError(string.Empty, "Cannot delete course because it is associated with students.");
                ViewBag.Courses = _dbContext.Courses.ToList();
                return View(courseViewModel);
            }

            // Если нет связанных записей, продолжайте с удалением
            var course = new Course(courseViewModel);
            try
            {
                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Обработка других ошибок, если необходимо
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the course.");
                ViewBag.Courses = _dbContext.Courses.ToList();
                return View(courseViewModel);
            }
        }
        public IActionResult DeleteGrade()
        {
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Grates = _dbContext.Grades.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult DeleteGrade(GradeViewModel gradeViewModel)
        {
            var grate = new Grade(gradeViewModel);
            try
            {
                _dbContext.Grades.Remove(grate);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(grate);
            }
        }
        public IActionResult DeleteStudent()
        {
            ViewBag.Students = _dbContext.Students.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult DeleteStudent(StudentViewModel studentViewModel)
        {
            var student = new Student(studentViewModel);

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteTeacher()
        {
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult DeleteTeacher(TeacherViewModel teacherViewModel)
        {
            var teacher = new Teacher(teacherViewModel);

            _dbContext.Teachers.Remove(teacher);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // Редагування 
        public IActionResult EditClas(int id)
        {
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            ViewBag.Clases = new SelectList(_dbContext.Clases.ToList());

            // Отримайте клас з бази даних
            var clas = _dbContext.Clases.Find(id);

            // Перевірте, чи клас існує
            if (clas == null)
            {
                return NotFound();
            }

            return View(clas);
        }

        [HttpPost]
        public IActionResult EditClas(Clas clas)
        {
            // Перевірка наявності класу з таким часом та кімнатою
            var existingClas = _dbContext.Clases.FirstOrDefault(
                t => t.Time == clas.Time && t.Room == clas.Room && t.Id != clas.Id);

            if (existingClas != null)
            {
                ModelState.AddModelError(string.Empty, "Клас з таким часом та кімнатою вже існує");
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Teachers = _dbContext.Teachers.ToList();
                ViewBag.Clases = new SelectList(_dbContext.Clases.ToList());
                return View(clas);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(clas).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(IndexClas));
                }

                ViewBag.Clases = new SelectList(_dbContext.Clases.ToList());
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Teachers = _dbContext.Teachers.ToList();
                return View(clas);
            }
            catch (Exception ex)
            {
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Teachers = _dbContext.Teachers.ToList();
                ViewBag.Clases = new SelectList(_dbContext.Clases.ToList());
                return View(clas);
            }
        }

        public IActionResult EditComment(int id)
        {
            ViewBag.Teachers = _dbContext.Teachers.ToList();
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Comments = new SelectList(_dbContext.Comments.ToList());
            var comment = _dbContext.Comments.Find(id);
            return View(comment);
        }
        [HttpPost]
        public IActionResult EditComment(Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(comment).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(IndexComment));
                }
                ViewBag.Teachers = _dbContext.Teachers.ToList();
                ViewBag.Students = _dbContext.Students.ToList();
                ViewBag.Comments = new SelectList(_dbContext.Comments.ToList());
                return View(comment);
            }
            catch (Exception ex)
            {
                ViewBag.Teachers = _dbContext.Teachers.ToList();
                ViewBag.Students = _dbContext.Students.ToList();
                ViewBag.Comments = new SelectList(_dbContext.Comments.ToList());
                return View(comment);
            }
        }
        public IActionResult EditCourse(int id)
        {
            ViewBag.Courses = new SelectList(_dbContext.Courses.ToList());
            var course = _dbContext.Courses.Find(id);
            return View(course);
        }
        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            var existingCourse = _dbContext.Courses.FirstOrDefault(p => p.CourseName == course.CourseName);

            if (existingCourse != null)
            {
                ModelState.AddModelError("CourseName", "Курс з такою назвою вже існує");
                return View(course);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(course).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(IndexCourse));
                }
                ViewBag.Courses = new SelectList(_dbContext.Courses.ToList());
                return View(course);
            }
            catch (Exception ex)
            {
                ViewBag.Courses = new SelectList(_dbContext.Courses.ToList());
                return View(course);
            }
        }
        public IActionResult EditGrade(int id)
        {
            ViewBag.Students = _dbContext.Students.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Grades = new SelectList(_dbContext.Grades.ToList());
            var grade = _dbContext.Grades.Find(id);
            return View(grade);
        }
        [HttpPost]
        public IActionResult EditGrade(Grade grade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(grade).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(IndexGrade));
                }
                ViewBag.Students = _dbContext.Students.ToList();
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Grades = new SelectList(_dbContext.Grades.ToList());
                return View(grade);
            }
            catch (Exception ex)
            {
                ViewBag.Students = _dbContext.Students.ToList();
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Grades = new SelectList(_dbContext.Grades.ToList());
                return View(grade);
            }
        }
        public IActionResult EditStudent(int id)
        {
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewBag.Students = new SelectList(_dbContext.Students.ToList());
            var student = _dbContext.Students.Find(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            var existingStudent = _dbContext.Students.FirstOrDefault(t => t.FirstName == student.FirstName && t.LastName == student.LastName);

            if (existingStudent != null)
            {
                ModelState.AddModelError(string.Empty, "Модель з такою назвою вже існує");
                return View(student);
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(student).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(IndexStudent));
                }
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Students = new SelectList(_dbContext.Students.ToList());
                return View(student);
            }
            catch (Exception ex)
            {
                ViewBag.Courses = _dbContext.Courses.ToList();
                ViewBag.Students = new SelectList(_dbContext.Students.ToList());
                return View(student);
            }
        }
        public IActionResult EditTeacher(int id)
        {
            ViewBag.Teachers = new SelectList(_dbContext.Teachers.ToList());
            var teacher = _dbContext.Teachers.Find(id);
            return View(teacher);
        }
        [HttpPost]
        public IActionResult EditTeacher(Teacher teacher)
        {
            var existingTeacher = _dbContext.Teachers.FirstOrDefault(t => t.FirstName == teacher.FirstName && t.LastName == teacher.LastName);

            if (existingTeacher != null)
            {
                ModelState.AddModelError(string.Empty, "Модель з такою назвою вже існує");
                return View(teacher);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(teacher).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(IndexTeacher));
                }
                ViewBag.Teachers = new SelectList(_dbContext.Teachers.ToList());
                return View(teacher);
            }
            catch (Exception ex)
            {
                ViewBag.Teachers = new SelectList(_dbContext.Teachers.ToList());
                return View(teacher);
            }
        }
        [AcceptVerbs("Get", "Post")]
        public IActionResult IsTeacherNameAvailable(string FirstName, string LastName)
        {
            var isNameAvailable = !_dbContext.Teachers.Any(t => t.FirstName == FirstName && t.LastName == LastName);
            return Json(isNameAvailable);
        }
        [AcceptVerbs("Get", "Post")]
        public IActionResult IsStudentNameAvailable(string FirstName, string LastName)
        {
            var isNameAvailable = !_dbContext.Students.Any(t => t.FirstName == FirstName && t.LastName == LastName);
            return Json(isNameAvailable);
        }
        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNameCourseAvailable(string CourseName)
        {
            var isNameCourseAvailable = !_dbContext.Courses.Any(c => c.CourseName == CourseName);
            return Json(isNameCourseAvailable);
        }
        public IActionResult IsClasNameAvailable(DateTime Time, string Room)
        {
            var isNameAvailable = !_dbContext.Clases.Any(t => t.Time == Time && t.Room == Room);
            return Json(isNameAvailable);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
