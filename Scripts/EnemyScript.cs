using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Audio;


public class EnemyScript : MonoBehaviour
{
    public Color[] ColorsArray;
    public GameObject Enemy;
    
    
    public Light2D Light;
    public GameObject[] EnemyVisualPhisics;
    public Animator ActorAnimator;
    public ParticleSystem Particle;
    public GameObject TestParticle;
    public Collider2D[] CollidersArray;
    public EnemyCollision EC;
    public GameManager GM;
    public AudioSource EnemyKillSound;

    public int MaxHP = 15;
    public int ColorType;

    private void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GM = GameManager.GetComponent<GameManager>();
        Light.color = ColorsArray[ColorType];
        Particle.startColor = Light.color;
        StartCoroutine("StartCo");
    }
    IEnumerator StartCo()
    {
        EnemyVisualPhisics[3].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject obj in EnemyVisualPhisics)
        {
            obj.SetActive(true);
        }
    }
    public void Jump()
    {
        StartCoroutine(JumpCo());
    }
    public void Hit()
    {     
        MaxHP -= 2;
        if (MaxHP <= 0)
        {
            StartCoroutine(RebornCo());
        }
    }
    IEnumerator JumpCo()
    {
        float save = EC.movementSpeed;
        EC.movementSpeed = EC.movementSpeed + 5;
        //yield return new WaitForSeconds(0.5f);
        ActorAnimator.SetTrigger("Jump");
        for (int i = 0; i < CollidersArray.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Actor"), LayerMask.NameToLayer("Fence"), true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < CollidersArray.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Actor"), LayerMask.NameToLayer("Fence"), false);
        }
        EC.movementSpeed = save;
    }

    bool Done;
    IEnumerator RebornCo()
    {
        if (!Done)
        {
            Done = true;

            for (int i = 0; i < 2; i++)
            {
                EnemyVisualPhisics[i].SetActive(false);
            }
            EnemyKillSound.Play();
            Particle.Play();
            Debug.Log("ÇàïóñêBurst");
            yield return new WaitForSeconds(0.4f);
            foreach (GameObject obj in EnemyVisualPhisics)
            {
                obj.SetActive(false);
            }
            yield return new WaitForSeconds(UnityEngine.Random.RandomRange(0.5f, 1f));
            GM.EnemySpawn(ColorType);

            Destroy(gameObject);
        }
        else
        {
            //Done = true;
        }
    }
}
