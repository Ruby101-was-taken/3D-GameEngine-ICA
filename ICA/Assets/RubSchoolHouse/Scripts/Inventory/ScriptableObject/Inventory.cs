using GD.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace RUB {
    [CreateAssetMenu(fileName = "Inventory", menuName = "RUB/Inventory")]
    public class Inventory : SerializedScriptableObject {
        [SerializeField]
        private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

        public void Start() {
            items.Clear();
        }

        public void AddItem(ItemData item, int count = 1) {
            if(count <= 0) {
                throw new System.Exception("Cannot add less than one item to an inventory!");
            }
            if(!items.ContainsKey(item)) {
                items.Add(item, count);
            }
            else {
                items[item] += count;
            }
        }

        public bool TakeItems(ItemData item, int count = 1) {
            bool itemRemoved = false;
            if(!items.ContainsKey(item)) {
                if(items[item] > count){
                    items[item] -= count;
                    itemRemoved = true;
                }
                else if(items[item] == count) {
                    items.Remove(item);
                    itemRemoved = true;
                }
            }
            return itemRemoved;
        }

        public bool HasItems(ItemData item, int count = 1) {
            bool hasItem = false;
            if(!items.ContainsKey(item)) {
                if(items[item] >= count) {
                    hasItem = true;
                }
            }
            return hasItem;
        }
    }
}
