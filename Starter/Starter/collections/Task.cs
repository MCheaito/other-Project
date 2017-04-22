using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace Starter.collections
{
    /// <summary>
    /// task is class to handle component's feature.
    /// </summary>
    /// 
    public enum TaskStatus
    {
        Stop=0,
        Run= 1
    }
    delegate void TaskCallBackMethod(Task tsk);
    class Task
    {
        private string taskId;
        private TaskStatus status;
        private TaskCallBackMethod cb;
        private ToolStripMenuItem cmi;// menu linked to task
        private string type;
        private string name;
        private string repository;  //assembly name (without Dll + class name)

        private string methodName;     //method name
        private string className;     //name space + classe name

        private string parameters;  //parametres seperated by |
        private Timer clock;
        private bool startup;
        private string icon;
        public bool ScheduelTask
        {
            get {
                return (clock != null);
            }
        }


        public Task(ToolStripMenuItem cmi, TaskCallBackMethod cb)
        {
            this.cmi = cmi;
//            this.cmi.Image = Image.FromFile(".images\\Disable_" + icon);
            this.cb = cb;
        }

        void clock_Tick(object sender, EventArgs e)
        {
            Execute();
        }
        public string Id
        {
            get { return taskId; }
            set { taskId = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Repository
        {
            get { return repository; }
            set { repository = value; }
        }
        public string Feature
        {
            get { return className+"."+methodName; }
            set
            {
                string[] st = value.Split(new char[] { '.' });
                this.methodName = st[st.Length - 1];
                this.className = value.Substring(0, value.Length - this.methodName.Length-1);
            }
        }
        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
        public TaskStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Interval
        {
            set {
                clock = new Timer();
                clock.Tick += new EventHandler(clock_Tick);
                clock.Interval = value; }
        }
        public bool Startup
        {
            set
            {
                startup = value;
            }
            get { return startup; }
        }
        public string Icon
        {
            set
            {
                icon = value;
            }
            get { return icon; }
        }

        public void Start()
        {
            status = TaskStatus.Run;
            this.cmi.Image = Image.FromStream(Main.GetFileRessouces("Icons.bullet_ball_green.ico"));
            if (clock != null)
            {
                clock.Start();
            }
            else {
                ExecuteTask();
            } 


        }
        public void Stop()
        {
            status = TaskStatus.Stop;
            this.cmi.Image = Image.FromStream(Main.GetFileRessouces("Icons.bullet_ball_grey.ico")); 
            clock.Stop();
        }

        private string getResult()
        {
            string[] paras = null;
            if (parameters != "") paras = parameters.Split(new char[] { ';' });
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(this.repository + ((this.repository.Trim().EndsWith(".dll"))? "":".dll"));
            Object obj = asm.CreateInstance(className);
            return ((string)obj.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, paras));
        }
        private void ExecuteTask()
        {
            string[] paras = null;
            if (parameters != "") paras = parameters.Split(new char[] { ';' });

            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(this.repository + ((this.repository.Trim().EndsWith(".dll")) ? "" : ".dll"));
            Object obj = asm.CreateInstance(className);
            string s = (string)obj.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, paras);
        }

        private void Execute()
        {
            if (this.type == "MenuBuiltIn") cmi.Text = getResult();
            if (this.type == "Console")  ExecuteTask();

        }
    }
}
