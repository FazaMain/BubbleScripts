using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class BonusScript : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody2D rb;
    Vector2 direction;
    float movementSpeed;
    public float MaxSpeed;
    public float MinSpeed;

    public static event Action BonusPick;

    private void Start()
    {
        StartCoroutine("MovementRandomizerCo");
    }
    IEnumerator MovementRandomizerCo()
    {
        direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        movementSpeed = UnityEngine.Random.RandomRange(MinSpeed, MaxSpeed);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("MovementRandomizerCo");
    }
    public void FixedUpdate()
    {
        rb.velocity = direction * movementSpeed;
    }
    bool gotBonus = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gotBonus)
        {
            gotBonus = true;
            BonusPick();
            obj.SetActive(false);
        }
    }
    public IEnumerator LifetimeCo()
    {
        yield return new WaitForSeconds(10f);
        Destroy(obj);
    }
}
