using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUseEffect : MonoBehaviour {

    //something to identify the item
    public PlayerStats player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    //now we can just inherit from this and make items have use effects!
    public virtual string Use()
    {
        return "";
    }
}
