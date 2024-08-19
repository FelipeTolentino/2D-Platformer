using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hurtSound;

    Animator animator;
    AudioSource audioSrc;

    int currentHealth;

    bool died = false;

    public bool Died { 
        get { return died; }
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            audioSrc.mute = true;
        else
            audioSrc.mute = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        if (died) return;

        audioSrc.clip = hurtSound;
        audioSrc.Play();

        animator.SetTrigger("Hit");
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        died = true;

        audioSrc.clip = deathSound;
        audioSrc.Play();
        
        animator.SetBool("Died", true);

        //a função Disable desativa os componentes atravez
        //de eventos de animação
    }

    void Disable()
    {
        animator.enabled = false;
    }


}
