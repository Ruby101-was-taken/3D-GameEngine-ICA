using GD.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GD.Items
{
    /// <summary>
    /// Stores all data for an item used when the item is consumed or picked up
    /// </summary>
    /// <see cref="Item"/>
    /// <see cref="Inventory"/>
    /// <see cref="InventoryCollection"/>
    [CreateAssetMenu(fileName = "Answer 1", menuName = "RUB/Questions/Answer")]
    public class AnswerData : ScriptableGameObject
    {
        #region Fields

        AnswerData(string name) {
            Name = name;
        }

        [FoldoutGroup("UI & Sound", expanded: true)]
        [SerializeField]
        [PreviewField(100, ObjectFieldAlignment.Left)]
        [Tooltip("The sprite that represents this item in the UI")]
        private Sprite uiIcon;

        [FoldoutGroup("UI & Sound", expanded: true)]
        [SerializeField]
        [Tooltip("The audio clip that represents this item")]
        private AudioClip audioClip;

        [FoldoutGroup("UI & Sound")]
        [SerializeField]
        [Tooltip("The position of the audio source that plays the audio clip")]
        private Vector3 audioPosition;

        #endregion Fields

        #region Properties


        public Sprite UiIcon { get => uiIcon; set => uiIcon = value; }
        public AudioClip AudioClip { get => audioClip; set => audioClip = value; }
        public Vector3 AudioPosition { get => audioPosition; set => audioPosition = value; }

        #endregion Properties

    }
}