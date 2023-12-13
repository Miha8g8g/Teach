using Teacherlog.ViewModels;

namespace Teacherlog.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } // Ім'я студента
        public string LastName { get; set; } // Прізвище студента
        public string Email { get; set; } // Електронна пошта студента
        // Зовнішній ключі для зв'язку з курсом
        public int CourseId { get; set; }
        // Навігаційна властивість для зв'язку з курсом, який обирає студент
        public Course? Course { get; set; }

        // Навігаційний властивість для зв'язку з оцінками
        public ICollection<Grade>? Grades { get; set; }

        // Навігаційна властивість для зв'язку зі списком коментарів, залишених студентом
        public ICollection<Comment>? Comments { get; set; }
        public Student() { }

        public Student(StudentViewModel studentViewModel)
        {
            Id = studentViewModel.Id;
            FirstName = studentViewModel.FirstName;
            LastName = studentViewModel.LastName;
            Email = studentViewModel.Email;
            CourseId = studentViewModel.CourseId;
        }
    }
}
