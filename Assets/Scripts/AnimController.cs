using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        animator.speed = 0.05f;
    }
    float horizontalvelocity;
    Rigidbody2D rigidBody;
    
    // Update is called once per frame
    void Update()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        horizontalvelocity = rigidBody.velocity.x;
        if (gameObject!=null) //Don't know if I must check it or not
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalvelocity));
        }
    }
}
