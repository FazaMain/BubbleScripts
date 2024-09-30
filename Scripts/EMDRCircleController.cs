using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class EMDRCircleController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] CircleArray;
    public Color[] ColorArray;

    public void StartSeq(int Type)
    { 
      foreach(GameObject obj in CircleArray)
      {
            obj.SetActive(false);
      }
        CircleArray[Type].SetActive(true);
    }
     public void colorSwitch(int Type)
    {

    }


    void Start()
    {
        
    }


}
