using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorSpeed();
        
    }

    void AnimatorSpeed()
    {
        animator = gameObject.GetComponent<Animator>();
        if (gameObject.name == "Player")
        {
            animator.speed = 0.15f;
        }
        else
            animator.speed = 0.05f;

    }
    float horizontalvelocity;
    Rigidbody2D rigidBody;
    void GORun()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        horizontalvelocity = rigidBody.velocity.x;
        if (gameObject != null) //Don't know if I must check it or not
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalvelocity));
        }
    }
    bool jump = false;
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    // Update is called once per frame
    void Update()
    {
        GORun();
        PlayerJump();
    }
}
