using QCHT.Interactions.Hands;
using QCHT.Samples.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/* This script is about choosing different magic types: 
     * Fire, Ice, Lightning, and Gravity
     * Each magic type has its 1 magic hands, 3 magic auras, 3 magic missiles, 1 aura charging sound
     * 3 magic missile firing sounds, 3 magic missile hit sounds
     * Thats 14 things... times 4 magic types ==== 56 things.
     * 3 - This script needs to connect with the LaunchSpell on the selected hand set the spells for that hand 1,2,3
     * 3 - This script needs the spellcastingaura script to set the auras 1,2,3 for right hand and for left hand.
     * 1 - The magic hands are currently set up in the editor on the button, but eventually they too will change on grab, maybe
     * 7 - sounds have not been set up yet
     * 
     * 
     * Then there is also particle effects when the spell is fired and effects when the spell hits something for each spell, 
     * so that is 2x3x4 which is an additional 24 things
     * 
     * 
     * there will be a script on the spell objects that cover the: spell itself, particle effect when its active, 
     * audio sound when its charging, audio sound when its fired, audio sound when its hit, particle effect when its hit, 
     * as well as any additional information.
     * 
     */


public class SpellType : MonoBehaviour
{
    [Header("Magic Scripts")]
    [SerializeField] SpellCastingAura spellCastingAura;
    [SerializeField] LaunchSpell l_SpellLauncher;
    [SerializeField] LaunchSpell r_SpellLauncher;

    [Header("Hands")]
    [SerializeField] SkinSwitcher handSkinSwitcher;
    [SerializeField] HandSkin r_noMagicHand;
    [SerializeField] HandSkin l_noMagicHand;
    [SerializeField] HandSkin r_fireHand;
    [SerializeField] HandSkin l_fireHand;
    [SerializeField] HandSkin r_iceHand;
    [SerializeField] HandSkin l_iceHand;
    [SerializeField] HandSkin r_lightningHand;
    [SerializeField] HandSkin l_lightningHand;
    [SerializeField] HandSkin r_gravityHand;
    [SerializeField] HandSkin l_gravityHand;


    [Header("No Magic")]
    [SerializeField] GameObject noSpell1;
    [SerializeField] GameObject r_NoAura1;
    [SerializeField] GameObject l_NoAura1;

    [Header("Fire Magic")]
    [SerializeField] GameObject fireSpell1;
    [SerializeField] GameObject fireSpell2;
    [SerializeField] GameObject fireSpell3;
    [SerializeField] GameObject r_FireAura1;
    [SerializeField] GameObject r_FireAura2;
    [SerializeField] GameObject r_FireAura3;
    [SerializeField] GameObject l_FireAura1;
    [SerializeField] GameObject l_FireAura2;
    [SerializeField] GameObject l_FireAura3;


    [Header("Ice Magic")]
    [SerializeField] GameObject iceSpell1;
    [SerializeField] GameObject iceSpell2;
    [SerializeField] GameObject iceSpell3;
    [SerializeField] GameObject r_IceAura1;
    [SerializeField] GameObject r_IceAura2;
    [SerializeField] GameObject r_IceAura3;
    [SerializeField] GameObject l_IceAura1;
    [SerializeField] GameObject l_IceAura2;
    [SerializeField] GameObject l_IceAura3;

    
    [Header("Lightning Magic")]
    [SerializeField] GameObject lightningSpell1;
    [SerializeField] GameObject lightningSpell2;
    [SerializeField] GameObject lightningSpell3;
    [SerializeField] GameObject r_LightningAura1;
    [SerializeField] GameObject r_LightningAura2;
    [SerializeField] GameObject r_LightningAura3;
    [SerializeField] GameObject l_LightningAura1;
    [SerializeField] GameObject l_LightningAura2;
    [SerializeField] GameObject l_LightningAura3;

    [Header("Gravity Magic")]
    [SerializeField] GameObject gravitySpell1;
    [SerializeField] GameObject gravitySpell2;
    [SerializeField] GameObject gravitySpell3;
    [SerializeField] GameObject r_GravityAura1;
    [SerializeField] GameObject r_GravityAura2;
    [SerializeField] GameObject r_GravityAura3;
    [SerializeField] GameObject l_GravityAura1;
    [SerializeField] GameObject l_GravityAura2;
    [SerializeField] GameObject l_GravityAura3;


    public int r_spellnumber;
    public int l_spellnumber;

    private void Start()
    {
        handSkinSwitcher.SetLeftSkin(l_noMagicHand);
        handSkinSwitcher.SetRightSkin(r_noMagicHand);
        r_spellnumber = 0;
        l_spellnumber = 0;
        R_NoMagic();
        L_NoMagic();
    }

    public void R_NoMagic()
    {
        r_spellnumber = 0;

        handSkinSwitcher.SetRightSkin(r_noMagicHand);

        spellCastingAura.R_SmAura = r_NoAura1;
        spellCastingAura.R_MeAura = r_NoAura1;
        spellCastingAura.R_LaAura = r_NoAura1;

        r_SpellLauncher.Spell1 = noSpell1;
        r_SpellLauncher.Spell2 = noSpell1;
        r_SpellLauncher.Spell3 = noSpell1;
    }

