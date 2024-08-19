using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] int healAmount = 50;
    [SerializeField] AudioClip chestSound;
    [SerializeField] PlayerHealth playerHealth;
    
    Animator animator;
    AudioSource audioSrc;

    [SerializeField] Animator healSrcAnimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interact"))
            if (collision.gameObject.tag == "Player")
            {
                animator.SetTrigger("Open");
                audioSrc.clip = chestSound;
                audioSrc.Play();

                StartCoroutine(WaitTheChest());     
            }
            
    }


    // Espera o baú terminar de abrir e heala o player
    // --> Para não misturar os sons
    IEnumerator WaitTheChest()
    {
        yield return new WaitUntil(() => !audioSrc.isPlaying);
        
        healSrcAnimator.SetTrigger("Heal");
        playerHealth.Heal(healAmount);
    }
}