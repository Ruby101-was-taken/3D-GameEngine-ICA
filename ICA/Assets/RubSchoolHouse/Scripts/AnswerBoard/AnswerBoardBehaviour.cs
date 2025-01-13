using System.Net.NetworkInformation;
using GD.Events;
using RUB.Questions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RUB {
    public class AnswerBoardBehaviour : MonoBehaviour {
        [SerializeField]
        private QuestionData question;

        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private GameEvent correctEvent;

        [SerializeField, ReadOnly, FoldoutGroup("Debug")]
        private bool answered = false;

        [SerializeField, FoldoutGroup("Visuals")]
        private TextMeshPro text;

        public bool Answered { get => answered; set => answered = value; }
        public QuestionData Question { get => question; set { question = value; text.text = question.question; } }

        private void Start() {
            text.text = Question.question;
        }


        private void Reset() {
            Answered = false;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player") && !Answered) {
                if(inventory.HasItemHighlighted(Question.answer)) {
                    inventory.TakeItems(Question.answer);
                    Answered=true;
                    text.text = "CORRECT!";
                    correctEvent?.Raise();
                }
            }
        }
    }
}