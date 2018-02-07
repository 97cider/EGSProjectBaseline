using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickCC : MonoBehaviour {

    public GameObject ccScreen, ccCreator, ccOptions;
	// Use this for initialization
    public void onClickChar() {
        ccOptions.gameObject.SetActive(false);
        ccScreen.gameObject.SetActive(true);
        Debug.Log("Clear UI");
    }
    public void onClickMaker() {
        ccCreator.gameObject.SetActive(true);
        Debug.Log("Create UI"); 
    }
    public void onClickOptions() {
        ccOptions.gameObject.SetActive(false);
        ccCreator.gameObject.SetActive(true);
        Debug.Log("Switch to Options");
    }

}
