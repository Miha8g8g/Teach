using System.ComponentModel.DataAnnotations;
using Teacherlog.Models;

namespace Teacherlog.ViewModels
{
    public class GradeViewModel
    {
        public int Id { get; set; }// Унікальний ідентифікатор оцінки

        // Зовнішній ключі для зв'язку зі студентом та курсом
        public int StudentId { get; set; } // Зовнішній ключ
        public int CourseId { get; set; } // Зовнішній ключ

        // Навігаційні властивості для зв'язку зі студентом та курсом, які пов'язані з оцінкою
        public Student? Student { get; set; }
        public Course? Course { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [Range(1, int.MaxValue, ErrorMessage = "оцінка  не може бути від'ємною або нюльовою")]
        public int Value { get; set; } // Значення оцінки

        public string? FirstNameS {  get; set; }
        public string? LastNameS { get; set; }
        public string? EmailS { get; set; }

        public string? CourseName { get; set; }
        public string? Description { get; set; }

        public GradeViewModel() { }

        public GradeViewModel(Grade grade)
        {
            Id = grade.Id;
            Value = grade.Value;
            StudentId = grade.StudentId;
            CourseId = grade.CourseId;
            if (grade.Student != null)
            {
                FirstNameS = grade.Student.FirstName;
                LastNameS = grade.Student.LastName;
                EmailS = grade.Student.Email;
            }
            if (grade.Course != null) 
            {
                CourseName = grade.Course.CourseName;
                Description = grade.Course.Description;
            }
        }

    }
}
