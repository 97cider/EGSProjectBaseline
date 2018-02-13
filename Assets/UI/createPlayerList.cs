using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class createPlayerList : MonoBehaviour {
    public GameObject players;
    public Button buttonPrefab;
    public ccPlayer i;
    public void Start() {
        List<ccPlayer> listPlayer = players.GetComponent<savePlayer>().players;
        foreach (ccPlayer i in listPlayer) {
            var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as Button;
            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            button.GetComponentInChildren<Text>().text = i.name;     
            }
        Debug.Log("FOR loop has finished and buttons should exist");
    }
}
