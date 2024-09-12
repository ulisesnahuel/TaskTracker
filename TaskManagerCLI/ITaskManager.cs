using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerCLI
{
    public interface ITaskManager
    {
        void Add(string description);
        void Update(int id,string description );
        void Delete(int id);

        void MarkInProcess(int id);
        void MarkDone(int id);
        void GetTasks();
         void GetTasksDone();
         void GetTasksToDo();
         void GetTasksInProgress();

    }
}
