using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Teacherlog.Models;

namespace Teacherlog.ViewModels
{
    public class ClasViewModel
    {
        public int Id { get; set; }// Унікальний ідентифікатор пари

        // Зовнішній ключі для зв'язку з курсом та викладачем
        public int CourseId { get; set; } // Зовнішній ключ
        public int TeacherId { get; set; } // Зовнішній ключ

        // Навігаційні властивості для зв'язку з курсом та викладачем
        public Course? Course { get; set; }
        public Teacher? Teacher { get; set; }
        [Remote("IsClasNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public DateTime Time { get; set; } // Час проведення пари
        [Remote("IsClasNameAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(50, ErrorMessage = "Максимальна довжина поля - 50 символів")]
        public string Room { get; set; } // Номер аудиторії

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public string? CourseName { get; set; }
        public string? Description { get; set; }

        public ClasViewModel() { }
        public ClasViewModel(Clas clas)
        {
            Id = clas.Id;
            Time = clas.Time;
            Room = clas.Room;
            CourseId = clas.CourseId;
            TeacherId = clas.TeacherId;
            if (clas.Teacher != null)
            {
                FirstName = clas.Teacher.FirstName;
                LastName = clas.Teacher.LastName;
                Email = clas.Teacher.Email;
            }
            if (clas.Course != null)
            {
                CourseName = clas.Course.CourseName;
                Description = clas.Course.Description;

            }
        }
    }
}
