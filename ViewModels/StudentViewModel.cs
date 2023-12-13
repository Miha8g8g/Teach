using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Teacherlog.Models;

namespace Teacherlog.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Remote("IsStudentNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string FirstName { get; set; } // Ім'я студента
        [Remote("IsStudentNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string LastName { get; set; } // Прізвище студента
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Довжина повинна бути від 7 до 50 символів")]
        public string Email { get; set; } // Електронна пошта студента
        // Зовнішній ключі для зв'язку з курсом
        public int CourseId { get; set; }
        // Навігаційна властивість для зв'язку з курсом, який обирає студент
        public Course? Course { get; set; }

        // Навігаційний властивість для зв'язку з оцінками
        public ICollection<Grade>? Grades { get; set; }

        // Навігаційна властивість для зв'язку зі списком коментарів, залишених студентом
        public ICollection<Comment>? Comments { get; set; }

        public StudentViewModel() { }

        public StudentViewModel(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Email = student.Email;
        }
    }
}
