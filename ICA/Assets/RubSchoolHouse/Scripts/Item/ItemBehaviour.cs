using GD.Items;
using RUB.Events;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace RUB {
    public class ItemBehaviour : MonoBehaviour {

        [SerializeField]
        private AnswerData itemInfo;

        [SerializeField, FoldoutGroup("Visuals")]
        private SpriteRenderer itemRenderer;
        [SerializeField, FoldoutGroup("Visuals")]
        private TextMeshPro textRenderer;

        [SerializeField, Title("GameEvents")]
        private ItemPickupGameEvent itemEvent;

        private bool canAddItem = true;

        private float startTime = 0;

        public bool CanAddItem { get => canAddItem; set => canAddItem = value; }

        private void Start() {
            CanAddItem = true;
            itemRenderer.sprite = itemInfo.UiIcon;
            textRenderer.text = itemInfo.Name;
            startTime = Time.time;
        }

        public void UpdateItem(AnswerData newAnswer) {
            itemInfo = newAnswer;
            itemRenderer.sprite = itemInfo.UiIcon;
            textRenderer.text = itemInfo.Name;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player") && canAddItem && Time.time - startTime > 1) {
                itemEvent?.Raise(new ItemPickup(itemInfo, 1));

                Destroy(gameObject);
            }
        }
    }
}
