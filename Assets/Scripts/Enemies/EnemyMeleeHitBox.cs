using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeHitBox : MonoBehaviour
{
    bool hit;
    public bool Hit {
        get { return hit; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Debug.Log("Acertou");
            hit = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            hit = false;
    }
}
