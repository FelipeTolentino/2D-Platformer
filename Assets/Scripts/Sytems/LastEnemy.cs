using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnemy : MonoBehaviour
{
    [SerializeField] EndScreen endScreen;
    [SerializeField] EnemyHealth enemyHealth;

    private void Update()
    {
        if (enemyHealth.Died)
            endScreen.CallEndScreen();
    }
}
