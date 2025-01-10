using DG.Tweening;
using GD.Items;
using GD.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RUB {
    public class InventoryUIManager : MonoBehaviour {
        [SerializeField]
        private Inventory Inventory;
        [SerializeField]
        private RectTransform InventoryItem;
        [SerializeField]
        private int InventorySize;
        [SerializeField]
        private Transform InventoryUI;

        [SerializeField, FoldoutGroup("Spacing")]
        private int LeftSidePadding = 30;
        [SerializeField, FoldoutGroup("Spacing")]
        private int TopSidePadding = 30;
        [SerializeField, FoldoutGroup("Spacing")]
        private int Spacing = 30;

        private void Start() {
            UpdatePosition();
        }
        private void UpdatePosition() {
            InventoryItem.transform.position = new Vector3(LeftSidePadding + (Spacing * (Inventory.SlotsFilled)), -TopSidePadding);
        }
        public void AddItem(ItemPickup item) {
            UpdatePosition();
            Instantiate(InventoryItem, InventoryUI);
        }

    }
}