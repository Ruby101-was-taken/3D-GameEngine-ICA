using System.Collections.Generic;
using GD.Items;
using RUB;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// Store reference to entities/objects that the conditions need to check against.
    /// </summary>
    public class ConditionContext
    {

        [Tooltip("All the questions that must be answered.")]
        [SerializeField, FoldoutGroup("Specific Context")]
        private List<AnswerBoardBehaviour> answerBoards;
        public List<AnswerBoardBehaviour> AnswerBoards { get => answerBoards; set => answerBoards = value; }

        public ConditionContext(List<AnswerBoardBehaviour> answerBoards)
        {
            this.AnswerBoards = answerBoards;
        }

    }
}