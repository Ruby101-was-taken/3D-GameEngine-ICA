using UnityEngine;
using GD.State;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace RUB.State {
    /// <summary>
    /// A condition that ensures prefabs are spawned within a specified area.
    /// </summary>
    [CreateAssetMenu(fileName = "AreaQuestionsAnswered", menuName = "RUB/Conditions/Single/AreaQuestionsAnswered", order = 1)]
    public class AreaQuestionsAnswered : ConditionBase {

        protected override bool EvaluateCondition(ConditionContext conditionContext) {
            foreach(AnswerBoardBehaviour answerBoard in conditionContext.AnswerBoards) {
                if(!answerBoard.Answered)
                    return false;
            }
            return true;
        }
    }
}