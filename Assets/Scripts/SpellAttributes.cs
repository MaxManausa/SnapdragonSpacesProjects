using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttributes : MonoBehaviour
{
    // This script has all the details about a certain spell
    // this script goes on a spell

    [SerializeField] private AudioSource spellAudio;
    [SerializeField] private AudioClip spell_StartSound;
    
    //[SerializeField] private AudioClip spell_EndSound;
    // THE END SOUND IS ON THE END PARTICLES

    //[SerializeField] private ParticleSystem startingParticles;

    //[SerializeField] private ParticleSystem endParticles;
    // THE END PARTICLES ARE ON THE SPELLCOLLISIONHANDLER SCRIPT

    // Start is called before the first frame update
    void Start()
    {
        //fired particle effect
        //fired sound from audio source on itself if sfx is on

        SceneControl sceneControl = FindObjectOfType<SceneControl>();

        if (sceneControl != null && sceneControl.sfx_On)
        {
            // Only play the sound if the sfx_On flag is true
            spellAudio.clip = spell_StartSound;
            spellAudio.Play();
        }

        //collides with target
        //spell renderer gets turned off
        //spell hit particle effect plays
        //spell hit sound plays
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