    public void L_NoMagic()
    {
        l_spellnumber = 0;

        handSkinSwitcher.SetLeftSkin(l_noMagicHand);

        spellCastingAura.L_SmAura = l_NoAura1;
        spellCastingAura.L_MeAura = l_NoAura1;
        spellCastingAura.L_LaAura = l_NoAura1;

        l_SpellLauncher.Spell1 = noSpell1;
        l_SpellLauncher.Spell2 = noSpell1;
        l_SpellLauncher.Spell3 = noSpell1;
    }

    public void R_Fire()
    {
        r_spellnumber = 1;

        handSkinSwitcher.SetRightSkin(r_fireHand);
        //this method makes the right hand spell casting magic as fire
        spellCastingAura.R_SmAura = r_FireAura1;
        spellCastingAura.R_MeAura = r_FireAura2;
        spellCastingAura.R_LaAura = r_FireAura3;

        r_SpellLauncher.Spell1 = fireSpell1;
        r_SpellLauncher.Spell2 = fireSpell2;
        r_SpellLauncher.Spell3 = fireSpell3;
    }

    public void L_Fire()
    {
        l_spellnumber = 1;

        handSkinSwitcher.SetLeftSkin(l_fireHand);

        spellCastingAura.L_SmAura = l_FireAura1;
        spellCastingAura.L_MeAura = l_FireAura2;
        spellCastingAura.L_LaAura = l_FireAura3;

        l_SpellLauncher.Spell1 = fireSpell1;
        l_SpellLauncher.Spell2 = fireSpell2;
        l_SpellLauncher.Spell3 = fireSpell3;
    }

    public void R_Ice()
    {
        r_spellnumber = 2;

        handSkinSwitcher.SetRightSkin(r_iceHand);

        spellCastingAura.R_SmAura = r_IceAura1;
        spellCastingAura.R_MeAura = r_IceAura2;
        spellCastingAura.R_LaAura = r_IceAura3;

        r_SpellLauncher.Spell1 = iceSpell1;
        r_SpellLauncher.Spell2 = iceSpell2;
        r_SpellLauncher.Spell3 = iceSpell3;
    }

    public void L_Ice()
    {
        l_spellnumber = 2;

        handSkinSwitcher.SetLeftSkin(l_iceHand);

        spellCastingAura.L_SmAura = l_IceAura1;
        spellCastingAura.L_MeAura = l_IceAura2;
        spellCastingAura.L_LaAura = l_IceAura3;

        l_SpellLauncher.Spell1 = iceSpell1;
        l_SpellLauncher.Spell2 = iceSpell2;
        l_SpellLauncher.Spell3 = iceSpell3;
    }

    public void R_Lightning()
    {
        r_spellnumber = 3;

        handSkinSwitcher.SetRightSkin(r_lightningHand);

        spellCastingAura.R_SmAura = r_LightningAura1;
        spellCastingAura.R_MeAura = r_LightningAura2;
        spellCastingAura.R_LaAura = r_LightningAura3;

        r_SpellLauncher.Spell1 = lightningSpell1;
        r_SpellLauncher.Spell2 = lightningSpell2;
        r_SpellLauncher.Spell3 = lightningSpell3;
    }

    public void L_Lightning()
    {
        l_spellnumber = 3;

        handSkinSwitcher.SetLeftSkin(l_lightningHand);

        spellCastingAura.L_SmAura = l_LightningAura1;
        spellCastingAura.L_MeAura = l_LightningAura2;
        spellCastingAura.L_LaAura = l_LightningAura3;

        l_SpellLauncher.Spell1 = lightningSpell1;
        l_SpellLauncher.Spell2 = lightningSpell2;
        l_SpellLauncher.Spell3 = lightningSpell3;
    }


    public void R_Gravity()
    {
        r_spellnumber = 4;

        handSkinSwitcher.SetRightSkin(r_gravityHand);

        spellCastingAura.R_SmAura = r_GravityAura1;
        spellCastingAura.R_MeAura = r_GravityAura2;
        spellCastingAura.R_LaAura = r_GravityAura3;
        
        r_SpellLauncher.Spell1 = gravitySpell1;
        r_SpellLauncher.Spell2 = gravitySpell2;
        r_SpellLauncher.Spell3 = gravitySpell3;
    }

    public void L_Gravity()
    {
        l_spellnumber = 4;

        handSkinSwitcher.SetLeftSkin(l_gravityHand);

        spellCastingAura.L_SmAura = l_GravityAura1;
        spellCastingAura.L_MeAura = l_GravityAura2;
        spellCastingAura.L_LaAura = l_GravityAura3;

        l_SpellLauncher.Spell1 = gravitySpell1;
        l_SpellLauncher.Spell2 = gravitySpell2;
        l_SpellLauncher.Spell3 = gravitySpell3;
    }
}
