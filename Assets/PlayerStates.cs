using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {
    //a class used to store a collection of states used by the player
    //hopefully this makes it more of an instanced scenario, which
    //might make Co-Op easier down the road

    //I HOPE PLEASE MAKE CO OP EASY I DONT WANT TO REDO THIS AGAIn
    // Use this for initialization
    private static PlayerStates _instanced;
    public static PlayerStates Instance
    {
        get
        {
            if (_instanced == null)
            {
                _instanced = new GameObject("StateManager").AddComponent<PlayerStates>();
            }
            return _instanced;
        }
    }
    //State storage
    //im guessing an enum is the best way to go
    //based on basic 2D movement systems, it would be great to help tie 
    //the action system here as well
    public Horizontal Horizontal;
    public Vertical Vertical;
    public DirectionFacing DirectionFacing;
    public Action Action;
}
    public enum Horizontal
    {
        idle = 0,
        mRight = 1,
        mLeft = -1
    }
    //states for the players potential vetical movement
    //climbing and hooked should be the same, but whatever
    public enum Vertical
    {
        OnGround,
        InAir,
        Climbing,
        Hooked
    }
    //basic direction facing
    public enum DirectionFacing
    {
        Right = 1,
        Left = -1
    }
    //i wanted to make one state for attacking, but it seems like
    //we can do some cool stuff with light and heavy. For example,
    //multiple attacks per weapon are confirmed, but maybe to keep the 
    //game balanced, a heavy attack can stop all momentum of the player

    //interacting is prolly just going to be looting and gathering materials
    public enum Action
    {
        Idle,
        LightAttack,
        HeavyAttack,
        Interacting,
        Dialouge
    }
