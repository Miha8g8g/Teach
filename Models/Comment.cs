using Teacherlog.ViewModels;

namespace Teacherlog.Models
{
    public class Comment
    {
        public int Id { get; set; }// Унікальний ідентифікатор коментаря

        // Зовнішній ключі для зв'язку зі студентом та викладачем
        public int StudentId { get; set; } // Зовнішній ключ
        public int TeacherId { get; set; } // Зовнішній ключ

        // Навігаційні властивості для зв'язку зі студентом та викладачем
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }

        public string Text { get; set; } // Текст коментаря
        public Comment() { }
        public Comment(CommentViewModel commentViewModel)
        {
            Id = commentViewModel.Id;
            Text = commentViewModel.Text;
            StudentId = commentViewModel.StudentId;
            TeacherId = commentViewModel.TeacherId;
        }
    }
}
