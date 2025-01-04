using System;
using System.Collections.Generic;
using UnityEngine;
using GD;

namespace GD.Task
{
    /// <summary>
    /// Manages scheduling, sorting, and cancellation of tasks.
    /// Delegates execution to TaskExecutor.
    /// </summary>
    public class TaskManager : Singleton<TaskManager>
    {
        private readonly List<ITask> tasks = new List<ITask>();
        private TaskExecutor executor;

        public event Action<ITask> OnTaskCompleted;

        protected override void Awake()
        {
            base.Awake();
            executor = new TaskExecutor(tasks, () =>
            {
                OnTaskCompleted?.Invoke(null); // Notify when tasks complete
            });
        }

        public void ScheduleTask(ITask task)
        {
            tasks.Add(task);
            tasks.Sort((a, b) => a.NextExecutionTime.CompareTo(b.NextExecutionTime));
            executor.StartExecution();
        }

        public void CancelTask(ITask task)
        {
            tasks.Remove(task);
        }
    }
}