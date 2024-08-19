using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float cooldown = 1f;

    bool inCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !inCooldown)
            StartCoroutine(DamageAndCooldown(collision));

    }

    IEnumerator DamageAndCooldown(Collider2D colision)
    {
        inCooldown = true;
        colision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        yield return new WaitForSeconds(cooldown);
        inCooldown = false;
    }
}
