using System.ComponentModel.DataAnnotations;
using Teacherlog.Models;

namespace Teacherlog.ViewModels
{
    public class CommentViewModel
    
    {
        public int Id { get; set; }// Унікальний ідентифікатор коментаря

        // Зовнішній ключі для зв'язку зі студентом та викладачем
        public int StudentId { get; set; } // Зовнішній ключ
        public int TeacherId { get; set; } // Зовнішній ключ

        // Навігаційні властивості для зв'язку зі студентом та викладачем
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 80 символів")]
        public string Text { get; set; } // Текст коментаря



        public string? FirstNameS { get; set; }
        public string? LastNameS { get; set; }
        public string? EmailS { get; set; }

        public string? FirstNameT { get; set; }
        public string? LastNameT { get; set; }
        public string? EmailT { get; set; }

        public CommentViewModel() { }
        public CommentViewModel(Comment comment)
        {
            Id = comment.Id;
            StudentId = comment.StudentId;
            TeacherId = comment.TeacherId;
            Text = comment.Text;
            if (comment.Student != null)
            {
                FirstNameS = comment.Student.FirstName;
                LastNameS = comment.Student.LastName;
                EmailS = comment.Student.Email;
            }
            if (comment.Teacher != null)
            {
                FirstNameT = comment.Teacher.FirstName;
                LastNameT = comment.Teacher.LastName;
                EmailT = comment.Teacher.Email;
            }

        }
    }
}

