using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // == delegate types used for event methods ==
    public delegate void EnemyKilled(Enemy enemy);
    //public delegate void PlayerDamaged(Enemy enemy);

    // == static methods to be implemented by the event listner ==
    public static EnemyKilled EnemyKilledEvent;
    //public static PlayerDamaged PlayerDamagedEvent;

    private void OnTriggerEnter(Collider whatHitMe) 
    {
        
        var bullet = whatHitMe.GetComponent<Bullet>();
        var axe = whatHitMe.GetComponent<Axe>();
        //var player = whatHitMe.GetComponent<Player>();

        if (bullet || axe)
        {
            // event to award points
            PublishEnemyKilledEvent();

            // play sound effects
            FindObjectOfType<AudioManager>().Play("ZombieDead");

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
