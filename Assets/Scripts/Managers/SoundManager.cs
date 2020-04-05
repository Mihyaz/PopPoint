using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    /* I hate singletons but what's the best practice of sound managers ?! */

    public static SoundManager Instance;
    public AudioSource EfxSource;
    public AudioClip[] Clips;

    public Button AudioUnmuted;
    public Button AudioMuted;

    private SoundType _soundType;
    private bool toggle;

    public bool Toggle
    {
        get => toggle;
        set
        {
            toggle = value;
            AudioMuted.gameObject.SetActive(toggle);
            AudioUnmuted.gameObject.SetActive(!toggle);
            gameObject.SetActive(!toggle);
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _soundType = new SoundType(Clips);
    }

    public void PlaySingle(SoundTypes sound)
    {
        if(gameObject.activeInHierarchy)
        {
            EfxSource.clip = _soundType.Sounds[sound];
            EfxSource.PlayOneShot(EfxSource.clip);
        }
        
    }

    public void Mute()
    {
        Toggle = !Toggle;
    }

    public class SoundType
    {
        public Dictionary<SoundTypes, AudioClip> Sounds = new Dictionary<SoundTypes, AudioClip>();
        public SoundType(AudioClip[] Clips)
        {
            Sounds.Add(SoundTypes.Swing, Clips[0]);
            Sounds.Add(SoundTypes.GameOver, Clips[1]);
            Sounds.Add(SoundTypes.Score, Clips[2]);
            Sounds.Add(SoundTypes.Coin, Clips[3]);
            Sounds.Add(SoundTypes.Die, Clips[4]);
        }
    }
}
