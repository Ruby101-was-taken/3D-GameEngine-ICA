using GD;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraSwitcherBehaviour : MonoBehaviour
{

    [SerializeField, Title("GameEvents")]
    private TransformGameEvent gameEvent;

    [SerializeField, Title("Camera Focus"), Tooltip("The transform the camera will look at")]
    private Transform cameraFocus;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameEvent?.Raise(cameraFocus);
        } 
    }
}
