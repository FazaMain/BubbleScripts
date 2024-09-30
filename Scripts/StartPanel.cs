using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class StartPanel : MonoBehaviour
{
    string Andrei;
    string Epilepsy;

    public float GreetTimeOut;
    public TextMeshProUGUI AndreiText;
    public TextMeshProUGUI EpilepsyText;
    public float Speed;
    public AudioSource StartSound;
    public AudioSource TypeSound;
    public GameObject GreetPandel;


    private void Start()
    {
        Andrei = "Made by Andrei Ter-Akopov";
        Epilepsy = "Epilepsy Warning: This game contains flashing lights that may trigger seizures";
        //if (!PlayerPrefs.HasKey("StartInt"))
        //{
        //    StartCoroutine(AndreiTyperCo());
        //    StartCoroutine(SoundCo());
        //}
        if(PlayerPrefs.GetInt("StartInt") == 0)
        {
            StartCoroutine(AndreiTyperCo());
            StartCoroutine(SoundCo());
        }
        else
        {
            GreetPandel.SetActive(false);
        }
    }

    IEnumerator SoundCo()
    {
        TypeSound.Play();
        yield return new WaitForSeconds(GreetTimeOut);
        StartSound.Play();
        yield return new WaitForSeconds(1f);
        GreetPandel.SetActive(false);
    }
    IEnumerator AndreiTyperCo()
    {       
        AndreiText.text = " ";
        EpilepsyText.text = " ";
        yield return null;
        for(int i = 0; i < Andrei.Length; i++)
        {
            string s = Andrei.Substring(0,i + 1);
            AndreiText.text = s;
            yield return new WaitForSeconds(Speed);
        }
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < Epilepsy.Length; i++)
        {
            string e = Epilepsy.Substring(0, i + 1);
            EpilepsyText.text = e;
            yield return new WaitForSeconds(Speed);
        }
        TypeSound.Stop();
        PlayerPrefs.SetInt("StartInt", 1);
    }
}
