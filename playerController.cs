using UnityEngine;

public class PlayerController : Monobehavior
{
    void Update()
    {
        var xMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var yMove = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, xMove, 0);
        transform.Translate(0, 0, yMove);
    }
}