using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLScript : MonoBehaviour
{

    // www.linkedin.com/in/andrei-ter-akopov-a78b41322
    // https://hh.ru/resume/d3cbbe97ff0dba59880039ed1f7671396d4743
    public string[] URLarray;

    public void OpenURL(int i)
    {
        Debug.Log("Открываю внешнюю ссылку");
        Application.OpenURL(URLarray[i]);
    }
}
