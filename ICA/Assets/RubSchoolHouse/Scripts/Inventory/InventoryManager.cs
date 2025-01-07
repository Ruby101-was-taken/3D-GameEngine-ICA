using GD.Items;
using UnityEngine;
using Sirenix.OdinInspector;
using RUB.Events;

namespace RUB {

    public class InventoryManager :  SerializedMonoBehaviour {
        #region VARS
        [SerializeField]
        private Inventory inventory;

        [FoldoutGroup("Debug"), SerializeField]
        private ItemData testItem;
        [FoldoutGroup("Debug"), SerializeField]
        private bool addTestItem;
        [FoldoutGroup("Debug"), SerializeField]
        private ItemPickupGameEvent itemPickupGameEvent;
        #endregion VARS

        private void Start() {
            inventory.Start();
            if(addTestItem) {
                itemPickupGameEvent?.Raise(new ItemPickup(testItem));
            }
        }

        public void AddItem(ItemPickup item) {
            inventory.AddItem(item.item, item.count);
        }
    }
}
