using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour {
    public float interactTime = 0.0f;
    public UiManager uimang;
    public GameObject worldSpaceText;
    private Vector3 worldPos;
    private Interactable oth;
    private float goalTime;
    bool canInteract;
    bool requiresTime;
	// Use this for initialization
	void Start ()
    {
        worldSpaceText.SetActive(false);
    }
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            worldSpaceText.SetActive(true);
            worldPos = other.transform.position + new Vector3(0f, 0.2f, 0f);
            oth = other.GetComponent<Interactable>();
            uimang.OverrideText(oth.InteractText, oth.intType, oth.intProc);
            if((goalTime = oth.timeRequired) > 0)
            {
                requiresTime = true;
            }
            canInteract = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            worldSpaceText.SetActive(false);
            canInteract = false;
            goalTime = 0;
            requiresTime = false;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if (worldSpaceText.activeSelf)
        {
            uimang.InteractUIMover(worldPos);
        }
        if (Input.GetKeyDown("e") && canInteract && !requiresTime)
        {
            Debug.Log("OWO");
            oth.OnInteract();
        }
        else if (Input.GetKey("e") && canInteract && requiresTime)
        {
            interactTime += Time.deltaTime;
            uimang.UpdateProgessBar(interactTime, goalTime);
            if (interactTime > goalTime)
            {
                uimang.RestartProgressBar();
                oth.OnInteract();
            }
        }
        else
        {
            interactTime = 0;
            uimang.RestartProgressBar();
        }
    }
}
