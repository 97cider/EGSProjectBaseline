using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour {
    //im guess the player??
    //for triggering changes and shiet
    public bool containsItem;
    public Item item;
    public EquipmentType equipmentType;
    public Inventory inventory;
    public Image icon;

	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
	}
    //this is where stuff is gonna change i guess
    //what im thinking is we have an event trigger here that gets triggered
    //when the "EquipItem" is called
}
