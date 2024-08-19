using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float minTimeBetween;
    [SerializeField] float maxTimeBetween;

    AudioSource audioSrc;
    EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            audioSrc.mute = true;
        else
            audioSrc.mute = false;

        if (!audioSrc.isPlaying && !enemyHealth.Died)
        {
            audioSrc.clip = clips[Random.Range(0, clips.Length - 1)];
            audioSrc.PlayDelayed(Random.Range(minTimeBetween, maxTimeBetween));
        }
    }
}
