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
        private Dictionary<AnswerData, int> items = new Dictionary<AnswerData, int>();

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

        public bool AddItem(AnswerData item, int count = 1) {
            bool itemAdded = false;
            if(count <= 0) {
                itemAdded = false;
                throw new System.Exception("Cannot add less than one item to an inventory!");
            }
            if(!items.ContainsKey(item)) {
                items.Add(item, count);
                slotsFilled++;
                itemAdded = true;
            }
            else {
                items[item] += count;
                slotsFilled++;
                itemAdded = true;
            }
            if(slotsFilled == capacity) inventoryFullEvent?.Raise();
            return itemAdded;
        }

        public bool TakeItems(AnswerData item, int count = 1) {
            bool itemRemoved = false;
            bool inventoryWasFull = slotsFilled == capacity;
            if(items.ContainsKey(item)) {
                if(items[item] > count){
                    items[item] -= count;
                    itemRemoved = true;
                }
                else if(items[item] == count) {
                    items.Remove(item);
                    itemRemoved = true;
                    slotsFilled--;
                }
                itemRemovedEvent?.Raise(item.Name);
            }
            if(slotsFilled <  capacity && inventoryWasFull) inventoryNoLongerFullEvent?.Raise();
            return itemRemoved;
        }

        public void Drop() {
            List<AnswerData> keys = new List<AnswerData>(items.Keys);
            if(slotsFilled > 0 && hightlightSlot < slotsFilled) {
                TakeItems(keys[hightlightSlot]);
                ItemBehaviour dropItem = Instantiate(ItemPrefab);
                Vector3 pos = FindFirstObjectByType<CharacterNavigationController>().transform.position;
                dropItem.transform.position = new Vector3(pos.x, -23.6f, pos.z);
                dropItem.GetComponent<ItemBehaviour>().UpdateItem(keys[hightlightSlot]);
            }
        }

        public bool HasItems(AnswerData item, int count = 1) {
            bool hasItem = false;
            if(items.ContainsKey(item)) {
                if(items[item] >= count) {
                    hasItem = true;
                }
            }
            return hasItem;
        }

        public bool HasItemHighlighted(AnswerData item, int count = 1) {
            bool hasItem = false;
            List<AnswerData> keys = new List<AnswerData>(items.Keys);
            if(keys[HightlightSlot] == item) {
                if(items[item] >= count) {
                    hasItem = true;
                }
            }
            return hasItem;
        }
    }
}
