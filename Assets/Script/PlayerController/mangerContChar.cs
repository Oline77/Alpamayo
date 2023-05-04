using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mangerContChar : MonoBehaviour
{
    // Components
    private CharacterController _charController; // R�f�rence au CharacterController attach� au GameObject
    private managerJoystick _mngrJoyStick; // R�f�rence au script managerJoystick attach� � l'objet imgJoystickBg

    // Movement variables
    private float inputX; // Valeur de l'axe horizontal
    private float inputZ; // Valeur de l'axe vertical
    private float moveSpeed; // Vitesse de d�placement
    private Vector3 v_movement; // Vecteur de mouvement

    void Start()
    {
        // Récupération des composants
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        _charController = tempPlayer.GetComponent<CharacterController>();
        _mngrJoyStick = GameObject.Find("imgJoystickBg").GetComponent<managerJoystick>();
        moveSpeed = 0.1f;
    }

    void Update()
    {
        // Récupération des valeurs de l'axe horizontal et vertical à partir du joystick
        inputX = _mngrJoyStick.inputHorizontal();
        inputZ = _mngrJoyStick.inputVertical();
    }

    private void FixedUpdate()
    {
        // Déplacement du personnage
        v_movement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
        _charController.Move(v_movement);

        // Rotation du personnage en direction du mouvement
        if (v_movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(v_movement);
            _charController.transform.rotation = Quaternion.Lerp(_charController.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}