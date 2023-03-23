using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mangerContChar : MonoBehaviour
{
    //componet
    private CharacterController _charController;
    private managerJoystick _mngrJoyStick;
    private managerJoystick joystick;

    // move 
    private float inputX;
    private float inputZ;
    private float moveSpeed;


    private Vector3 v_movement;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.1f;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        _mngrJoyStick = GameObject.Find("imgJoystickBg").GetComponent<managerJoystick>();
        joystick = GameObject.FindObjectOfType<managerJoystick>();

    }

    // Update is called once per frame
    void Update()
    {
        //inputX = Input.GetAxis("Horizontal");
        //inputZ = Input.GetAxis("Vertical");
        inputX = _mngrJoyStick.inputHorizontal();
        inputZ = _mngrJoyStick.inputVertical();

    }

    private void FixedUpdate()
    {
        v_movement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
        _charController.Move(v_movement);
    }
}
