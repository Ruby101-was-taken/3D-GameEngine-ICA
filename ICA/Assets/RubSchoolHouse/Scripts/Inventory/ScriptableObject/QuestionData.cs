using GD.Items;
using RUB.categories;
using UnityEngine;


namespace RUB.Questions {
    [CreateAssetMenu(fileName = "Question 1", menuName = "RUB/Questions/Question")]
    public class QuestionData : ScriptableObject {
        [SerializeField, TextArea(2, 4)]
        public string question = "...?";
        [SerializeField]
        public AnswerData answer;
        [SerializeField]
        public QuestionCategories category;

        public bool CheckAnswer(AnswerData item) {
            return item == answer;
        }
    }
}
