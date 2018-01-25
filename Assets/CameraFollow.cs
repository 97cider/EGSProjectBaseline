using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //will need to be refactored to include player instances
    public Transform target;
	public int depth;
    //so the player stays in the lower portion of the screen
    public float heightOffset, minY, maxY, minX, maxX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target != null)
        {
            //still needs a lot of work tee bee hahce
            //it still follows the player at any y change, which makes the camera look really wierd while jumping 
            //there needs to be a minimum change in y distance for a vertical change to occur
            //for example there should be instances where the character is at the top of the screen, and some where he is at the botton
            //top of a cliff, bottom of a pit, etc
            //maximize screenspace in relevancy to the direction the player must be going (ie artificially direct the player where to go by screen size)

			transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y + heightOffset, minY, maxY), depth), 0.1f);
        }
    }
}
