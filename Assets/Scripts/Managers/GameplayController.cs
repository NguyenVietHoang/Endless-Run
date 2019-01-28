using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    [Header("Manager")]
    public SoundController sound;
    public MainView view;

    [Header ("Gameplay parameter")]
    public Pooling obstaclePool;
    public Pooling birdPool;
    public int obstacleNB = 10;
    public Transform endPoint;
    public float scrollingSpd = 0.5f;//Better is (1/obstacleSpd), like frequency formular
    public float obstacleSpd = 2;

    //Private parameter
    int tutorial = 0;
    bool gamePaused;
    bool gameStarted;
	// Use this for initialization
	void Start ()
    {
        gamePaused = false;
        gameStarted = false;
        view.Init();

        //Initialize the pool
        obstaclePool.OnPoolingReady += OnObstaclePoolingFinished;
        birdPool.OnPoolingReady += OnBirdPoolingFinished;

        StartCoroutine(obstaclePool.Preload(obstacleNB));
        StartCoroutine(birdPool.Preload(obstacleNB));
    }
	
    void OnObstaclePoolingFinished()
    {
        //Set the obstacle parameter after instantiate it.
        foreach(GameObject go in obstaclePool.objs)
        {
            ObstacleModel obs = go.GetComponent<ObstacleModel>();
            obs.pool = obstaclePool;
            obs.endPoint = endPoint;
            obs.speed = obstacleSpd;
        }
        Debug.Log("Finish the obstacle pooling");
        tutorial++;
    }

    void OnBirdPoolingFinished()
    {
        //Set the obstacle parameter after instantiate it.
        foreach (GameObject go in birdPool.objs)
        {
            ObstacleModel obs = go.GetComponent<ObstacleModel>();
            obs.pool = birdPool;
            obs.endPoint = endPoint;
            obs.speed = obstacleSpd;
        }
        Debug.Log("Finish the bird pooling");
        tutorial++;
    }

    void OnPoolingFinished()
    {
        //If there's still a pool was not finished the preload
        if (tutorial < 2)
            return;

        //Wait for 4*scrollingSpd for the tutorial of obstacle
        Invoke("InvokeObstacle", 4 * scrollingSpd);
        //Wait for 8*scrollingSpd for the tutorial of bird
        Invoke("InvokeObstacle", 16 * scrollingSpd);
    }

    //Spawn the Obstacle, then set a new delay to spawn the next obstacle
    void InvokeObstacle()
    {
        //If the player passed the tutorial
        if(tutorial <= 0)
        {
            int prob = Random.Range(1, 10);
            //
Debug.Log("Prob: " + prob);
            //30% invoke the bird, 70% invoke an obstacle
            if (prob < 3)
            {
                GameObject tmp = birdPool.Spawn();
                tmp.GetComponent<ObstacleModel>().TriggerObstacleMovement();
            }
            else
            {
                GameObject tmp = obstaclePool.Spawn();
                tmp.GetComponent<ObstacleModel>().TriggerObstacleMovement();
            }

            //Spawn obstacle after 'rand*scrollingSpd' second
            int rand = Random.Range(2, 4);
            Invoke("InvokeObstacle", rand * scrollingSpd);
        }        
        else
        {
            if (tutorial == 2)
            {
                GameObject tmp = obstaclePool.Spawn();
                tmp.GetComponent<ObstacleModel>().TriggerObstacleMovement();
                tutorial--;
            }
            else
            {
                GameObject tmp = birdPool.Spawn();
                tmp.GetComponent<ObstacleModel>().TriggerObstacleMovement();

                //The player have finished the tutorial, now spawn randomly the obstacle
                Invoke("InvokeObstacle", 4 * scrollingSpd);
                tutorial--;
            }
        }
    }

    //Close the Application
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;       
        gamePaused = true;
        view.ToPauseTab();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        view.ToMainUITab();
    }

    //Event when the player press Start Button
    public void StartGame()
    {
        //Only start the game when all the resources were loaded
        if(GameReady() && tutorial >= 2)
        {
            view.ToMainUITab();
            OnPoolingFinished();
            gameStarted = true;

            //Start tutorial
            view.StartTutorial();
        }
    }

    //Event when the game is over
    public void StopGame()
    {
        gameStarted = false;
        view.ToGameOverTab();
    }
    //Reload the actual scene = Reset the Game
    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Check if the game is ready to start (all the resources were loaded)
    public bool GameReady()
    {
        if (obstaclePool.PoolingReady && birdPool.PoolingReady)
            return true;
        else
            return false;
    }

    private void Update()
    {
        //If the player want to pause, just tap ESC key
        if(!gamePaused && gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}
