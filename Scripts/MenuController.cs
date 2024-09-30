using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject SettingsPanel;
    public GameObject MenuPanel;
    public GameObject UIPanel;
    public GameObject ContactPanel;
    public GameObject ExitPanel;
    public GameObject RestartButton;
    public GameObject GuidePanel;
    public GameObject SubCoverPanel;
    public GameObject LoadingPanel;

    [Header("Text")]
    public TextMeshProUGUI StartButtonText;
    public TextMeshProUGUI SoundText;
    public TextMeshProUGUI RoundText;
    public TextMeshProUGUI TotalScore;

    [Header("Audio")]
    float Sound = 100;
    public Slider SoundBar;
    public AudioMixer MasterMixer;

    [Header("Ссылки")]
    public FPSER FpsController;
    public GameManager GM;
    [Header("тогглы")]
    public Toggle VToggle;
    public Toggle FToggle;


    bool fps;
    bool vibro;
    public bool Playing;
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        RoundText.text = "Best Round: " + PlayerPrefs.GetInt("BestRounds");
        TotalScore.text = "Score: " + PlayerPrefs.GetInt("TotalScore");
        int v = PlayerPrefs.GetInt("VibroInt");
        int f = PlayerPrefs.GetInt("FPSint");
        if (v == 1)
        {
            vibro = true;
        }
        else 
        { vibro = false; }
        if(f == 1)
        {
            fps = true;
        }
        else
        {
            fps = false;
        }
        Debug.Log("fps " + PlayerPrefs.GetInt("FPSint"));
        Debug.Log("vibro " + PlayerPrefs.GetInt("VibroInt"));
        Debug.Log("fps bool " + fps + " vibrobool " + vibro);
    }
    void Start()
    {
        SoundBar.onValueChanged.AddListener(SoundChange);
        if (PlayerPrefs.HasKey("SoundValue"))
        {
            SoundChange(PlayerPrefs.GetFloat("SoundValue"));
            SoundBar.value = PlayerPrefs.GetFloat("SoundValue");
        }
        else
        {
            SoundChange(0.5f);
            SoundBar.value = 0.5f;///////////ТУТ//////////////////////////////////////////////////////////////////////////////////////////
        }
        
        togglersReactivate(vibro, fps);
    }
    void togglersReactivate(bool v, bool f)
    {
        VToggle.isOn = v;
        FToggle.isOn = f;
        Debug.Log("устанавливаю тоггл");
        StartCoroutine(CostylCo());
    }
  IEnumerator CostylCo()
    {
        yield return new WaitForSeconds(0.2f);
        Vfirsttime = false;
        Ffirsttime = false;
        yield return null;
        if (Playing)
        {
            FpsController.FPSON = fps;
            GM.VibroOn = vibro;
        }
    }
    public void GameStart()
    {
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene("Scene1");
    }
    public void RestartCurrentScene()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }

    bool PauseBool = false;
    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Пауза вне");
        if (Playing && !pause)
        {
            Debug.Log("Пауза в");
            Pause();
        }
        
    }
    public void Pause()
    {
        Debug.Log("Time1 " + Time.timeScale);
        PauseBool = true;
        RoundText.text = "Best Round: " + PlayerPrefs.GetInt("BestRounds");
        TotalScore.text = "Total Score: " + PlayerPrefs.GetInt("TotalScore");
        Time.timeScale = 0;
        SubCoverPanel.SetActive(true);
        MenuPanel.SetActive(true);
        GM.Music.Pause();
        Debug.Log("Time2 " + Time.timeScale);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseBool = false; 
        SubCoverPanel.SetActive(false);
        MenuPanel.SetActive(false);
        GM.Music.Play();
    }
    public void GuidePanelToggle()
    {
        Debug.Log("Гайд");
       GuidePanel.SetActive(!GuidePanel.activeSelf);
    }

    #region SettingsPanel
    public void SettingsPanelEnter()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
        if (PlayerPrefs.HasKey("SoundValue"))
        {
            SoundChange(PlayerPrefs.GetFloat("SoundValue"));
        }
    }
    public void SettinggsPanelExit()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
        PlayerPrefs.SetFloat("SoundValue", Sound);
    }
    bool Ffirsttime = true;
    bool Vfirsttime = true;
    public void FPSToggle()
    {
        Debug.Log("Прогон");
        if (!Ffirsttime)
        {
            Debug.Log("фпс переключил");
            fps = !fps;
            if (FpsController != null)
            {
                FpsController.FPSON = fps;
            }
            if (fps)
            {
                PlayerPrefs.SetInt("FPSint", 1);
            }
            else
            {
                PlayerPrefs.SetInt("FPSint", 0);
            }
            Debug.Log("fpsToSet " + PlayerPrefs.GetInt("FPSint"));
        }
        Ffirsttime = false;
    }
    
    public void vibroToggler()
    {
        if (!Vfirsttime)
        {
            Debug.Log("вибро переключил");
            vibro = !vibro;
            if (GM != null)
            {
                GM.VibroOn = vibro;
            }
            if (vibro)
            {
                PlayerPrefs.SetInt("VibroInt", 1);
            }
            else { PlayerPrefs.SetInt("VibroInt", 0); }
            Debug.Log("vibroToSet " + PlayerPrefs.GetInt("VibroInt"));
        }
        Vfirsttime = false;
    }
    void SoundChange(float f)
    {
        Sound = f;
        MasterMixer.SetFloat("AllSound", Mathf.Log10(Sound) * 20);
        float i = Sound * 100;
        SoundText.text = i.ToString("F0") + "%";
    }

    #endregion


    #region ExitPanel
    public void PreExitPanelToggle()
    {
        ExitPanel.SetActive(!ExitPanel.activeSelf);
    }
    public void Exit()
    {
        Debug.Log("Выхожу");
        PlayerPrefs.SetInt("StartInt", 0);
        Application.Quit();
    }
    #endregion

    public void GetContacts()
    {
        ContactPanel.SetActive(!ContactPanel.activeSelf);
    }
}
