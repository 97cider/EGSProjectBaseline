using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour {
    public UiManager uimang;
    public GameObject worldSpaceText;
    private Vector3 worldPos;
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
            Debug.Log("Interacted object within range");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            worldSpaceText.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if (worldSpaceText.activeSelf)
        {
            uimang.InteractUIMover(worldPos);
        }
    }
}
