using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetect : MonoBehaviour
{
    public GameManager GM;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            //Debug.Log("ѕытаюсь крутануть корутину");
            GM.HPManagerStarter(false, 10);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            GM.PlayerSounds[2].Play();

        }
    }
}
