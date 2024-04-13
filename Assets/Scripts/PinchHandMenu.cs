using System.Diagnostics; // For Stopwatch
using UnityEngine;
using UnityEngine.InputSystem;

public class PinchHandMenu : MonoBehaviour
{
    [Header("Input Action References")]
    public InputActionReference leftHandPinchAction;
    public InputActionReference rightHandPinchAction;

    public Transform leftHandMenuLocation;
    public Transform rightHandMenuLocation;

    [Header("Hand Menus")]
    public GameObject handMenu;

    private Stopwatch pinchTimer = new Stopwatch();
    private bool isLeftHandPinching = false;
    private bool isRightHandPinching = false;

    private void Awake()
    {
        leftHandPinchAction.action.Enable();
        rightHandPinchAction.action.Enable();
    }

    private void OnEnable()
    {
        leftHandPinchAction.action.performed += OnHandPinchPerformed;
        leftHandPinchAction.action.canceled += OnHandPinchCanceled;
        rightHandPinchAction.action.performed += OnHandPinchPerformed;
        rightHandPinchAction.action.canceled += OnHandPinchCanceled;
    }

    private void OnDisable()
    {
        leftHandPinchAction.action.performed -= OnHandPinchPerformed;
        leftHandPinchAction.action.canceled -= OnHandPinchCanceled;
        rightHandPinchAction.action.performed -= OnHandPinchPerformed;
        rightHandPinchAction.action.canceled -= OnHandPinchCanceled;

        leftHandPinchAction.action.Disable();
        rightHandPinchAction.action.Disable();
    }

    private void OnHandPinchPerformed(InputAction.CallbackContext context)
    {
        if (context.action == leftHandPinchAction.action)
        {
            isLeftHandPinching = true;
        }
        else if (context.action == rightHandPinchAction.action)
        {
            isRightHandPinching = true;
        }

        if (!pinchTimer.IsRunning)
        {
            pinchTimer.Restart();
        }
    }

    private void OnHandPinchCanceled(InputAction.CallbackContext context)
    {
        isLeftHandPinching = false;
        isRightHandPinching = false;

        // Reset and stop the timer when either hand's pinch is released
        pinchTimer.Reset();
        pinchTimer.Stop();
    }

    private void Update()
    {
        if (pinchTimer.IsRunning && pinchTimer.Elapsed.TotalSeconds >= 2)
        {
            pinchTimer.Stop(); // Stop the timer to prevent multiple activations

            if (isLeftHandPinching)
            {
                // activate hand menu
                handMenu.SetActive(true);
                //MoveMenuToHand(leftHandMenu, leftHandMenuLocation);
            }
            else if (isRightHandPinching)
            {
                // activate hand menu
                handMenu.SetActive(true);
                //MoveMenuToHand(rightHandMenu, rightHandMenuLocation);
            }
        }
    }

    // no longer in use because hand menu is set up to camera and follows head tracking
    private void MoveMenuToHand(GameObject handMenu, Transform handLocation)
    {
        handMenu.transform.position = handLocation.position;
        handMenu.SetActive(true); // Activate the menu at the new position
    }
}