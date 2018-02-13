using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterCreation : MonoBehaviour {
    public GameObject player;
    public InputField nameInputField;
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

    public void editName() {
        player.name = nameInputField.text;
    }
}
