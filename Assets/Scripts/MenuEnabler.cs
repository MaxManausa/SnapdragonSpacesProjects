using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is accessed from the Story Manager script to turn off the in game menu while popup screens are active.
public class MenuEnabler : MonoBehaviour
{
    [SerializeField] PinchHandMenu pinchHandMenu;
    [SerializeField] GameObject storyMenu;

    // Start is called before the first frame update
    public void NoMenusAllowed()
    {
        storyMenu.SetActive(false);
        pinchHandMenu.enabled= false;
    }

    public void MenusAreAllowed()
    {
        storyMenu.SetActive(true);
        pinchHandMenu.enabled = true;
    }
}
