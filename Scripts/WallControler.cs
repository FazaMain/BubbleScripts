using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class WallControler : MonoBehaviour
{
    public Animator[] AnimatorsArray;
    public float Delay;
    public GameManager GM;
    public Light2D[] LightsArray;
    public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        LightsArray = Parent.GetComponentsInChildren<Light2D>()
                                         .AsEnumerable()
                                         .Where(l => l.gameObject != Parent)
                                         .ToArray();
        StartCoroutine(WaveAnimCo());
        foreach (Light2D L in LightsArray)
        {
            L.color = GM.LevelColor;
            L.intensity = 1f;
        }
        
    }

    public IEnumerator WaveAnimCo()
    {
        for(int i = 0; i < AnimatorsArray.Length; i++)
        {
            AnimatorsArray[i].SetTrigger("Go");
            yield return new WaitForSeconds(Delay);
        }
        StartCoroutine(WaveAnimCo());
    }

}
