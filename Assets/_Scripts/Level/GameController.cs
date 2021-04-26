using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] public Text scoreBoard;
    // member variables;
    private const int maxPlayerHealth = 3;
    private int playerHealth = 3;
    private int playerScore = 0;
    private bool isAlive = true;
    private float timePassed = 0;
    private float timeDelay = 2f;

    private SpawnController spawns;
    
    //private ScoreUI scoreUI;

    private void Start() 
    {
        // set local references to game objects
        //scoreUI = GameObject.Find("Canvas").GetComponent<ScoreUI>();
        //playerHealthBar = GameObject.Find("Player").GetComponent<Health>();
        spawns = GameObject.Find("SpawnController").GetComponent<SpawnController>();
        FindObjectOfType<AudioManager>().Play("SceneMusic");
    }

    private void Update() 
    {
        //update mission time
        timePassed += Time.deltaTime;
        //UpdateTimeGUI();
        scoreBoard.text = "" + playerScore;

        // check player health
        if (playerHealth <= 0 && isAlive == true)
        {
            //PlayerDestroyed();
            isAlive = false;
            Debug.Log("Player has Died...");

            // restart menu
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        //killed events
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;

        //damaged events
        EnemyBehaviour.PlayerDamagedEvent += OnPlayerDamagedEvent;
    }

    private void OnDisable()
    {
        //killed events
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;

        //damaged events
        EnemyBehaviour.PlayerDamagedEvent -= OnPlayerDamagedEvent;
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add score to player
        
        playerScore += 10;
        //scoreBoard.text = playerScore.ToString();
        //UpdateScoreGUI();
        Debug.Log("Enemy killed...");
    }

    private void OnPlayerKillEvent(Player player)
    {
        Invoke("PlayerDestroyed", timeDelay);
    }

    private void OnPlayerDamagedEvent(EnemyBehaviour enemy) //enemy damage
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
