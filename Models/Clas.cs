using Teacherlog.ViewModels;

namespace Teacherlog.Models
{
    public class Clas
    {
        public int Id { get; set; }// Унікальний ідентифікатор пари

        // Зовнішній ключі для зв'язку з курсом та викладачем
        public int CourseId { get; set; } // Зовнішній ключ
        public int TeacherId { get; set; } // Зовнішній ключ

        // Навігаційні властивості для зв'язку з курсом та викладачем
        public Course? Course { get; set; }
        public Teacher? Teacher { get; set; }

        public DateTime Time { get; set; } // Час проведення пари
        public string Room { get; set; } // Номер аудиторії
        public Clas() { }
        public Clas(ClasViewModel clasViewModel)
        {
            Id = clasViewModel.Id;
            Time = clasViewModel.Time;
            Room = clasViewModel.Room;
            CourseId = clasViewModel.CourseId;
            TeacherId = clasViewModel.TeacherId;
        }
    }
}
