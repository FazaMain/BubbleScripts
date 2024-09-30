using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class FPSER : MonoBehaviour
{
    public TextMeshProUGUI FPStext;
    public TextMeshProUGUI FPSforReleaseText;
    public bool FPSON;
    float fps;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        fps = (int)(1 / Time.unscaledDeltaTime);
        if (FPSON)
        {
            FPSforReleaseText.text = "FPS: " + fps;
        }
        else
        {
            FPSforReleaseText.text = " ";
        }
        FPStext.text = "FPS: " + fps;
    }

}
