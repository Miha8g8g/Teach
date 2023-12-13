using System.Security.Claims;
using Teacherlog.ViewModels;

namespace Teacherlog.Models
{
    public class Teacher
    {
        public int Id { get; set; } // Унікальний ідентифікатор викладача
        public string FirstName { get; set; } // Ім'я викладача
        public string LastName { get; set; } // Прізвище викладача
        public string Email { get; set; } // Електронна пошта викладача

        // Навігаційна властивість для зв'язку зі списком пар, які веде викладач
        public ICollection<Clas>? Clases { get; set; }

        // Навігаційна властивість для зв'язку зі списком коментарів, залишених викладачем
        public ICollection<Comment>? Comments { get; set; }
        public Teacher() { }

        public Teacher(TeacherViewModel teacherViewModel)
        {
            Id = teacherViewModel.Id;
            FirstName = teacherViewModel.FirstName;
            LastName = teacherViewModel.LastName;
            Email = teacherViewModel.Email;
        }
    }
}
