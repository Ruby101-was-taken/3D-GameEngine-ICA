using System.Collections.Generic;
using RUB;
using UnityEngine;

public class AnswerAreaTrigger : MonoBehaviour
{
    [SerializeField]
    private List<RandomisedAnswerBoardBehaviour> boards;

    private bool randomised = false;

    public bool Randomised { get => randomised; set => randomised = value; }

    private void Awake() {
        Randomised = false;
    }

    public void UnDoRandomised() {
        Randomised = false;
        foreach(var board in boards) {
            board.ResetBoard();
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(randomised);
        if(other.CompareTag("Player") && !randomised) {
            foreach(var board in boards) {
                board.Randomise();
            }
            randomised = true;
        }
    }
}
