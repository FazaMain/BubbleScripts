using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float DampingRatio;
    public float Frequency;
    SpringJoint2D[] SpringArray;
    public GameObject Circle;
    public Collider2D[] Colliders;
    public Rigidbody2D RBody;
    public Animator PlayerAnimator;
    public FixedJoystick Joystick;
    public float moveSpeed;
    public GameManager GM;
    public AudioSource[] PlayerSoundsArray;
    void Start()
    {
        SpringArray = GetComponentsInChildren<SpringJoint2D>();
        foreach (SpringJoint2D spring in SpringArray)
        {
            spring.dampingRatio = DampingRatio;
            spring.frequency = Frequency;
        }
    }
    private void FixedUpdate()
    {
        RBody.velocity = new Vector3(Joystick.Horizontal * moveSpeed, Joystick.Vertical * moveSpeed); //RBody.velocity.y,
    }
    public void TESTBUTTON()
    {
        
    }
    bool jumping = false;
    public void JUMP()
    {
        if (!jumping)
        {
            Debug.Log("Ïðûæîê");
            PlayerSoundsArray[0].Play();
            StartCoroutine(JumpCo());
        }
    }
    IEnumerator JumpCo()
    {
        jumping = true;
        PlayerAnimator.SetTrigger("Jump");
        for (int i = 0; i < Colliders.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Actor"), LayerMask.NameToLayer("Fence"), true);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < Colliders.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Actor"), LayerMask.NameToLayer("Fence"), false);
        }
        jumping = false;
    }
}
