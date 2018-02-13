using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePlayer : MonoBehaviour {

    //Currently designed character
    public GameObject player;

    //List of all players created by user
    public List<ccPlayer> players;

    //Save the player to a serialized list of players.
    public void Save() {
        string playerName;
        Sprite spriteChoice;

        playerName = player.name;
        spriteChoice = player.GetComponent<SpriteRenderer>().sprite;

        ccPlayer cachedPlayer = new ccPlayer();
        cachedPlayer.chosenSprite = spriteChoice;
        cachedPlayer.name = playerName;
        players.Add(cachedPlayer);

    }

}
