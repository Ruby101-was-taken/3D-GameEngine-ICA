using GD.Audio;
using GD.Toast;
using GD.Types;
using UnityEngine;

namespace GD.Examples
{
    public class MyGameStings : Singleton<MyGameStings>
    {
        [SerializeField]
        private AudioClip readyClip;

        /// <summary>
        /// Packages together the ready sting for the game.
        /// </summary>
        /// <example>
        /// MyGameStings.Instance.DoReadySting();
        /// </example>
        public void DoReadySting()
        {
            ToastManager.Instance.AddToast("Ready!!!", 3, 0);
            AudioManager.Instance.PlaySound(readyClip, AudioMixerGroupName.Voiceover);
            //CameraManager
            //EffectsManager
        }
    }
}