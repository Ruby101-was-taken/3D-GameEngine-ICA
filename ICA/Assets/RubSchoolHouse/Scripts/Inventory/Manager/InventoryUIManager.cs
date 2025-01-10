using GD.Pool;
using UnityEngine;

namespace RUB {
    public class InventoryUIManager : MonoBehaviour {
        [SerializeField]
        private Inventory Inventory;
        [SerializeField]
        private RectTransform InventoryItem;
        [SerializeField]
        private int InventorySize;

        private ObjectPool<RectTransform> InventoryPool;

        void Awake() {
            InventoryPool = new ObjectPool<RectTransform>(InventoryItem, InventorySize);
        }

        
    }
}