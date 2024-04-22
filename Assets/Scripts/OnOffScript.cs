using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;
    
    public void TurnThisOffTurnThisOn()
    {
        if (object1.activeSelf)
        {
            object1.SetActive(false);
            object2.SetActive(true);
        }
        else
        {
            object2.SetActive(false);
            object1.SetActive(true);
        }
    }
}
