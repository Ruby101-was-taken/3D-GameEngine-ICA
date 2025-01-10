using GD.Events;
using GD.Items;
using GD.Tick;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// Manages the game state by evaluating win and loss conditions.
    /// </summary>
    public class StateManager : MonoBehaviour, IHandleTicks
    {
        [FoldoutGroup("Timing & Reset", expanded: true)]
        [SerializeField]
        [Tooltip("The tick rate type for the state manager (i.e. multiple of baseTickIntervalSecs)")]
        private TimeTickSystem.TickRateMultiplierType tickRateType
    = TimeTickSystem.TickRateMultiplierType.BaseInterval;

        [FoldoutGroup("Timing & Reset")]
        [SerializeField]
        [Tooltip("Reset all conditions on start")]
        private bool resetAllConditionsOnStart = true;

        [FoldoutGroup("Context", expanded: true)]
        [SerializeField]
        [Tooltip("Player reference to evaluate conditions required by the context")]
        private Player player;

        [FoldoutGroup("Context")]
        [SerializeField]
        [Tooltip("Player inventory collection to evaluate conditions required by the context")]
        private InventoryCollection inventoryCollection;

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
        /// Indicates whether the game has ended.
        /// </summary>
        private bool gameEnded = false;

        private ConditionContext conditionContext;

        private void Awake()
        {
            if (player == null)
                throw new System.Exception("Player reference is required!");

            if (inventoryCollection == null)
                throw new System.Exception("Inventory collection reference is required!");

            // Wrap the two objects inside the context envelope
            conditionContext = new ConditionContext(player, inventoryCollection);

            // Register with the tick system
            TimeTickSystem.Instance.RegisterListener(tickRateType, HandleTick);
        }

        private void OnDestroy()
        {
            // Unregister with the tick system
            TimeTickSystem.Instance.UnregisterListener(tickRateType, HandleTick);
        }

        private void Start()
        {
            if (resetAllConditionsOnStart)
                ResetConditions();
        }

        /// <summary>
        /// Evaluates conditions each frame and handles game state transitions.
        /// </summary>
        //private void Update()  //TODO - NMCG : Slow down the update rate to once every 0.1 seconds
        //{
        //    //// If the game has already ended, no need to evaluate further
        //    //if (gameEnded)
        //    //    return;

        //    //// Evaluate the win condition
        //    //if (winCondition != null && winCondition.Evaluate(conditionContext))
        //    //{
        //    //    HandleWin();
        //    //    // Set gameEnded to true to prevent further updates
        //    //    gameEnded = true;
        //    //    // Optionally, disable this component
        //    //    // enabled = false;
        //    //}
        //    //// Evaluate the lose condition only if the win condition is not met
        //    //else if (loseCondition != null && loseCondition.Evaluate(conditionContext))
        //    //{
        //    //    HandleLoss();
        //    //    // Set gameEnded to true to prevent further updates
        //    //    gameEnded = true;
        //    //    // Optionally, disable this component
        //    //    // enabled = false;
        //    //}

        //    //foreach (var achievmentCondition in achievementConditions)
        //    //{
        //    //    if (achievmentCondition != null && achievmentCondition.Evaluate(conditionContext))
        //    //    {
        //    //        //do something
        //    //    }
        //    //}
        //}

        /// <summary>
        /// Handles the logic when the player wins.
        /// </summary>
        protected virtual void ConditionMet()
        {
            conditionMetGameEvent?.Raise();
        }

        /// <summary>
        /// Resets the win and loss conditions.
        /// Call this method when restarting the game or level.
        /// </summary>
        public void ResetConditions()
        {
            // Reset the gameEnded flag
            gameEnded = false;

            // Reset the condition
            if (condition != null)
                condition.ResetCondition();


        }

        /// <summary>
        /// Move code from Update to HandleTick to perform the tasks at a slower rate
        /// </summary>
        /// <see cref="TimeTickSystem"/>
        public void HandleTick()
        {
            // If the game has already ended, no need to evaluate further
            if (gameEnded)
                return;

            // Evaluate the win condition
            if (condition != null && condition.Evaluate(conditionContext))
            {
                ConditionMet();
            }
        }
    }
}