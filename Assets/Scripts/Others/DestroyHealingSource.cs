using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHealingSource : MonoBehaviour
{
    [SerializeField] AudioClip healSound;
    AudioSource audioSrc;

    // Chamadas atrav�z de evento de anima��o (anima��o do cora��o)
    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }


    void HealSound()
    {
        audioSrc.clip = healSound;
        audioSrc.Play();
    }

    IEnumerator DestroyHeal()
    {
        yield return new WaitUntil(() => !audioSrc.isPlaying);
        Destroy(gameObject);
    }
}
