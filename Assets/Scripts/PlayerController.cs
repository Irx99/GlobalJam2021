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

    private float inputHorizontal, inputVertical;
    private Vector2 mousePosition;

    private void Update()
    {
        inputHorizontal = playerInput.actions["inputHorizontal"].ReadValue<float>();
        inputVertical = playerInput.actions["inputVertical"].ReadValue<float>();

        mousePosition = playerInput.actions["mousePosition"].ReadValue<Vector2>();

        this.transform.Rotate(0, (mousePosition.x) * mouseSensibility, 0);

        Debug.Log(Vector3.SignedAngle(playerCamera.transform.forward, this.transform.forward, this.transform.right));

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
}
