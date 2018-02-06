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
        //BOY I SURE HOPE YOU DONT PLAN ON MOVING THIS TO UPDATE()
        //its fine on start (we can prolly dupe the function to an OnLoad() to assure this aswell)
		rbody = player.GetComponent<Rigidbody2D> ();
		AnimatorComponent = player.GetComponent<Animator> ();

    }
    
	//Basic ass movement idk if we actually want to stick with rigidbody/physics based movement
    void FixedUpdate()
    {
        walk();
        jump();
    }
    void Update()
    {
        //this is where we handle determining what the player does
        //the actual actions (physics based) are done in a fixed update
        horizontalMult = Input.GetAxisRaw("Horizontal");
        if(horizontalMult != 0)
        {
            //flip that sprite
           
            transform.localScale = new Vector3(horizontalMult, 1, 1);
            PlayerStates.Instance.DirectionFacing = (DirectionFacing)horizontalMult;
            if(rbody.velocity.y == 0)
            {
                AnimatorComponent.Play("CharacterRun");
            }
        }
        if(horizontalMult == 0)
        {
            AnimatorComponent.Play("CharacterIdl");
        }
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        if(rbody.velocity.y == 0)
        {
            PlayerStates.Instance.Vertical = Vertical.OnGround;
        }
        else
        {
            AnimatorComponent.Play("CharacterJump");
        }
    }
    private void MovementAbility()
    {
        //yeah this can be used with the action system to do stuff
        //i guess
    }
    //call these motions in the fixed update rather than have a messy Update
    private void walk()
    {
        rbody.velocity = new Vector2(horizontalMult * movementSpeed, rbody.velocity.y);
    }
    private void jump()
    {
        if (isJumping)
        {
            if (PlayerStates.Instance.Vertical == Vertical.OnGround)
            {
                PlayerStates.Instance.Vertical = Vertical.InAir;
                rbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
        }
        isJumping = false;
    }
}
