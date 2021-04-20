using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // member variables;
    private const int maxPlayerHealth = 3;
    private int playerHealth = 3;
    private int playerScore = 0;
    private bool isAlive = true;
    private float timePassed = 0;
    private float timeDelay = 2f;

    private SpawnController spawns;

    private void Start() 
    {
        // set local references to game objects
        //gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        //playerHealthBar = GameObject.Find("Player").GetComponent<Health>();
        spawns = GameObject.Find("SpawnController").GetComponent<SpawnController>();
    }

    private void Update() 
    {
        //update mission time
        timePassed += Time.deltaTime;   
        //UpdateTimeGUI();

        // check player health
        if(playerHealth <= 0 && isAlive == true)
        {
            //PlayerDestroyed();
            isAlive = false;
            Debug.Log("Player has Died...");
        }
    }

    private void OnEnable()
    {
        //killed events
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;

        //damaged events
        //Enemy.PlayerDamagedEvent += OnPlayerDamagedEvent;
    }

    private void OnDisable()
    {
        //killed events
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;

        //damaged events
        //Enemy.PlayerDamagedEvent -= OnPlayerDamagedEvent;
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add score to player
        playerScore += 10;
        //UpdateScoreGUI();
        Debug.Log("Enemy killed...");
    }

    private void OnPlayerKilledEvent(Player player)
    {
        Invoke("PlayerDestroyed", timeDelay);
    }

    private void OnPlayerDamagedEvent(Enemy enemy) //enemy damage
    {
        //damage sound
        //FindObjectOfType<AudioManager>().Play("PlayerExplosion");

        //apply damage to player based on enemy damage value
        playerHealth--;
        //playerHealthBar.setHealth(playerHealth);
    }

    private void UpdateScoreGUI()
    {
        //scoreText.text = playerScore.ToString();
    }

    private void UpdateTimeGUI()
    {
        //timerText.text = ((int)timePassed).ToString();
    }

}
