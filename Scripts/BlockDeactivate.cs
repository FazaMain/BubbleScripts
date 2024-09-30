using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlockDeactivate : MonoBehaviour
{
    public GameObject obj;
    public Light2D StartLight;
    float StartSpeed;
    GameManager GM;
    public bool Vertical;

    void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GM = GameManager.GetComponent<GameManager>();
        StartLight.color = GM.LevelColor;
        StartSpeed = 10;

        StartCoroutine(LiveCo());
    }

    public IEnumerator LiveCo()
    {
        yield return new WaitForSeconds(10f);
        Destroy(obj);
    }
    private void FixedUpdate()
    {
        if (Vertical)
        {
            transform.position += Vector3.down * StartSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * StartSpeed * Time.deltaTime;
        }
        
    }
}
