using Teacherlog.ViewModels;

namespace Teacherlog.Models
{
    public class Grade
    {
        public int Id { get; set; }// Унікальний ідентифікатор оцінки

        // Зовнішній ключі для зв'язку зі студентом та курсом
        public int StudentId { get; set; } // Зовнішній ключ
        public int CourseId { get; set; } // Зовнішній ключ

        // Навігаційні властивості для зв'язку зі студентом та курсом, які пов'язані з оцінкою
        public Student? Student { get; set; }
        public Course? Course { get; set; }

        public int Value { get; set; } // Значення оцінки
        public Grade() { }
        public Grade(GradeViewModel gradeViewModel)
        {
            Id = gradeViewModel.Id;
            Value = gradeViewModel.Value;
            StudentId = gradeViewModel.StudentId;
            CourseId = gradeViewModel.CourseId;
        }
    }
}
