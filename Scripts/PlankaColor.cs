using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlankaColor : MonoBehaviour
{
    public Light2D StartLight;
    public Animator FenceAnimator;
    public GameManager GM;
    public BoxCollider2D Collider;

    private void OnEnable()
    {
        GameManager.FenceOffSeq += FenceOutStarter;
        StartLight.color = GM.LevelColor;
        StartCoroutine(StartCo());
    }
    private void OnDisable()
    {
        GameManager.FenceOffSeq -= FenceOutStarter;
    }
    IEnumerator StartCo()
    {
        Collider.isTrigger = true;
        yield return new WaitForSeconds(1f);
        Collider.isTrigger = false;
    }
    void FenceOutStarter()
    {
        StartCoroutine(FenceOutCo());
    }
    IEnumerator FenceOutCo()
    {
        Collider.isTrigger = true;
        FenceAnimator.SetTrigger("End");
        Debug.Log("PlankaOut");
        yield return new WaitForSeconds(4f);
        Collider.isTrigger = false;
        this.gameObject.SetActive(false);
        
    }
}
