using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public int timeRequired;
    public string InteractText;
    public GameObject playerObj;
    public PlayerActionController player;
    public InteractType intType;
    public InteractProcess intProc;
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
