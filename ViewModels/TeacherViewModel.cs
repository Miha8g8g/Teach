using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Teacherlog.Models;

namespace Teacherlog.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; } // Унікальний ідентифікатор викладача
        [Remote("IsTeacherNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string FirstName { get; set; } // Ім'я викладача
        [Remote("IsTeacherNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string LastName { get; set; } // Прізвище викладача
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Довжина повинна бути від 7 до 50 символів")]
        public string Email { get; set; } // Електронна пошта викладача

        // Навігаційна властивість для зв'язку зі списком пар, які веде викладач
        public ICollection<Clas>? Clases { get; set; }

        // Навігаційна властивість для зв'язку зі списком коментарів, залишених викладачем
        public ICollection<Comment>? Comments { get; set; }

        public TeacherViewModel() { }

        public TeacherViewModel(Teacher teacher)
        {
            Id = teacher.Id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Email = teacher.Email;
        }
    }
}
