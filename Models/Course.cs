using System.Security.Claims;
using Teacherlog.ViewModels;

namespace Teacherlog.Models
{
    public class Course
    {
        public int Id { get; set; }// Унікальний ідентифікатор курсу
        public string CourseName { get; set; } // Назва курсу
        public string Description { get; set; } // Опис курсу

        // Навігаційна властивість для зв'язку зі списком студентів, які обирають цей курс
        public ICollection<Student>? Students { get; set; }

        // Навігаційна властивість для зв'язку зі списком пар, пов'язаних з цим курсом
        public ICollection<Clas>? Clases { get; set; }
        // Навігаційний властивість для зв'язку з оцінками
        public ICollection<Grade>? Grades { get; set; }
        public Course() { }
        public Course(CourseViewModel courseViewModel)
        {
            Id = courseViewModel.Id;
            CourseName = courseViewModel.CourseName;
            Description = courseViewModel.Description;
        }
    }
}
