using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject spellButtons;
    [SerializeField] GameObject optionButtons;

    // Start is called before the first frame update
    void Awake()
    {
        mainButtons.SetActive(true);
        spellButtons.SetActive(false);
        optionButtons.SetActive(false);
    }

}
