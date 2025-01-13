using System.Collections.Generic;
using RUB.Questions;
using UnityEngine;

namespace RUB {
    public class RandomisedAnswerBoardBehaviour : MonoBehaviour {

        [SerializeField]
        private AnswerBoardBehaviour AnswerBoardBehaviour;
        [SerializeField]
        private List<QuestionData> possibleQuestions;

        [SerializeField]
        private List<ItemBehaviour> items;

        [SerializeField]
        private QuestionData defaultQuestionData;

        public void Randomise() {
            if(possibleQuestions.Count > 0) {
                int randomQuestionIndex = Random.Range(0, possibleQuestions.Count - 1);
                AnswerBoardBehaviour.Question = possibleQuestions[randomQuestionIndex];
                int i = 0;
                int randomAnswerIndex = Random.Range(0, items.Count - 1);
                Debug.Log($"Correct Answer: {randomAnswerIndex}");
                foreach(var item in items) {
                    if(i == randomAnswerIndex) {
                        item.UpdateItem(possibleQuestions[randomQuestionIndex].answer);
                    }
                    else {
                        int rand = Random.Range(0, possibleQuestions.Count - 1);
                        do {
                            rand = Random.Range(0, possibleQuestions.Count - 1);
                        }while(rand == randomQuestionIndex);
                        item.UpdateItem(possibleQuestions[rand].answer);
                    }
                    i++;
                }
            }
        }
        public void ResetBoard() {
            AnswerBoardBehaviour.Question = defaultQuestionData;
        }
    }
}
