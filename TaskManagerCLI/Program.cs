// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using TaskManagerCLI;

class Program
{

    static void Main(string[] args)
    {
        
        TaskManager taskManager = new TaskManager();
        
        string entry;


        string command;



        do
        {
            Console.Write("task-cli ");           
            entry = Console.ReadLine();

            string[] partsCommand = entry.Split(' ');

            command = partsCommand[0];


            command = entry.Split(' ')[0];
            if (command == "list")
            {
                if (partsCommand.Length > 1)
                {
                    command += " " +  partsCommand[1];
                }
                
            }
            switch (command) 
            {
                case "add":
                    taskManager.Add(entry);                    
                    break;
                case "update":
                    taskManager.Update(int.Parse(partsCommand[1]), entry);
                    break ;
                case "delete":
                    taskManager.Delete(int.Parse(partsCommand[1]));
                    break;
                case "mark-in-progress":
                    taskManager.MarkInProcess(int.Parse(partsCommand[1]));
                    break;
                case "mark-done":
                    taskManager.MarkDone(int.Parse(partsCommand[1]));
                    break;
                case "list":
                    taskManager.GetTasks();
                    break;
                case "list done":
                    taskManager.GetTasksDone();
                    break;
                case "list todo":
                    taskManager.GetTasksToDo();
                    break;
                case "list in-progress":
                    taskManager.GetTasksInProgress();

                    break;
                default:
                    Console.WriteLine("error");
                    break;
            }

        }
        while (command != "exit");
    }
}
