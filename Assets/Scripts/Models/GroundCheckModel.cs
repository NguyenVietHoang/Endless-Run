using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckModel : MonoBehaviour
{
    public PlayerController player;

	// Use this for initialization
	void Start ()
    {

    }

    ///Detect if the marker is collided with the ground or not-----------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            player.isJumping = false;
            player.anim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            player.isJumping = true;
            player.anim.SetBool("isJumping", true);
        }
    }
}
