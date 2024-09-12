using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerCLI
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
        
    }
    public class ItemTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }


        public ItemTask(int id, string description)
        {
            Id = id;
            Description = description;
            Status = TaskStatus.ToDo;
            CreatedAt = DateTime.Now;
        }

        
    }

    
}