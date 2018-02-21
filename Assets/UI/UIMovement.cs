using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour {
    //Really basic camera movement script to speed up UI (aka make me do less)
    public Camera sceneCam;
    public Transform MainCameraPosition;
    public Transform DesiredScenePosition;
    public float flySpeed = 1.0F;
    private float beginTime;
    private float lengthToTravel;
    public Vector3 i;
    public Animator animator;
    public void Start() {
        beginTime = Time.time;
        lengthToTravel = Vector3.Distance(MainCameraPosition.position, DesiredScenePosition.position);
    }
    public void cameraMovement()
    {
        Transform currentLocation = MainCameraPosition;
        float distance = (Time.time - beginTime) * flySpeed;
        float travelDiv = distance / lengthToTravel;
        while ((currentLocation.position != DesiredScenePosition.position) && (Mathf.Ceil(currentLocation.position.x) < Mathf.Ceil(DesiredScenePosition.position.x)))
        {
            Debug.Log(currentLocation.position.x + " : " + DesiredScenePosition.position.x);
            sceneCam.transform.position = Vector3.Lerp(currentLocation.position, DesiredScenePosition.position, flySpeed * Time.deltaTime);
            currentLocation.position = sceneCam.transform.position;
        }
    }
    public void playAnimation()
    {
        animator.Play("MoveCamera");
    }
    public void playMore()
    {
        animator.Play("MoveMore");
    }


}
