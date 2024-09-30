using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public float movementSpeed;
    public EnemyScript ES;
    public Rigidbody2D rb;
    Vector2 direction;
    bool Spawned = false;
    bool Retreat = false;
    private void Start()
    {
        StartCoroutine("StarterCo");
    }

    IEnumerator StarterCo()
    {
        yield return new WaitForSeconds(0.5f);
        Spawned = true;
        StartCoroutine(GoCo());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fence") & !Retreat && Spawned)
        {
            StopCoroutine(DerganieCO(0));
            StopCoroutine(RetreatCO());
            StopCoroutine(GoCo());
            int i = Random.RandomRange(0, 4);
            switch (i) 
            {
                case 0:
                    //Debug.Log("Бегу");
                    StartCoroutine(RetreatCO());
                    break;
                case 1:
                    StartCoroutine(RetreatCO());
                    //Debug.Log("Прыгаю");
                    //ES.Jump();
                    break;
                case 2:
                    StartCoroutine(RetreatCO());
                    //Debug.Log("Туплю");
                    break;
                case 3:
                   // Debug.Log("Дергаюсь");
                    StartCoroutine(DerganieCO(Random.RandomRange(3,6)));
                    break;
            }

            //bool decide = Random.value < 0.5f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            ES.Hit();
            //StopCoroutine(RetreatCO());
            //StopCoroutine(GoCo());
            //StartCoroutine(RetreatCO());
        }
    }

    IEnumerator GoCo()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        movementSpeed = Random.RandomRange(4, 6);
        yield return null;
        yield return new WaitForSeconds(Random.RandomRange(1f, 3));
        StartCoroutine(GoCo());
    }
    public IEnumerator RetreatCO()
    {
        Retreat = true;
        direction = -direction;
        movementSpeed = Random.RandomRange(6, 8);
        yield return null;
        yield return new WaitForSeconds(Random.RandomRange(0.5f, 2));
        Retreat = false;
        StartCoroutine(GoCo());
    }
    public IEnumerator DerganieCO(int i)
    {
        for(int x =0; x < i; x++)
        {
            direction = -direction;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void FixedUpdate()
    {
        rb.velocity = direction * movementSpeed;
    }
}
