using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleModel : MonoBehaviour
{
    //The speed of the obstacle
    public float speed;
    public Pooling pool;//The pooling manager
    public Transform endPoint;
    bool triggerMovement;
	
	// Update is called once per frame
	void Update ()
    {
        //With speed = 1, the obj will translate 1 Unity unit each second
		if(triggerMovement)
        {
            Vector3 pos = gameObject.transform.position;

            //If the obstacle is out of the screen
            if(pos.x <= endPoint.position.x)
            {
                pool.Despawn(gameObject);
                triggerMovement = false;
            }
            else
            {
                pos.x -= Time.deltaTime * speed;
                gameObject.transform.position = pos;
            }          
        }
	}

    //Call this function to set the obstacle auto-moving
    public void TriggerObstacleMovement()
    {
        triggerMovement = true;
    }
}
