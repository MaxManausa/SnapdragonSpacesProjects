using UnityEngine;
using UnityEngine.InputSystem;
using QCHT.Interactions.Core;
using System.Diagnostics; // For Stopwatch
using QCHT.Interactions.Hands;

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
    public SpellType spellType;

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
            spellType.LeftMagicHand();
            leftHandSpellChargeTimer.Restart();
            leftHandAudio.Play();
        }
        else
        {
            spellType.RightMagicHand();
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
            spellType.LeftHandNoMagic();
            leftHandSpellLevel = CalculateSpellLevel(leftHandSpellChargeTimer.Elapsed.TotalSeconds);
            leftHandSpellLauncher.PrepareSpell(true);
            leftHandSpellLauncher.Launch();
            leftHandAudio.Stop();
            DeactivateAllLeftAuras();
        }
        else
        {
            spellType.RightHandNoMagic();
            rightHandSpellLevel = CalculateSpellLevel(rightHandSpellChargeTimer.Elapsed.TotalSeconds);
            rightHandSpellLauncher.PrepareSpell(false);
            rightHandSpellLauncher.Launch();
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

        // Only deactivate and unparent the auras for the current hand
        DeactivateAndUnparentAura(smAura);
        DeactivateAndUnparentAura(meAura);
        DeactivateAndUnparentAura(laAura);

        // Activate the appropriate aura and make it a child of the hand
        GameObject activeAura = null;
        if (spellLevel == 1) activeAura = smAura;
        else if (spellLevel == 2) activeAura = meAura;
        else if (spellLevel >= 3) activeAura = laAura;

        if (activeAura != null)
        {
            activeAura.SetActive(true);
            activeAura.transform.SetParent(handTransform, false);
            activeAura.transform.localPosition = new Vector3(0, 0, 0.05f); // Adjust local position as needed
            activeAura.transform.localRotation = Quaternion.identity; // Adjust local rotation as needed
        }
    }

    private void DeactivateAllLeftAuras()
    {
        DeactivateAndUnparentAura(L_SmAura);
        DeactivateAndUnparentAura(L_MeAura);
        DeactivateAndUnparentAura(L_LaAura);
    }

    private void DeactivateAllRightAuras()
    {
        DeactivateAndUnparentAura(R_SmAura);
        DeactivateAndUnparentAura(R_MeAura);
        DeactivateAndUnparentAura(R_LaAura);
    }

    private void DeactivateAndUnparentAura(GameObject aura)
    {
        if (aura.activeSelf)
        {
            aura.SetActive(false);
            aura.transform.SetParent(null);
        }
    }

    private int CalculateSpellLevel(double elapsedTime)
    {
        if (elapsedTime < 2) return 1;
        else if (elapsedTime < 5) return 2;
        else return 3;
    }
}