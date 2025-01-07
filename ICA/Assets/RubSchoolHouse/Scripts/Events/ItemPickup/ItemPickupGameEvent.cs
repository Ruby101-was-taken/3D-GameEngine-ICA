using UnityEngine;
using GD.Events;

/*
 *  Item Pickup GameEvent - STARTED BY AND WRITTEN BY RUBY
 *  Based on game events given in class, located in the GD folder
 */
namespace RUB.Events {
    [CreateAssetMenu(fileName = "ItemPickupGameEvent",
    menuName = "RUB/Patterns/Events/ItemPickup")]
    public class ItemPickupGameEvent : BaseGameEvent<ItemPickup>
    { }
}