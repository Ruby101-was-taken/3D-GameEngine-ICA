using GD.Items;
using RUB.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RUB {
    public class ItemBehaviour : MonoBehaviour {

        [SerializeField]
        private ItemData itemInfo;

        [SerializeField]
        private SpriteRenderer itemRenderer;

        [SerializeField, Title("GameEvents")]
        private ItemPickupGameEvent itemEvent;

        private void Start() {
            itemRenderer.sprite = itemInfo.UiIcon;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                itemEvent?.Raise(new ItemPickup(itemInfo, 1));

                DestroyObject(gameObject);
            }
        }
    }
}
