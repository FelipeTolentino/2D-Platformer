using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float coolDown = 2f;
    [SerializeField] Sprite usedSprite;
    [SerializeField] AudioClip[] sounds;

    SpriteRenderer sprRenderer;
    AudioSource audioSrc;
    PlayerHealth player;

    bool inCooldown = false;
    bool isInside = false;

    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            audioSrc.mute = true;
        else
            audioSrc.mute = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isInside)
        {
            sprRenderer.sprite = usedSprite;
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            PlaySound();
        }
        isInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isInside = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !inCooldown)
            StartCoroutine(DamageCooldown(collision));
    }



    IEnumerator DamageCooldown(Collider2D collision)
    {
        inCooldown = true;
        yield return new WaitForSeconds(coolDown);
        if (isInside)
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            PlaySound();
        }
            
        inCooldown = false;
    }

    void PlaySound()
    {
        if (!player.Died)
        {
            int index = Random.Range(0, sounds.Length);
            audioSrc.PlayOneShot(sounds[index]);
        }
        
    }
}
