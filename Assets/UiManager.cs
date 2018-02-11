using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public GameObject worldSpaceText;
	// Use this for initialization
	void Start () {
		
	}
   public void InteractUIMover(Vector3 loc)
    {
        worldSpaceText.transform.position = Camera.main.WorldToScreenPoint(loc);
    }
	// Update is called once per frame
	void Update () {
	}
}
