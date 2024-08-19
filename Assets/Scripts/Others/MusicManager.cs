using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [SerializeField] AudioSource audioSrc;

    public bool Muted {
        get { return audioSrc.mute; }
        set { audioSrc.mute = value; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);       
    }
}
