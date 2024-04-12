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

    /*
    [Header("Lightning Magic")]
    [SerializeField] GameObject lightningSpell1;
    [SerializeField] GameObject lightningSpell2;
    [SerializeField] GameObject lightningSpell3;
    [SerializeField] GameObject lightningAura1;
    [SerializeField] GameObject lightningAura2;
    [SerializeField] GameObject lightningAura3;

    [Header("Gravity Magic")]
    [SerializeField] GameObject gravitySpell1;
    [SerializeField] GameObject gravitySpell2;
    [SerializeField] GameObject gravitySpell3;
    [SerializeField] GameObject gravityAura1;
    [SerializeField] GameObject gravityAura2;
    [SerializeField] GameObject gravityAura3;
    */


    public void R_Fire()
    {
        //this method makes the left hand spell casting magic as fire
        spellCastingAura.R_SmAura = r_FireAura1;
        spellCastingAura.R_MeAura = r_FireAura2;
        spellCastingAura.R_LaAura = r_FireAura3;

        r_SpellLauncher.Spell1 = fireSpell1;
        r_SpellLauncher.Spell2 = fireSpell2;
        r_SpellLauncher.Spell3 = fireSpell3;
    }

    public void L_Fire()
    {
        spellCastingAura.L_SmAura = l_FireAura1;
        spellCastingAura.L_MeAura = l_FireAura2;
        spellCastingAura.L_LaAura = l_FireAura3;

        l_SpellLauncher.Spell1 = fireSpell1;
        l_SpellLauncher.Spell2 = fireSpell2;
        l_SpellLauncher.Spell3 = fireSpell3;
    }

    public void R_Ice()
    {
        spellCastingAura.R_SmAura = r_IceAura1;
        spellCastingAura.R_MeAura = r_IceAura2;
        spellCastingAura.R_LaAura = r_IceAura3;

        r_SpellLauncher.Spell1 = iceSpell1;
        r_SpellLauncher.Spell2 = iceSpell2;
        r_SpellLauncher.Spell3 = iceSpell3;
    }

    public void L_Ice()
    {
        spellCastingAura.L_SmAura = l_IceAura1;
        spellCastingAura.L_MeAura = l_IceAura2;
        spellCastingAura.L_LaAura = l_IceAura3;

        l_SpellLauncher.Spell1 = iceSpell1;
        l_SpellLauncher.Spell2 = iceSpell2;
        l_SpellLauncher.Spell3 = iceSpell3;
    }
}
