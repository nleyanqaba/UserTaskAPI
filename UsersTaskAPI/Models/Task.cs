using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTasksAPI.Models
{
    public class TaskItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public int Assignee { get; set; } // User ID  

        public DateTime DueDate { get; set; }
    }
}
