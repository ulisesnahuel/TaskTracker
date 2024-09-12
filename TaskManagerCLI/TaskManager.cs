using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManagerCLI
{
    public class TaskManager : ITaskManager
    {
        private readonly string filePath = "Tareas.json";
        private List<ItemTask> tasks;

        public TaskManager()
        {
            tasks = ReadTasksFromJson(filePath);
        }


        private void SaveTaskToJson(List<ItemTask> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, jsonString);

        }
        private List<ItemTask> ReadTasksFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                }
                return new List<ItemTask>();
            }

            string jsonString = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new List<ItemTask>();
            }

            List<ItemTask> taskList = JsonSerializer.Deserialize<List<ItemTask>>(jsonString) ?? new List<ItemTask>();

            return taskList;
        }


        public void Add(string entry)
        {

            Regex regex = new Regex("\"([^\"]*)\"");
            Match match = regex.Match(entry);

            var description =  match.Groups[1].Value;

            if (!match.Success)
            {
                Console.WriteLine("the system cannot add the task");
                return;
            }
            if (tasks.Any(d => d.Description == description))
            {
                Console.WriteLine("the task exists");
                return;
            }
            var newItemTask = new ItemTask(tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1, description);

            tasks.Add(newItemTask);
            SaveTaskToJson(tasks);
            Console.WriteLine($"Task added successfully (ID: {newItemTask.Id})");

        }



        public void Delete(int id)
        {
            tasks.Remove(tasks.Find(t => t.Id == id));
            SaveTaskToJson(tasks);

        }

        public void Update(int id, string entry)
        {
            Regex regex = new Regex("\"([^\"]*)\"");
            Match match = regex.Match(entry);
            var description = match.Groups[1].Value;

            if (!match.Success)
            {
                Console.WriteLine("the system cannot update the task");
                return;
            }
            if (tasks.Any(d => d.Description == description && d.Id != id ))
            {
                Console.WriteLine("the task exists");
                return;
            }
            var tasksSelected = tasks.Where(t => t.Id == id).FirstOrDefault();
            tasksSelected.Description = description;
            tasksSelected.UpdateAt = DateTime.Now;
            SaveTaskToJson(tasks);

        }

        public void GetTasks()
        {
            Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}", "ID", "Description", "Status", "Created", "Updated");

            foreach (var item in tasks)
            {
                Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}",
               item.Id,
               item.Description,
               item.Status,
               item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), 
               item.UpdateAt?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public void GetTasksDone()
        {
            Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}", "ID", "Description", "Status", "Created", "Updated");

            foreach (var item in tasks)
            {
                if (item.Status == TaskStatus.Done)
                    Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}",
               item.Id,
               item.Description,
               item.Status,
               item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
               item.UpdateAt?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }


        public void GetTasksInProgress()
        {
            Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}", "ID", "Description", "Status", "Created", "Updated");

            foreach (var item in tasks)
            {
                if (item.Status == TaskStatus.InProgress)
                    Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}",
               item.Id,
               item.Description,
               item.Status,
               item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
               item.UpdateAt?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        public void GetTasksToDo()
        {
            Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}", "ID", "Description", "Status", "Created", "Updated");

            foreach (var item in tasks)
            {
                if (item.Status == TaskStatus.ToDo)
                    Console.WriteLine("{0,-5} {1,-30} {2,-12} {3,-25} {4,-25}",
               item.Id,
               item.Description,
               item.Status,
               item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
               item.UpdateAt?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        public void MarkDone(int id)
        {
            var taskSelected = tasks.Find(t => t.Id == id);

            taskSelected.Status = TaskStatus.Done;
            SaveTaskToJson(tasks);

        }

        public void MarkInProcess(int id)
        {
            var taskSelected = tasks.Find(t => t.Id == id);

            taskSelected.Status = TaskStatus.InProgress;
            SaveTaskToJson(tasks);

        }


    }
}
