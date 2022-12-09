using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public GetPriority Priority { get; set; }

        public GetStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? Completed { get; set; }


        public enum GetPriority
        {
            Low, medium, High
        }

        public enum GetStatus
        {
            Pending, Completed, Uncompleted
        }
    }
}
