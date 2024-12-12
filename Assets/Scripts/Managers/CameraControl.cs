using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f; // How fast the camera moves

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalInput = 0f;  // For left/right movement (X-axis)
        float forwardBackwardInput = 0f; // For forward/backward movement (Z-axis)

        // Get left/right input (A/D keys) for horizontal movement (X-axis)
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; // Move left on X-axis
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // Move right on X-axis
        }

        // Get forward/backward input (W/S keys) for Z-axis movement
        if (Input.GetKey(KeyCode.W))
        {
            forwardBackwardInput = 1f; // Move forward on Z-axis
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forwardBackwardInput = -1f; // Move backward on Z-axis
        }

        // Move the camera on the X (side-to-side) and Z (forward/backward) axes based on input
        Vector3 movement = new Vector3(horizontalInput, 0f, forwardBackwardInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}

        /* [SerializeField] float keyboardInputSensitivity = 1f;
         [SerializeField] float mouseInputSensitivity = 1f;
         [SerializeField] bool continious = true;
         [SerializeField] Transform bottomLeft;
         [SerializeField] Transform topRight;

         Vector3 input;
         Vector3 pointOfOrigin;


         private void Update()
         {
             NullInput();

             MoveCameraInput();

             MoveCamera();
         }

         private void NullInput()
         {
             input.x = 0;
             input.y = 0;
             input.z = 0;
         }

         private void MoveCamera()
         {
             Vector3 position = transform.position;
             position += (input * Time.deltaTime);
             position.x = Mathf.Clamp(position.x, bottomLeft.position.x, topRight.position.x);
             position.z = Mathf.Clamp(position.z, bottomLeft.position.z, topRight.position.z);

             transform.position = position;
         }

         private void MoveCameraInput()
         {
             AxisInput();
             MouseInput();
         }

         private void MouseInput()
         {
             if (Input.GetMouseButtonDown(0)) { 
                 pointOfOrigin = Input.mousePosition;
             }
             if (Input.GetMouseButton(0)) {
                 Vector3 mouseInput = Input.mousePosition;
                 input.x += (mouseInput.x - pointOfOrigin.x) * mouseInputSensitivity;
                 input.z += (mouseInput.y - pointOfOrigin.y) * mouseInputSensitivity;
                 if (continious == false) {
                     pointOfOrigin = mouseInput;
                 }
             }
         }

         private void AxisInput()
         {
             input.x += Input.GetAxisRaw("Horizontal") *keyboardInputSensitivity;
             input.z += Input.GetAxisRaw("Vertical") * keyboardInputSensitivity;
         }*/
    
