using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFence : MonoBehaviour
{
    public Transform[] SpawnsArray;
    public GameObject Fence;
    public bool Vertical;

    private void OnEnable()
    {
        GameManager.FenceOffSeq += OutLauncher;
        Debug.Log("Начал блоки");
        StartCoroutine(LaunchCo());
    }
    private void OnDisable()
    {
        GameManager.FenceOffSeq -= OutLauncher;
    }

    public IEnumerator LaunchCo()
    {
        for (int i =0; i < SpawnsArray.Length; i++)
        { 
            Instantiate(Fence, SpawnsArray[i].position, Quaternion.identity);
            BlockDeactivate BlockFence = Fence.GetComponent<BlockDeactivate>();
            BlockFence.Vertical = Vertical;
            if (Vertical)
            {
                yield return new WaitForSeconds(Random.RandomRange(0.1f, 0.5f));
            }
            else { yield return new WaitForSeconds(Random.RandomRange(0.2f, 1f)); }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LaunchCo());
    }
    void OutLauncher()
    {
        StartCoroutine(OutCo());
    }
    IEnumerator OutCo()
    {
        StopCoroutine(LaunchCo());
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
