using System.Collections.Generic;
using GD.State;
using GD.Tick;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RUB.State {
    public class AreaQuestionsCompletedManager : StateManager {
        [FoldoutGroup("Context", expanded: true)]
        [SerializeField]
        [Tooltip("Player reference to evaluate conditions required by the context")]
        private List<AnswerBoardBehaviour> answerBoards;


        private void Awake() {
            conditionContext = new ConditionContext(answerBoards);
        }


    }
}
