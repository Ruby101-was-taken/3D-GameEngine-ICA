using GD.Controllers;
using GD.Events;
using GD.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace RUB {
    [CreateAssetMenu(fileName = "Inventory", menuName = "RUB/Inventory")]
    public class Inventory : SerializedScriptableObject {
        [SerializeField]
        private List<AnswerData> items = new List<AnswerData>();

        [SerializeField]
        private int capacity = 3;

        [SerializeField]
        private ItemBehaviour ItemPrefab;

        [SerializeField, FoldoutGroup("Events")]
        private StringGameEvent itemRemovedEvent;
        [SerializeField, FoldoutGroup("Events")]
        private GameEvent inventoryFullEvent;
        [SerializeField, FoldoutGroup("Events")]
        private GameEvent inventoryNoLongerFullEvent;

        [SerializeField, ReadOnly, FoldoutGroup("Debug")]
        private int slotsFilled = 0;

        private int hightlightSlot = 0;

        public int SlotsFilled { get => slotsFilled; set => slotsFilled = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public int HightlightSlot { get => hightlightSlot; set => hightlightSlot = value; }

        public void Start() {
            Clear();
        }


        public void Clear() {
            itemRemovedEvent?.Raise("all");
            inventoryNoLongerFullEvent?.Raise();
            slotsFilled = 0;
            hightlightSlot = 0;
            items.Clear();
        }

        public bool AddItem(AnswerData item) {
            bool itemAdded = false;
            if(slotsFilled != capacity) {
                items.Add(item);
                slotsFilled++;
                itemAdded = true;
            }
            if(slotsFilled == capacity) inventoryFullEvent?.Raise();
            return itemAdded;
        }

        public bool TakeItems(AnswerData item) {
            bool itemRemoved = false;
            bool inventoryWasFull = slotsFilled == capacity;
            if(items.Contains(item)) {
                items.Remove(item);
                itemRemoved = true;
                slotsFilled--;
            }
            itemRemovedEvent?.Raise(item.Name);
            if(slotsFilled <  capacity && inventoryWasFull) inventoryNoLongerFullEvent?.Raise();
            return itemRemoved;
        }

        public void Drop() {
            if(slotsFilled > 0 && hightlightSlot < slotsFilled) {
                ItemBehaviour dropItem = Instantiate(ItemPrefab);
                Vector3 pos = FindFirstObjectByType<CharacterNavigationController>().transform.position;
                dropItem.transform.position = new Vector3(pos.x, -23.6f, pos.z);
                dropItem.GetComponent<ItemBehaviour>().UpdateItem(items[hightlightSlot]);
                TakeItems(items[hightlightSlot]);
            }
        }

        public bool HasItems(AnswerData item) {
            bool hasItem = false;
            if(items.Contains(item)) {
                hasItem = true;
            }
            return hasItem;
        }

        public bool HasItemHighlighted(AnswerData item) {
            bool hasItem = false;
            if(items[HightlightSlot] == item) {
                hasItem = true;
            }
            return hasItem;
        }
    }
}
