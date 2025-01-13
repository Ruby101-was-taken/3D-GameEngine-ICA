using UnityEngine;
using UnityEngine.Audio;
using GD.Pool;
using GD.Types;
using Sirenix.OdinInspector;
using System.Collections;
using GD;
using GD.Audio;

/*
 * MUSIC MANAGER - STARTED BY AND WRITTEN BY RUBY FOR revolvium (Game Dev GCA -> https://github.com/JakeDaSpud/revolvium)
 * BAsed on the audio manager given in class - Found in the GD folder
 */
namespace RUB.Audio {
    public class MusicManager : Singleton<MusicManager> {
        [Title("AudioSource & Pool")]
        [SerializeField]
        [Tooltip("The prefab used to instantiate AudioSources.")]
        private AudioSource audioSourcePrefab;

        [SerializeField]
        [Tooltip("The initial size of the AudioSource pool.")]
        [ReadOnly]
        private int initialPoolSize = 3;

        [Title("Audio Mixer")]
        [SerializeField]
        [Tooltip("The AudioMixer used to control the audio groups.")]
        private AudioMixer audioMixer;


        [Title("Play Mode Music")]
        [SerializeField]
        AudioClip PlayModeMusicIntro;
        [SerializeField]
        AudioClip PlayModeMusic;


        private ObjectPool<AudioSource> audioSourcePool;

        protected override void Awake() {
            base.Awake();

            // Initialize the AudioSource pool
            audioSourcePool = new ObjectPool<AudioSource>(audioSourcePrefab,
                initialPoolSize);
        }


        private void Start() {
            StartCoroutine(StartSong(PlayModeMusicIntro, PlayModeMusic, AudioMixerGroupName.Background));
        }


        private IEnumerator StartSong(AudioClip songIntro, AudioClip fullSong, AudioMixerGroupName type) {
            AudioManager.Instance.PlaySound(songIntro, type);
            Debug.Log(songIntro.length);
            yield return new WaitForSeconds(songIntro.length);
            PlayMusic(fullSong, type);
        }


        public void PlayMusic(AudioClip clip, AudioMixerGroupName groupName = AudioMixerGroupName.Background, Vector3 position = default) {
            AudioSource audioSource = audioSourcePool.Get();
            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = AudioManager.Instance.GetAudioMixerGroup(AudioMixerGroupName.Background);
            audioSource.Play();

        }

    }
}