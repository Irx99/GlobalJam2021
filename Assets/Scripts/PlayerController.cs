using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerInput playerInput;
    public float velocityForward = 0.1f, velocityRight = 0.1f, mouseSensibility = 0.1f;
    public Camera playerCamera;

    public Vector3 hitDetectionHalfCube = new Vector3(0.5f, 0.5f, 0.5f);
    public float hitDetectionDistance = 3f;

    private float inputHorizontal, inputVertical;
    private Vector2 mousePosition;
    private RaycastHit hit;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput.actions["mouseClick"].started += OnCustomMouseClick;
    }

    private void OnDestroy()
    {
        playerInput.actions["mouseClick"].started -= OnCustomMouseClick;
    }

    private void Update()
    {
        inputHorizontal = playerInput.actions["inputHorizontal"].ReadValue<float>();
        inputVertical = playerInput.actions["inputVertical"].ReadValue<float>();

        mousePosition = playerInput.actions["mousePosition"].ReadValue<Vector2>();

        this.transform.Rotate(0, (mousePosition.x) * mouseSensibility, 0);

        if(
            !(Vector3.SignedAngle(playerCamera.transform.forward, this.transform.forward, this.transform.right) > +60 && -mousePosition.y < 0) &&
            !(Vector3.SignedAngle(playerCamera.transform.forward, this.transform.forward, this.transform.right) < -60 && -mousePosition.y > 0)
        )
        {
            playerCamera.transform.Rotate((-mousePosition.y) * mouseSensibility, 0, 0);
        }   
    }

    private void FixedUpdate()
    {
        characterController.SimpleMove(this.transform.forward * inputVertical * velocityForward + this.transform.right * inputHorizontal * velocityRight);
    }

    private void OnCustomMouseClick(InputAction.CallbackContext obj)
    {
        if(Physics.BoxCast(this.transform.position, hitDetectionHalfCube, this.transform.forward, out hit, this.transform.rotation, hitDetectionDistance))
        {
            if(hit.transform.GetComponent<DestructibleObject>())
            {
                hit.transform.GetComponent<DestructibleObject>().Destroy();
            }
        }
    }
}
