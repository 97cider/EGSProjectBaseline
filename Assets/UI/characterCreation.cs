using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterCreation : MonoBehaviour {
    public GameObject player;
    private  SpriteRenderer spriteChoice;
    private Sprite realSprite;
    public Sprite defaultSprite, paragonSprite;

    public void editSpriteChoiceOne() {
        spriteChoice = player.GetComponent<SpriteRenderer>();
        spriteChoice.sprite = paragonSprite;
        Debug.Log("Sprite changed to paragon");
    }

    public void editSpriteChoiceTwo() {
        spriteChoice = player.GetComponent<SpriteRenderer>();
        spriteChoice.sprite = defaultSprite;
        Debug.Log("Sprite Changed to default");
    }
}
