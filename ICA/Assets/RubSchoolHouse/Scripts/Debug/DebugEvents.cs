using GD.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

public class DebugEvents : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Game Events To Run On Start")]
    private List<GameEvent> StartGameEvents;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        foreach (GameEvent gameEvent in StartGameEvents) {
            gameEvent?.Raise();
        }
    }

    public void RaiseEvent(GameEvent gameEvent) {
        gameEvent?.Raise();
    }

}
