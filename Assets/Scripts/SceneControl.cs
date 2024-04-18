using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Methods in this script:
 * - Start
 * - PauseNPlay / Music
 * - SFX_OnOff / SFX
 * - CloseApp / Close App
 */




public class SceneControl : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusicSource;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] Text storyMode_musicOnOff_text;
    [SerializeField] Text mainMenu_musicOnOff_text;
    [SerializeField] Text storyMode_sfxOnOff_text;
    [SerializeField] Text mainMenu_sfxOnOff_text;

    public bool sfx_On = true;

    // Start is called before the first frame update

    private void Start()
    {
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.Play();
        storyMode_musicOnOff_text.text = "Music: " + "\nOn";
        mainMenu_musicOnOff_text.text = "Music: " + "\nOn";

        sfx_On = true;
        storyMode_sfxOnOff_text.text = "SFX: " + "\nOn";
        mainMenu_sfxOnOff_text.text = "SFX: " + "\nOn";
    }

    public void PauseNPlay()
    {
        if (backgroundMusicSource.isPlaying) 
        { 
            backgroundMusicSource.Pause();
            storyMode_musicOnOff_text.text = "Music: " + "\nOff";
            mainMenu_musicOnOff_text.text = "Music: " + "\nOff";
        }
        else
        {
            backgroundMusicSource.Play();
            storyMode_musicOnOff_text.text = "Music: " + "\nOn";
            mainMenu_musicOnOff_text.text = "Music: " + "\nOn";
        }
    }

    public void SFX_OnOff()
    {
        if (sfx_On == true)
        {
            sfx_On = false;
            storyMode_sfxOnOff_text.text = "SFX: " + "\nOff";
            mainMenu_sfxOnOff_text.text = "SFX: " + "\nOff";
        }
        else
        {
            sfx_On = true;
            storyMode_sfxOnOff_text.text = "SFX: " + "\nOn";
            mainMenu_sfxOnOff_text.text = "SFX: " + "\nOn";
        }
    }

    public void CloseApp()
    {
        Application.Quit();
    }

}
