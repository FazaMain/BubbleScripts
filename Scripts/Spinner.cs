using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Spinner : MonoBehaviour
{
    public Animator InOutAnim;
    public GameObject Object;
    public GameObject Object2;

    public float rotationSpeed = 1f;
    public Light2D[] StartLight;
    public GameManager GM;
    public bool Dual;
    public BoxCollider2D[] ColliderArray;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.FenceOffSeq += OutLauncher;
        for (int i = 0; i < StartLight.Length; i++)
        {
            StartLight[i].color = GM.LevelColor;
        }
        StartCoroutine(StartCo());
    }
    
    void OnDisable()
    {
        GameManager.FenceOffSeq -= OutLauncher;
    }
    IEnumerator StartCo()
    {
        foreach (Collider2D col in ColliderArray)
        {
            col.isTrigger = true;
        }
        yield return new WaitForSeconds(1f);
        foreach (Collider2D col in ColliderArray)
        {
            col.isTrigger = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Object.transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
        if (Dual)
        {
            Object2.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        
    }
    public void OutLauncher()
    {
        StartCoroutine(OutCo());
    }

    public IEnumerator OutCo()
    {
        foreach(Collider2D col in ColliderArray)
        {
            col.isTrigger = true;
        }
        InOutAnim.SetTrigger("Out");
        yield return new WaitForSeconds(3f);
        foreach (Collider2D col in ColliderArray)
        {
            col.isTrigger = false;
        }
        this.gameObject.SetActive(false);
    }
}
