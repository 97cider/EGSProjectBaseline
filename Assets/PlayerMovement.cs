using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator AnimatorComponent;
    public float movementSpeed, jumpHeight, horizontalMult;
    public bool isJumping;
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
    }

    void FixedUpdate()
    {
        //handle walking
        //handle jumping 
    }
    // Update is called once per frame
    void Update()
    {

    }
}
