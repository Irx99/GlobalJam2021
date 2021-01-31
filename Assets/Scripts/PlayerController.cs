using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerInput playerInput;
    public float velocityForward = 0.1f, velocityRight = 0.1f, gravityForce, jumpForce, mouseSensibility = 0.1f;
    public Camera playerCamera;

    public float angleCameraLimit = 80;

    public float hitDetectionRadious = 0.5f;
    public float hitDetectionDistance = 3f;

    public Transform pickablePosition;
    public float pickableTime;
    public AnimationCurve pickableCurve;

    public float launchForce = 25f;

    public float bufferJumpLenghtTime = 0.5f;

    public HandsAnimatorHandle handsAnimatorHandle;

    private float inputHorizontal, inputVertical;
    private Vector2 mousePosition;

    private GameObject objectPicked;
    private bool pickableNotCentered;
    private Rigidbody objectPickedRgbd;
    private RaycastHit hit;

    private float velocityY;
    private float bufferJumpTime;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput.actions["mouseClick"].started += OnCustomMouseClick;
        playerInput.actions["mouseSecondaryClick"].started += OnCustomSecondaryMouseClick;

        playerInput.actions["jump"].started += Jump;
    }

    private void OnDestroy()
    {
        playerInput.actions["mouseClick"].started -= OnCustomMouseClick;
        playerInput.actions["mouseSecondaryClick"].started -= OnCustomSecondaryMouseClick;

        playerInput.actions["jump"].started -= Jump;
    }

    private void Update()
    {
        inputHorizontal = playerInput.actions["inputHorizontal"].ReadValue<float>();
        inputVertical = playerInput.actions["inputVertical"].ReadValue<float>();

        mousePosition = playerInput.actions["mousePosition"].ReadValue<Vector2>();

        this.transform.Rotate(0, (mousePosition.x) * mouseSensibility, 0);

        if(
            !(Vector3.SignedAngle(playerCamera.transform.forward, this.transform.forward, this.transform.right) >  angleCameraLimit && -mousePosition.y < 0) &&
            !(Vector3.SignedAngle(playerCamera.transform.forward, this.transform.forward, this.transform.right) < -angleCameraLimit && -mousePosition.y > 0)
        )
        {
            playerCamera.transform.Rotate((-mousePosition.y) * mouseSensibility, 0, 0);
        }   

        if(characterController.isGrounded)
        {
            if (bufferJumpTime > Time.timeSinceLevelLoad)
            {
                velocityY = jumpForce;
                characterController.Move((this.transform.forward * inputVertical * velocityForward + this.transform.right * inputHorizontal * velocityRight + this.transform.up * velocityY) * Time.deltaTime);
            }
            else
            {
                characterController.SimpleMove((this.transform.forward * inputVertical * velocityForward + this.transform.right * inputHorizontal * velocityRight));
            }
        }
        else
        {
            velocityY -= gravityForce * Time.deltaTime;
            characterController.Move((this.transform.forward * inputVertical * velocityForward + this.transform.right * inputHorizontal * velocityRight + this.transform.up * velocityY) * Time.deltaTime);
        }
    }

    private void OnCustomMouseClick(InputAction.CallbackContext obj)
    {
        if(objectPicked != null)
        {
            LaunchPickable();
        }
        else
        {
            if(Physics.SphereCast(playerCamera.transform.position, hitDetectionRadious, playerCamera.transform.forward, out hit, hitDetectionDistance))
            {
                if(hit.transform.GetComponent<DestructibleObject>())
                {
                    handsAnimatorHandle.PlayAnimation(HandsAnimatorHandle.Anims.PUNCH);
                    hit.transform.GetComponent<DestructibleObject>().Destroy();
                }
                else if(hit.transform.GetComponent<PickableObject>())
                {
                    if(objectPicked == null)
                    {
                        handsAnimatorHandle.PlayAnimation(HandsAnimatorHandle.Anims.GRAB);

                        (objectPicked, pickableNotCentered) = hit.transform.GetComponent<PickableObject>().Pick();
                        objectPicked.transform.parent = pickablePosition.transform;
                        objectPicked.GetComponent<Rigidbody>().useGravity = false;
                        objectPicked.GetComponent<Rigidbody>().isKinematic = true;
                        StartCoroutine(MovePickable(objectPicked, pickablePosition, pickableNotCentered));
                    }
                }
            }
        }
    }

    private IEnumerator MovePickable(GameObject objectPicked, Transform pickablePosition, bool PickableNotCentered)
    {
        float startingTime = Time.time;
        Vector3 startingPosition = objectPicked.transform.position;

        while ((startingTime + pickableTime) > Time.time)
        {
            objectPicked.transform.position = Vector3.Lerp(startingPosition, pickablePosition.position + (pickableNotCentered ? (objectPicked.transform.position - objectPicked.GetComponent<Renderer>().bounds.center) : Vector3.zero), pickableCurve.Evaluate(1 - (((startingTime + pickableTime) - Time.time)) / pickableTime));
            yield return new WaitForEndOfFrame();
        }

        objectPicked.transform.position = pickablePosition.position + (pickableNotCentered ? (objectPicked.transform.position - objectPicked.GetComponent<Renderer>().bounds.center) : Vector3.zero);
    }

    private void OnCustomSecondaryMouseClick(InputAction.CallbackContext obj)
    {
        if (objectPicked != null)
        {
            LaunchPickable();
        }
    }

    private void LaunchPickable()
    {
        handsAnimatorHandle.PlayAnimation(HandsAnimatorHandle.Anims.THROW);

        objectPickedRgbd = objectPicked.GetComponent<Rigidbody>();
        objectPickedRgbd.useGravity = true;
        objectPickedRgbd.isKinematic = false;
        objectPicked.transform.parent = null;
        objectPickedRgbd.AddForce(playerCamera.transform.forward * launchForce, ForceMode.Impulse);
        objectPicked = null;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if(characterController.isGrounded)
        {
            velocityY = jumpForce;
            characterController.Move((this.transform.forward * inputVertical * velocityForward + this.transform.right * inputHorizontal * velocityRight + this.transform.up * velocityY) * Time.deltaTime);
        }
        else
        {
            bufferJumpTime = Time.timeSinceLevelLoad + bufferJumpLenghtTime;
        }        
    }
}
