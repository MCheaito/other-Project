using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;


namespace Starter.collections
{
    class TaskCollections :System.Collections.CollectionBase
    {
        private List<Task> taskList;
        public TaskCollections()
        {
            taskList = new List<Task>();
        }
        public void Add(Task tsk)
        {
            lock (taskList)
            {
                taskList.Add(tsk);
                if (tsk.Startup) Start(tsk);
            }
        }
        public void Remove(int index)
        {
            // Check to see if there is a widget at the supplied index.
            if (index > Count - 1 || index < 0)
            // If no widget exists, a messagebox is shown and the operation 
            // is cancelled.
            {
                System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                lock (taskList)
                {
                    taskList.RemoveAt(index);
                }
            }

        }
        public void Remove(Task tsk)
        {
            lock (taskList)
            {
                taskList.Remove(tsk);
            }
        }
        public Task Item(int Index)
        {
            // The appropriate item is retrieved from the List object and
            // explicitly cast to the Widget type, then returned to the 
            // caller.
            return (Task)taskList[Index];
        }
        public Task Create(ToolStripMenuItem cmi)
        {
            return new Task(cmi, null);
        }
        public Task Find(string taskId)
        {

            return (taskList.Find(delegate(Task predicate) { return predicate.Id.Equals(taskId); }));
        }
        public void Start(string taskId)
        {
            Task tsk = this.Find(taskId);
            Start(tsk);
        }
        public void Start(Task tsk)
        {   
            tsk.Start();
        }

        public void Toggle(string taskId)
        {           
            Task tsk = this.Find(taskId);
            if (!tsk.ScheduelTask)
            {
                tsk.Start();
            }
            else
            {
                if (tsk.Status == TaskStatus.Run)
                    tsk.Stop();
                else
                    tsk.Start();
            }

 
        }

    }
}
