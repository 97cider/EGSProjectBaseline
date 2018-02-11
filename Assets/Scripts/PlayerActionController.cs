using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour {
    public UiManager uimang;
    public GameObject worldSpaceText;
    private Vector3 worldPos;
    private Interactable oth;
    bool canInteract;
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
            Debug.Log("uwu");
            oth = other.GetComponent<Interactable>();
            canInteract = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            worldSpaceText.SetActive(false);
            canInteract = false;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if (worldSpaceText.activeSelf)
        {
            uimang.InteractUIMover(worldPos);
        }
        if (Input.GetKeyDown("e") && canInteract)
        {
            Debug.Log("OWO");
            oth.OnInteract();
        }
    }
}
