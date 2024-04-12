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

    public AudioSource leftHandAudio;
    public AudioSource rightHandAudio;

    private Stopwatch leftHandSpellChargeTimer = new Stopwatch();
    private Stopwatch rightHandSpellChargeTimer = new Stopwatch();

    public int leftHandSpellLevel;
    public int rightHandSpellLevel;

    private void Awake()
    {
        leftHandGrabAction.action.Enable();
        rightHandGrabAction.action.Enable();
    }

    private void OnEnable()
    {
        leftHandGrabAction.action.performed += context => OnHandGrabPerformed(context, true);
        leftHandGrabAction.action.canceled += context => OnHandGrabCanceled(context, true);
        rightHandGrabAction.action.performed += context => OnHandGrabPerformed(context, false);
        rightHandGrabAction.action.canceled += context => OnHandGrabCanceled(context, false);
    }

    private void OnDisable()
    {
        leftHandGrabAction.action.performed -= context => OnHandGrabPerformed(context, true);
        leftHandGrabAction.action.canceled -= context => OnHandGrabCanceled(context, true);
        rightHandGrabAction.action.performed -= context => OnHandGrabPerformed(context, false);
        rightHandGrabAction.action.canceled -= context => OnHandGrabCanceled(context, false);

        leftHandGrabAction.action.Disable();
        rightHandGrabAction.action.Disable();
    }

    private void OnHandGrabPerformed(InputAction.CallbackContext context, bool isLeftHand)
    {
        if (isLeftHand)
        {
            leftHandSpellChargeTimer.Restart();
            leftHandAudio.Play();
        }
        else
        {
            rightHandSpellChargeTimer.Restart();
            rightHandAudio.Play();
        }
    }

    private void OnHandGrabCanceled(InputAction.CallbackContext context, bool isLeftHand)
    {
        Stopwatch timer = isLeftHand ? leftHandSpellChargeTimer : rightHandSpellChargeTimer;
        timer.Stop();

        if (isLeftHand)
        {
            leftHandSpellLevel = CalculateSpellLevel(leftHandSpellChargeTimer.Elapsed.TotalSeconds);
            leftHandSpellLauncher.PrepareSpell(true); // Prepare the spell based on the left hand's spell level
            leftHandSpellLauncher.Launch(); // Launch the spell
            leftHandAudio.Stop();
            DeactivateAllLeftAuras();
        }
        else
        {
            rightHandSpellLevel = CalculateSpellLevel(rightHandSpellChargeTimer.Elapsed.TotalSeconds);
            rightHandSpellLauncher.PrepareSpell(false); // Prepare the spell based on the right hand's spell level
            rightHandSpellLauncher.Launch(); // Launch the spell
            rightHandAudio.Stop();
            DeactivateAllRightAuras();
        }
    }

    private void Update()
    {
        if (handTrackingManager != null && handTrackingManager.LeftHand != null && handTrackingManager.RightHand != null)
        {
            UpdateAura(leftHandSpellChargeTimer, true, handTrackingManager.LeftHand.transform);
            UpdateAura(rightHandSpellChargeTimer, false, handTrackingManager.RightHand.transform);
        }
    }

    private void UpdateAura(Stopwatch timer, bool isLeftHand, Transform handTransform)
    {
        if (!timer.IsRunning) return;

        int spellLevel = CalculateSpellLevel(timer.Elapsed.TotalSeconds);
        GameObject smAura = isLeftHand ? L_SmAura : R_SmAura;
        GameObject meAura = isLeftHand ? L_MeAura : R_MeAura;
        GameObject laAura = isLeftHand ? L_LaAura : R_LaAura;

        // Deactivate all auras first
        smAura.SetActive(false);
        meAura.SetActive(false);
        laAura.SetActive(false);

        // Activate the appropriate aura
        if (spellLevel == 1) smAura.SetActive(true);
        else if (spellLevel == 2) meAura.SetActive(true);
        else if (spellLevel >= 3) laAura.SetActive(true);

        // Update the position and rotation of the active aura
        UpdateAuraPositionAndRotation(smAura, handTransform);
        UpdateAuraPositionAndRotation(meAura, handTransform);
        UpdateAuraPositionAndRotation(laAura, handTransform);
    }

    private int CalculateSpellLevel(double elapsedTime)
    {
        if (elapsedTime < 2) return 1;
        else if (elapsedTime < 5) return 2;
        else return 3;
    }

    private void UpdateAuraPositionAndRotation(GameObject aura, Transform handTransform)
    {
        if (aura.activeSelf)
        {
            aura.transform.position = handTransform.position + handTransform.forward * 0.05f;
            aura.transform.rotation = handTransform.rotation;
        }
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
}