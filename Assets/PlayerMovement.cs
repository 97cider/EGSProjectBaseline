using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator AnimatorComponent;
    public float movementSpeed, jumpHeight, horizontalMult;
    public bool isJumping;
	public GameObject player;
	private Rigidbody2D rbody;

    // Use this for initialization
    void Start()
    {
        //UNITY WHAT THE FUCK
        //alright, well we know that the player will start is a specific spot for now, we will have to change this when saving/loading is introduced
        //oh god i hope this is serializable
        horizontalMult = 0;
        PlayerStates.Instance.Horizontal = Horizontal.idle;
        PlayerStates.Instance.Vertical = Vertical.InAir;
        PlayerStates.Instance.DirectionFacing = DirectionFacing.Right;
        PlayerStates.Instance.Action = Action.Idle;

		//TODO: Move to Update()
		rbody = player.GetComponent<Rigidbody2D> ();
		AnimatorComponent = player.GetComponent<Animator> ();
    }

	//Basic ass movement idk if we actually want to stick with rigidbody/physics based movement
    void FixedUpdate()
    {
		float h = Input.GetAxis ("Horizontal");
		Debug.Log (rbody.velocity);

		if (Mathf.Abs (rbody.velocity.magnitude) < 2.0f) {
			rbody.AddForce ((Vector2.right * movementSpeed) * h);
		}

		if (Input.GetAxis ("Jump") > 0.0f && !isJumping) {
			isJumping = true;	
			rbody.AddForce (Vector2.up * jumpHeight);
		}

		//This doesnt have to be what checks for 'done jumping' but it's here until we think of something better(collision check?)
		if(isJumping && rbody.velocity.y == 0){
			isJumping = false;
		}
    }
}
