using UnityEngine;
using UnityEngine.InputSystem;
// Include the namespace that contains XRHandTrackingManager
using QCHT.Interactions.Core;

public class SpellCastingAura : MonoBehaviour
{
    [Header("Input Action References")]
    public InputActionReference leftHandGrabAction;
    public InputActionReference rightHandGrabAction;

    [Header("Aura Objects")]
    public GameObject R_Aura;
    public GameObject L_Aura;

    // Reference to the XRHandTrackingManager component
    public XRHandTrackingManager handTrackingManager;

    private void Awake()
    {
        leftHandGrabAction.action.Enable();
        rightHandGrabAction.action.Enable();
    }

    private void OnEnable()
    {
        leftHandGrabAction.action.performed += OnHandGrabPerformed;
        leftHandGrabAction.action.canceled += OnHandGrabCanceled;
        rightHandGrabAction.action.performed += OnHandGrabPerformed;
        rightHandGrabAction.action.canceled += OnHandGrabCanceled;
    }

    private void OnDisable()
    {
        leftHandGrabAction.action.performed -= OnHandGrabPerformed;
        leftHandGrabAction.action.canceled -= OnHandGrabCanceled;
        rightHandGrabAction.action.performed -= OnHandGrabPerformed;
        rightHandGrabAction.action.canceled -= OnHandGrabCanceled;

        leftHandGrabAction.action.Disable();
        rightHandGrabAction.action.Disable();
    }

    private void OnHandGrabPerformed(InputAction.CallbackContext context)
    {
        if (context.action == leftHandGrabAction.action)
        {
            L_Aura.SetActive(true);
        }
        else if (context.action == rightHandGrabAction.action)
        {
            R_Aura.SetActive(true);
        }
    }

    private void OnHandGrabCanceled(InputAction.CallbackContext context)
    {
        if (context.action == leftHandGrabAction.action)
        {
            L_Aura.SetActive(false);
        }
        else if (context.action == rightHandGrabAction.action)
        {
            R_Aura.SetActive(false);
        }
    }

    private void Update()
    {
        // Ensure the hand tracking manager and hands are not null
        if (handTrackingManager != null && handTrackingManager.LeftHand != null && handTrackingManager.RightHand != null)
        {
            // Update aura positions and rotations to match the hands, if the auras are active
            if (L_Aura.activeSelf)
            {
                Vector3 leftHandPositionOffset = handTrackingManager.LeftHand.transform.position + handTrackingManager.LeftHand.transform.forward * 0.05f;
                L_Aura.transform.position = leftHandPositionOffset;
                L_Aura.transform.rotation = handTrackingManager.LeftHand.transform.rotation;
            }

            if (R_Aura.activeSelf)
            {
                Vector3 rightHandPositionOffset = handTrackingManager.RightHand.transform.position + handTrackingManager.RightHand.transform.forward * 0.05f;
                R_Aura.transform.position = rightHandPositionOffset;
                R_Aura.transform.rotation = handTrackingManager.RightHand.transform.rotation;
            }
        }
    }
}