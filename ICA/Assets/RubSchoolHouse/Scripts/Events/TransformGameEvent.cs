using UnityEngine;
using GD.Events;

/*
 *  Transform GameEvent - STARTED BY AND WRITTEN BY RUBY FOR revolvium (Game Dev GCA -> https://github.com/JakeDaSpud/revolvium)
 *  Based on game events given in class, located in the GD folder
 */

namespace GD
{
    [CreateAssetMenu(fileName = "TransformGameEvent",
    menuName = "RUB/Patterns/Events/Transform")]
    public class TransformGameEvent : BaseGameEvent<Transform>
    { }
}