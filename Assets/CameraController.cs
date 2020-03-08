using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //get player object
    public GameObject player;
    //define camera offset
    public Vector3 offset;
    //object to look at?
    public Transform lookAt;
    private Camera cam;

    //set the min and max vertical direction
    public const float Y_ANGLE_MIN = -120.0f;
    public const float Y_ANGLE_MAX = 80.0f;
    //gives the camera's distance from the player
    public float distance = 5.0f;
    //sensitivity mulitplier
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;

    //stores the values of the mouse's position by coordinates
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void Start() 
    {
        //set cam as the main Camera
        cam = Camera.main;
    }

    private void Update()
    {
        //adds mouse positions to the current x and y multiplying them by the sensitivity
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += Input.GetAxis("Mouse Y") * (sensitivityY);
        //Clamps the Y value to the min and max
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

    }

    void LateUpdate() 
    {
        //the camera transform is supposed to be the player's plus the offset value
        this.transform.position = player.transform.position + offset;
        //new Vector3 with distance being subtracted from Z
        Vector3 dir = new Vector3(0, 10, -distance);
        //creates a rotation based on euler angles with the calculated y and x  
        Quaternion rotation = Quaternion.Euler(currentY + 200, currentX, 0);
        //camera position is set to the lookAt's position with multiplied by dir
        cam.transform.position = lookAt.position + rotation * dir;
        //uses the LookAt function to rotate and face the lookAt target (the player)
        cam.transform.LookAt(lookAt.position);
    }
} 

