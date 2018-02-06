using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickCC : MonoBehaviour {

    public GameObject ccScreen, ccCreator;
	// Use this for initialization
    public void onClickChar() {
        ccScreen.gameObject.SetActive(false);
        Debug.Log("Clear UI");
    }
    public void onClickMaker() {
        ccCreator.gameObject.SetActive(true);
        Debug.Log("Create UI");
    }

}
