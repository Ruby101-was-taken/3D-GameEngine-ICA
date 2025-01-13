using System.Collections.Generic;
using DG.Tweening;
using GD.Items;
using GD.Pool;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RUB {
    public class InventoryUIManager : MonoBehaviour {
        [SerializeField]
        private Inventory Inventory;
        [SerializeField]
        private RectTransform InventorySlotPrefab;
        [SerializeField]
        private RectTransform InventoryItemPrefab;
        [SerializeField]
        private RectTransform Highlight;
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

        private List<RectTransform> InventoryItems = new List<RectTransform>();

        private void Awake() {
            InventoryItemPrefab.position = new Vector3(0,0,0);
            MoveHightlight(0);
        }

        private void Start() {
            UpdatePosition();
            for(int i = 0; i < Inventory.Capacity; i++) {
                RectTransform slot = Instantiate(InventorySlotPrefab, InventoryUI);
                slot.anchoredPosition = new Vector3(LeftSidePadding + (Spacing * (i)), -TopSidePadding);
            }
        }
        private void UpdatePosition() { //u[pdates the position for the next time the object is created
            InventoryItemPrefab.transform.position = new Vector3(LeftSidePadding + (Spacing * (Inventory.SlotsFilled)), -TopSidePadding);
        }
        public void AddItem(ItemPickup item) {
            UpdatePosition();
            InventoryItemPrefab.gameObject.GetComponentInChildren<Image>().sprite = item.item.UiIcon;
            InventoryItemPrefab.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = item.item.Name;
            InventoryItems.Add(Instantiate(InventoryItemPrefab, InventoryUI));
        }

        public void ResetItems(string itemRemoved) {
            int i = 0;
            bool removedItem = false;
            RectTransform rToRemove = null;
            foreach(RectTransform r in InventoryItems) {
                if(r.gameObject.GetComponentInChildren<TextMeshProUGUI>().text == itemRemoved && !removedItem || itemRemoved == "all") {
                    removedItem = true;
                    rToRemove = r;
                    Destroy(r.gameObject);
                }
                else {
                    r.transform.localPosition = new Vector3(LeftSidePadding + (Spacing * (i)), -TopSidePadding, 0);
                    i++;
                }
            }
            InventoryItems.Remove(rToRemove);
            UpdatePosition();
        }

        public void MoveHightlight(int slot) {
            Highlight.anchoredPosition = new Vector3(LeftSidePadding + (Spacing * slot), -TopSidePadding -100, 0);
        }

    }
}