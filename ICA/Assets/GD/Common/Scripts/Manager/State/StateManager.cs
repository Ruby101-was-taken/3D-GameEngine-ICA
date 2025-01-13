using GD.Events;
using GD.Items;
using GD.Tick;
using Sirenix.OdinInspector;
using UnityEngine;
using static GD.Tick.TimeTickSystem;

namespace GD.State
{
    /// <summary>
    /// Manages the game state by evaluating win and loss conditions.
    /// </summary>
    public abstract class StateManager : MonoBehaviour, IHandleTicks
    {
        [FoldoutGroup("Timing & Reset", expanded: true)]
        [SerializeField]
        [Tooltip("The tick rate type for the state manager (i.e. multiple of baseTickIntervalSecs)")]
        protected TimeTickSystem.TickRateMultiplierType tickRateType = TimeTickSystem.TickRateMultiplierType.BaseInterval;

        [FoldoutGroup("Timing & Reset")]
        [SerializeField]
        [Tooltip("Reset all conditions on start")]
        private bool resetAllConditionsOnStart = true;

        /// <summary>
        /// The condition that determines if the player wins.
        /// </summary>
        [FoldoutGroup("Condition")]
        [SerializeField]
        [Tooltip("The condition that must be met")]
        private ConditionBase condition;

        [FoldoutGroup("Events")]
        [SerializeField]
        [Tooltip("Event that will be run when the condition is met")]
        private GameEvent conditionMetGameEvent;


        /// <summary>
        /// Indicates whether the condition is met
        /// </summary>
        [SerializeField, ReadOnly, FoldoutGroup("Debug")]
        private bool conditionMet = false;

        protected ConditionContext conditionContext;


        private void OnDestroy()
        {
            // Unregister with the tick system
            TimeTickSystem.Instance.UnregisterListener(tickRateType, HandleTick);
        }

        private void Start()
        {
            if (resetAllConditionsOnStart)
                ResetConditions();
            TimeTickSystem.Instance.RegisterListener(tickRateType, HandleTick);
        }

        /// <summary>
        /// Handles the logic when the condition is met.
        /// </summary>
        protected virtual void ConditionMet()
        {
            conditionMetGameEvent?.Raise();
        }

        /// <summary>
        /// Resets the condition.
        /// Call this method when restarting the game or level.
        /// </summary>
        public void ResetConditions()
        {
            // Reset the conditionMet flag
            conditionMet = false;

            // Reset the condition
            if (condition != null)
                condition.ResetCondition();


        }

        /// <summary>
        /// Move code from Update to HandleTick to perform the tasks at a slower rate
        /// </summary>
        /// <see cref="TimeTickSystem"/>
        public void HandleTick() {
            // If the game has already ended, no need to evaluate further
            if (conditionMet)
                return;
            // Evaluate the win condition
            if (condition != null && condition.Evaluate(conditionContext))
            {
                ConditionMet();
            }
        }
    }
}