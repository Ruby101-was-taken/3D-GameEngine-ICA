using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD.Task
{
    /// <summary>
    /// Executes tasks efficiently using a single coroutine.
    /// </summary>
    public class TaskExecutor
    {
        private readonly List<ITask> tasks;
        private readonly Action onAllTasksCompleted;
        private Coroutine executorCoroutine;

        public TaskExecutor(List<ITask> tasks, Action onAllTasksCompleted)
        {
            this.tasks = tasks;
            this.onAllTasksCompleted = onAllTasksCompleted;
        }

        public void StartExecution()
        {
            if (executorCoroutine == null)
                executorCoroutine = TaskManager.Instance.StartCoroutine(ExecutionCoroutine());
        }

        private IEnumerator ExecutionCoroutine()
        {
            while (tasks.Count > 0)
            {
                float currentTime = Time.time;

                // Execute ready tasks
                for (int i = 0; i < tasks.Count;)
                {
                    ITask task = tasks[i];

                    if (task.IsReadyToExecute(currentTime))
                    {
                        task.Execute();

                        if (task.IsComplete)
                        {
                            tasks.RemoveAt(i);
                            continue;
                        }

                        task.UpdateNextExecutionTime();
                    }
                    i++;
                }

                // Sort tasks by next execution time
                tasks.Sort((a, b) => a.NextExecutionTime.CompareTo(b.NextExecutionTime));

                // Wait until the next task is ready
                if (tasks.Count > 0)
                {
                    float nextTaskTime = tasks[0].NextExecutionTime;
                    float waitTime = Mathf.Max(0, nextTaskTime - Time.time);
                    yield return new WaitForSeconds(waitTime);
                }
            }

            executorCoroutine = null;
            onAllTasksCompleted?.Invoke();
        }
    }
}