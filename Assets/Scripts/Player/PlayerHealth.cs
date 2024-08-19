using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameOverScreen gameOver;

    [SerializeField] int maxHealth;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] float hurtSoundCooldown = 0.5f;
    [SerializeField] AudioClip deathSound;

    int currentHealth;
    bool died = false;
    bool hit = false;
    Animator animator;
    Rigidbody2D body;
    AudioSource audioSrc;
    [SerializeField]
    PlayerControls pC;

    public bool Died { 
        get { return died; }
    }
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.setMaxHealthBar(maxHealth);

        //*******************************************
        died = false;
        //*******************************************
    }

    public void takeDamage(int damage)
    {
        if (!died)
        {
            animator.SetTrigger("Hit");

            if (!hit)
            {
                audioSrc.clip = hurtSound;
                audioSrc.Play();
            }
            StartCoroutine(DontSpamSound());
            
            currentHealth -= damage;

            healthBar.setHealthBar(currentHealth);

            if (currentHealth <= 0)
                Die();
        }
    }

    public void Heal(int health)
    {
        if (currentHealth + health <= maxHealth)
        {
            currentHealth += health;
        }
        else
            currentHealth = maxHealth;
        
        healthBar.setHealthBar(currentHealth);
    }

    void Die()
    {
        
        died = true;
        Debug.Log("Definindo died = " + died);

        body.constraints = RigidbodyConstraints2D.FreezePositionX;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        animator.SetBool("Died", true);
        audioSrc.clip = deathSound;
        audioSrc.Play();
        pC.audioSrcPasso.Stop();
       
        GetComponent<PlayerControls>().enabled = false;

        this.enabled = false;
        
        Debug.Log("Chamando Gamer Over");
        gameOver.CallGameOverScreen();
        Debug.Log("Chamei Gamer Over");
    }

    IEnumerator DontSpamSound()
    {
        hit = true;
        yield return new WaitForSeconds(hurtSoundCooldown);
        hit = false;
    }

}
