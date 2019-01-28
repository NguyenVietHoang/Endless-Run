using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Manager")]
    public GameplayController manager;
    public MainView view;

    [Header ("Public Param")]
    public GameObject root;    
    public Animator anim;

    [HideInInspector]
    public bool isJumping;//for Groundcheck event Calling

    ///private Param
    int score;

    // Use this for initialization
    void Start ()
    {
        isJumping = false;
        score = 0;
    }
	
	// Update is called once per frame
    //Update for Key event trigger
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            //Debug.Log("Up Key");
            anim.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isJumping)
        {
            //Debug.Log("Down Key");
            anim.SetTrigger("Crounch");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle")
        {
            manager.sound.PlayScreamSound();
            GameOver();
            //Debug.Log("Game over");
        }
        if(collision.tag == "CheckPoint")
        {
            manager.sound.PlayCoinSound();
            score += 10;
            view.UpdateMainUIScore(score);
            //Debug.Log("Score: " + score);
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        view.UpdateMainUIScore(score);
        manager.StopGame();

        //Attempt to Call SendScore to server here
        //StartCoroutine(UploadData());
    }
}
