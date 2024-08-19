using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    [SerializeField] float lifeTime;
    [SerializeField] float explosionRange;
    [SerializeField] int damage;
    //[SerializeField] bool follow;
    [SerializeField] AudioClip explosionSound;

    Rigidbody2D body;
    CircleCollider2D circleColl;
    Animator animator;
    AudioSource audioSrc;
    SpriteRenderer sprite;
    Transform target;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        circleColl = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
         
    }


    private void Update()
    {
        if (Time.timeScale == 0)
            audioSrc.mute = true;
        else
            audioSrc.mute = false;

        float angle = Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("Hit");
        audioSrc.clip = explosionSound;
        audioSrc.Play();
        circleColl.enabled = false;
        if(collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
    }

    void EndOfLife()
    {
        Destroy(gameObject);
    }

    //FUNÇÕES CHAMADAS VIA EVENTO DE ANIMAÇÃO 

    void Explosion()
    {
        circleColl.enabled = false;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, explosionRange, playerLayer);
        if (collider != null && collider.gameObject.tag == "Player")
            collider.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        audioSrc.clip = explosionSound;
        audioSrc.Play();
    }

    void Invisible()
    {
        sprite.enabled = false;
    }

    //chamada atravez de evento de animação
    //utilizada em animações iniciais que loopam
    IEnumerator LifeTime(float life)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
