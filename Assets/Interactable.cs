using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public GameObject playerObj;
    public PlayerActionController player;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("PlayerCC");
        player = playerObj.GetComponent<PlayerActionController>();
    }
	public virtual string OnInteract()
    {
        return "";
    }
}
