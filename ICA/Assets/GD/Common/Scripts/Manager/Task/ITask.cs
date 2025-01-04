using System;

namespace GD.Task
{
    public interface ITask
    {
        void Initialize(Action executeAction, float startDelay, float repeatInterval, int repeatCount);
        bool IsReadyToExecute(float currentTime);
        void Execute();
        bool IsComplete { get; }
        float NextExecutionTime { get; }
        void UpdateNextExecutionTime();
    }
}