using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugtoggler : MonoBehaviour
{
    public GameObject[] TriArray;
    public GameObject DebugPanel;

    bool On = false;

    public void Toggler()
    {
        On = !On;
        if (On)
        {
            DebugPanel.SetActive(true);
            TriArray[0].SetActive(false);
            TriArray[1].SetActive(true);
        }
        else
        {
            DebugPanel.SetActive(false);
            TriArray[1].SetActive(false);
            TriArray[0].SetActive(true);
        }
    }


}
