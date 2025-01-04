using System;
using UnityEngine;

namespace GD.Task
{
    /// <summary>
    /// Represents a task with scheduled execution logic.
    /// </summary>
    public class ScheduledTask : MonoBehaviour, ITask
    {
        private Action executeAction;
        private float startDelay;
        private float repeatInterval;
        private int repeatCount;
        private int executedCount;
        private float nextExecutionTime;

        public bool IsComplete => repeatCount != -1 && executedCount >= repeatCount;
        public float NextExecutionTime => nextExecutionTime;

        public void Initialize(Action action, float startDelay, float repeatInterval, int repeatCount)
        {
            this.executeAction = action;
            this.startDelay = startDelay;
            this.repeatInterval = repeatInterval;
            this.repeatCount = repeatCount;

            executedCount = 0;
            nextExecutionTime = Time.time + startDelay;
        }

        public bool IsReadyToExecute(float currentTime)
        {
            return currentTime >= nextExecutionTime;
        }

        public void Execute()
        {
            executeAction?.Invoke();
            executedCount++;
        }

        public void UpdateNextExecutionTime()
        {
            if (!IsComplete)
                nextExecutionTime = Time.time + repeatInterval;
        }
    }
}