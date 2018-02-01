using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class isGrounded : NetworkBehaviour
{
    public GameObject PLAYER;
    private Rigidbody2D rbody;
    public float jumpHeight;
    bool canJump;

    void Start()
    {
        rbody = PLAYER.GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (col.transform.tag == "Ground")
        {
            canJump = true;
            Debug.Log("Update: The Player should be able to jump");
        }
    }

    public void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (canJump)
            {
                Debug.Log("Update: The Player should be able to jump");
                rbody.AddForce(Vector2.up * jumpHeight);
                canJump = false;
            }
        }
    }
}