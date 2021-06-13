using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct SchedulingEvents
    {
        public UnityEvent OnStartup;
        public UnityEvent OnDestroy;
    }

    [System.Serializable]
    public struct RandomAudioPack
    {
        public List<AudioClip> AudioClips;
        [SerializeField]
        private float RandomSoundDelayMin;
        [SerializeField]
        private float RandomSoundDelayMax;
        public float SoundDelay { get { return Random.Range(RandomSoundDelayMin, RandomSoundDelayMax); } }
        public bool CanPlaySound { get; set; }

        public void PlayRandomAudio(Vector3 location)
        {
            if (CanPlaySound)
            {
                CanPlaySound = false;
                AudioSource.PlayClipAtPoint(AudioClips[Random.Range(0, AudioClips.Count)], location);
            }
        }
    }

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (!instance)
                {
                    instance = new AudioManager();
                }
            }
            return instance;
        }
    }

    [Header("AUDIO")]
    // [SerializeField]
    // private AudioSource playerAudioSource;
    [SerializeField]
    private SchedulingEvents events;

    [Header("AUDIO- Random")]
    [SerializeField]
    private RandomAudioPack random;

    public void Start()
    {
        events.OnStartup?.Invoke();
        //if (playerAudioSource)
        //{
        //    playerAudioSource = FindObjectOfType<AudioSource>();
        //}


        //if (!playerAudioSource)
        //{
        //    Debug.LogError("No audio source given to " + this);
        //    Debug.Break();
        //}

        random.CanPlaySound = true;
    }

    public void OnDestroy()
    {
        events.OnDestroy?.Invoke();
    }

    //public void PlayLoopSound(AudioClip sound)
    //{
    //    playerAudioSource.clip = sound;
    //    playerAudioSource.loop = true;
    //    playerAudioSource?.Play();
    //}

    //public void StopLoopSound()
    //{
    //    if (playerAudioSource)
    //    {
    //        playerAudioSource.loop = false;
    //        playerAudioSource?.Stop();
    //    }
    //}

    public void PlayRandomAudio(Vector3 location)
    {
        if (random.CanPlaySound)
        {
            random.PlayRandomAudio(location);
            StartCoroutine(DyingPlaySoundDelay_Coroutine());
        }
    }

    IEnumerator DyingPlaySoundDelay_Coroutine()
    {
        yield return new WaitForSeconds(random.SoundDelay);
        random.CanPlaySound = true;
    }
}
