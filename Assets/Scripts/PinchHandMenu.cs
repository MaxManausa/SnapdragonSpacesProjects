using UnityEngine;
using UnityEngine.InputSystem;

public class PinchHandMenu : MonoBehaviour
{
    [Header("Input Action References")]
    public InputActionReference leftHandPinchAction;
    public InputActionReference rightHandPinchAction;

    public Transform leftHandMenuLocation;
    public Transform rightHandMenuLocation;

    [Header("Hand Menu")]
    public GameObject handMenu;

    private System.Diagnostics.Stopwatch pinchTimer; // Timer to track pinch duration
    private bool isLeftHandPinching = false;
    private bool isRightHandPinching = false;

    private void Awake()
    {
        leftHandPinchAction.action.Enable();
        rightHandPinchAction.action.Enable();

        pinchTimer = new System.Diagnostics.Stopwatch(); // Initialize the stopwatch
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

    private void OnHandPinchPerformed(InputAction.CallbackContext situation)
    {


        if (situation.action == leftHandPinchAction.action)
        {
            isLeftHandPinching = true;
            isRightHandPinching = false;
        }
        else if (situation.action == rightHandPinchAction.action)
        {
            isRightHandPinching = true;
            isLeftHandPinching = false;
        }
        pinchTimer.Restart(); // Start or restart the timer
    }

    private void OnHandPinchCanceled(InputAction.CallbackContext situation2)
    {

        isLeftHandPinching = false;
        isRightHandPinching = false;
        pinchTimer.Stop(); // Stop the timer
    }

    private void Update()
    {
        if (pinchTimer.IsRunning && pinchTimer.Elapsed.TotalSeconds > 3)
        {


            if (isLeftHandPinching)
            {
                MoveMenuToHand(leftHandMenuLocation);
            }
            else if (isRightHandPinching)
            {
                MoveMenuToHand(rightHandMenuLocation);
            }
            pinchTimer.Reset(); // Reset the timer to prevent multiple activations
        }
    }

    private void MoveMenuToHand(Transform handLocation)
    {
        handMenu.transform.position = handLocation.position;
        handMenu.SetActive(true); // Activate the menu at the new position
    }
}