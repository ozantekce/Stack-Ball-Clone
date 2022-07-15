using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;


    void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private AudioSource audioSource;

    private bool sound = true;



    public void PlaySoundFX(AudioClip clip,float volume)
    {
        if (sound)
            audioSource.PlayOneShot(clip,volume);

    }



    #region GetterSetter
    public static SoundManager Instance { get => instance; set => instance = value; }
    public bool Sound { get => sound; set => sound = value; }

    #endregion
}
