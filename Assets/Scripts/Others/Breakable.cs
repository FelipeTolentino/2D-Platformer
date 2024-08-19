using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] AudioClip breakingSound;
    [SerializeField] int goldAmount = 10;

    Animator animator;
    BoxCollider2D coll;
    AudioSource audioSrc;
    //MoneyCounter moneyCounter;

    bool broken = false;

    public bool Broken { 
        get { return broken; }
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            audioSrc.mute = true;
        else
            audioSrc.mute = false;
    }

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        //moneyCounter = GameObject.Find("/Money").GetComponent<MoneyCounter>();
    }

    public void Break()
    {
        if (!broken)
        {   
            audioSrc.clip = breakingSound;
            audioSrc.Play();
            animator.SetTrigger("Broken");

            //moneyCounter.catchGold(goldAmount);
            MoneyCounter.instance.catchGold(goldAmount);

            broken = true;
        }
    }


    //chamado por evento de animação

    IEnumerator Remove()
    {
        yield return new WaitUntil(() => !audioSrc.isPlaying);
        Destroy(gameObject);
    }

}
