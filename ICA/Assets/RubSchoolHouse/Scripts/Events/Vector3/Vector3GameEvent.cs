using UnityEngine;
using GD.Events;

/*
 *  Vector3 GameEvent - STARTED BY AND WRITTEN BY RUBY FOR revolvium (Game Dev GCA -> https://github.com/JakeDaSpud/revolvium)
 *  Based on game events given in class, located in the GD folder
 */
namespace RUB.Events {
    [CreateAssetMenu(fileName = "Vector3GameEvent",
    menuName = "RUB/Patterns/Events/Vector3")]
    public class Vector3GameEvent : BaseGameEvent<Vector3>
    { }
}