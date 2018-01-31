using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class nonRigiMove : NetworkBehaviour
{
    public void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        transform.Translate(x, 0, 0);

    }
}