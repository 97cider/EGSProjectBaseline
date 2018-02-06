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
        horizontalMult = 8f;

        PlayerStates.Instance.Horizontal = Horizontal.idle;
        PlayerStates.Instance.Vertical = Vertical.OnGround;
        PlayerStates.Instance.DirectionFacing = DirectionFacing.Right;
        PlayerStates.Instance.Action = PlayerAction.Idle;

		//TODO: Move to Update()(hehe funi mem)
		rbody = player.GetComponent<Rigidbody2D> ();
		AnimatorComponent = player.GetComponent<Animator> ();

    }

	//Basic ass movement idk if we actually want to stick with rigidbody/physics based movement
    void FixedUpdate()
    {
		float h = Input.GetAxis("Horizontal");
		//Debug.Log (h);

		//Add force, which accelerates and gibs velocity
		if (Mathf.Abs (rbody.velocity.magnitude) < movementSpeed)
        {
			rbody.AddForce ((Vector2.right * horizontalMult) * h);
		}

        //Basic directional triggers
        if (h < 0.0f)
        {
            PlayerStates.Instance.DirectionFacing = DirectionFacing.Left;
        }
        else if (h > 0.0f){
            PlayerStates.Instance.DirectionFacing = DirectionFacing.Right;
        }
        //Check velocity for going left, right or idle
        if (rbody.velocity.x < 0.0f)
        {
            PlayerStates.Instance.Horizontal = Horizontal.mLeft;
        }
        else if (rbody.velocity.x > 0.0f)
        {
            PlayerStates.Instance.Horizontal = Horizontal.mRight;
        }
        else
            PlayerStates.Instance.Horizontal = Horizontal.idle;

		//Check if we in the air or not
		if (Input.GetAxis ("Jump") > 0.0f && PlayerStates.Instance.Vertical != Vertical.InAir) {
			rbody.AddForce (Vector2.up * jumpHeight);
		}

		//This doesnt have to be what checks for 'done jumping' but it's here until we think of something better(collision check?)
		//In fact, we have to check collision because of the grappling hook and climbing
		if (rbody.velocity.y != 0) {
			PlayerStates.Instance.Vertical = Vertical.InAir; 
		} else {
			PlayerStates.Instance.Vertical = Vertical.OnGround;
		}
    }
}
