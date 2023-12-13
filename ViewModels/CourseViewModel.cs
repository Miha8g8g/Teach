using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Teacherlog.Models;

namespace Teacherlog.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }// Унікальний ідентифікатор курсу
        [Remote("IsCourseNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string CourseName { get; set; } // Назва курсу
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]

        public string Description { get; set; } // Опис курсу

        // Навігаційна властивість для зв'язку зі списком студентів, які обирають цей курс
        public ICollection<Student>? Students { get; set; }

        // Навігаційна властивість для зв'язку зі списком пар, пов'язаних з цим курсом
        public ICollection<Clas>? Clases { get; set; }
        // Навігаційний властивість для зв'язку з оцінками
        public ICollection<Grade>? Grades { get; set; }
        public CourseViewModel() { }

        public CourseViewModel(Course course)
        {
            Id = course.Id;
            CourseName = course.CourseName;
            Description = course.Description;

        }
    }
}
