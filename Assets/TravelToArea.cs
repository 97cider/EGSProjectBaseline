using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TravelToArea : Interactable {
    //this is where we can prolly do some cool stuff, im just going to have a fade to black, enable and teleport
    //also i guess we can do something with the environment light
    public Image blackOverlay;
    public float fadeSpeed;
    public Transform telePortLocation;
    void Start()
    {
        fadeFromBlack();
    }
	public override string OnInteract()
    {
        //we can prolly call a load here or something down the road
        fadeFromBlack();
        playerObj.transform.position = telePortLocation.position;
        return "";
    }
    private void fadeToBlack()
    {
        blackOverlay.color = Color.Lerp(blackOverlay.color, Color.black, 90.0f * Time.deltaTime);
    }
    private void fadeFromBlack()
    {
        blackOverlay.color = Color.Lerp(blackOverlay.color, Color.clear, 90.0f * Time.deltaTime);
    }
}
