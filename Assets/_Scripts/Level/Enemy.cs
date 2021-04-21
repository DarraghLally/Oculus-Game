using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // == delegate types used for event methods ==
    public delegate void EnemyKilled(Enemy enemy);

    // == static methods to be implemented by the event listner ==
    public static EnemyKilled EnemyKilledEvent;
    
    private void OnTriggerEnter(Collider whatHitMe) 
    {
        
        var bullet = whatHitMe.GetComponent<Bullet>();
        var axe = whatHitMe.GetComponent<Axe>();

        if (bullet)
        {
            // event to award points
            PublishEnemyKilledEvent();

            // play sound effects
            //FindObjectOfType<AudioManager>().Play("EnemyExplosion");

            // destroy enemy
            Destroy(gameObject);
        }

        if (axe)
        {
            // event to award points
            PublishEnemyKilledEvent();

            // play sound effects
            //FindObjectOfType<AudioManager>().Play("EnemyExplosion");

            // destroy enemy
            Destroy(gameObject);
        }
        
    }

    private void PublishEnemyKilledEvent()
    {
        if(EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
    }
}
