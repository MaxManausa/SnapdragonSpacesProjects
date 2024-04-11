using UnityEngine;
using UnityEngine.InputSystem;
using QCHT.Interactions.Core;
using System.Diagnostics; // For Stopwatch

public class SpellCastingAura : MonoBehaviour
{
    [Header("Input Action References")]
    public InputActionReference leftHandGrabAction;
    public InputActionReference rightHandGrabAction;

    [Header("Aura Objects")]
    public GameObject R_SmAura;
    public GameObject R_MeAura;
    public GameObject R_LaAura;
    public GameObject L_SmAura;
    public GameObject L_MeAura;
    public GameObject L_LaAura;

    // Reference to the XRHandTrackingManager component
    public XRHandTrackingManager handTrackingManager;

    [Header("Spell Launching")]
    public LaunchSpell leftHandSpellLauncher; // Assign in the Inspector
    public LaunchSpell rightHandSpellLauncher; // Assign in the Inspector

    private Stopwatch spellChargeTimer; // Timer to measure spell charge time
    private bool isLeftHandGrabbing = false;
    private bool isRightHandGrabbing = false;

    public int spellLevel;

    private void Awake()
    {
        leftHandGrabAction.action.Enable();
        rightHandGrabAction.action.Enable();

        spellChargeTimer = new Stopwatch(); // Initialize the stopwatch
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
        spellChargeTimer.Restart(); // Start or restart the timer
        spellLevel = 0;

        if (context.action == leftHandGrabAction.action)
        {
            isLeftHandGrabbing = true;
        }
        else if (context.action == rightHandGrabAction.action)
        {
            isRightHandGrabbing = true;
        }

        UpdateAuras();
    }

    private void OnHandGrabCanceled(InputAction.CallbackContext context)
    {
        spellChargeTimer.Stop(); // Stop the timer

        if (context.action == leftHandGrabAction.action)
        {
            DeactivateAllLeftAuras();
            isLeftHandGrabbing = false;
            leftHandSpellLauncher.Launch(); // Launch the spell when the left hand grab is released
        }
        else if (context.action == rightHandGrabAction.action)
        {
            DeactivateAllRightAuras();
            isRightHandGrabbing = false;
            rightHandSpellLauncher.Launch(); // Launch the spell when the right hand grab is released
        }
    }

    private void Update()
    {
        if (handTrackingManager != null && handTrackingManager.LeftHand != null && handTrackingManager.RightHand != null)
        {
            if (spellChargeTimer.IsRunning)
            {
                UpdateAuras();
            }

            UpdateAuraPositionAndRotation(L_SmAura, handTrackingManager.LeftHand.transform);
            UpdateAuraPositionAndRotation(L_MeAura, handTrackingManager.LeftHand.transform);
            UpdateAuraPositionAndRotation(L_LaAura, handTrackingManager.LeftHand.transform);

            UpdateAuraPositionAndRotation(R_SmAura, handTrackingManager.RightHand.transform);
            UpdateAuraPositionAndRotation(R_MeAura, handTrackingManager.RightHand.transform);
            UpdateAuraPositionAndRotation(R_LaAura, handTrackingManager.RightHand.transform);
        }
    }

    public void UpdateAuras()
    {
        double elapsedTime = spellChargeTimer.Elapsed.TotalSeconds;

        // Determine which aura to activate based on the elapsed time
        if (elapsedTime < 2)
        {
            ActivateAura(isLeftHandGrabbing ? L_SmAura : null, isRightHandGrabbing ? R_SmAura : null);
            spellLevel = 1;
        }
        else if (elapsedTime < 5)
        {
            ActivateAura(isLeftHandGrabbing ? L_MeAura : null, isRightHandGrabbing ? R_MeAura : null);
            spellLevel = 2;
        }
        else if (elapsedTime >= 5)
        {
            ActivateAura(isLeftHandGrabbing ? L_LaAura : null, isRightHandGrabbing ? R_LaAura : null);
            spellLevel = 3;
        }
    }

    private void ActivateAura(GameObject leftAura, GameObject rightAura)
    {
        // Deactivate all auras first
        DeactivateAllLeftAuras();
        DeactivateAllRightAuras();

        // Then activate the selected ones
        if (leftAura != null) leftAura.SetActive(true);
        if (rightAura != null) rightAura.SetActive(true);
    }

    private void DeactivateAllLeftAuras()
    {
        L_SmAura.SetActive(false);
        L_MeAura.SetActive(false);
        L_LaAura.SetActive(false);
    }

    private void DeactivateAllRightAuras()
    {
        R_SmAura.SetActive(false);
        R_MeAura.SetActive(false);
        R_LaAura.SetActive(false);
    }

    private void UpdateAuraPositionAndRotation(GameObject aura, Transform handTransform)
    {
        if (aura != null && aura.activeSelf)
        {
            Vector3 handPositionOffset = handTransform.position + handTransform.forward * 0.05f;
            aura.transform.position = handPositionOffset;
            aura.transform.rotation = handTransform.rotation;
        }
    }
}