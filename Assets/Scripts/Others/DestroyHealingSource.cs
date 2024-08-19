using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHealingSource : MonoBehaviour
{
    [SerializeField] AudioClip healSound;
    AudioSource audioSrc;

    // Chamadas atravéz de evento de animação (animação do coração)
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
