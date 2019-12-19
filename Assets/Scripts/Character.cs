using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int HP { get; private set; }

    private float speed = 8f;
    private float jumpForce = 19f;
    private float rayLength = 0.89f;
    private LayerMask ground;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private BoxCollider2D innerCollider;

    // Start is called before the first frame update
    void Start()
    {

        //TEMPORARY FOR TESTING
        HP = 100;


        ground = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        innerCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }


    public void Move(float moveInput)
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (moveInput < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    /// <summary>
    /// A function used to Make a character Jump if a jump can be made
    /// </summary>
    public void Jump()
    {
        if (isGrounded() && innerCollider.IsTouching(GameObject.Find("Environment").GetComponent<CompositeCollider2D>()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    /// <summary>
    /// A function if a character is touching the ground
    /// </summary>
    /// <returns></returns>
    private bool isGrounded()
    {
        Vector3 boxPos = transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y);
        Vector2 posL = boxPos - new Vector3(boxCollider.size.x / 2, 0);
        Vector2 posR = boxPos + new Vector3(boxCollider.size.x / 2, 0);
        Vector2 direction = Vector2.down;

        RaycastHit2D hitL = Physics2D.Raycast(posL, direction, rayLength, ground);
        RaycastHit2D hitR = Physics2D.Raycast(posR, direction, rayLength, ground);

        return hitL.collider != null || hitR.collider != null;
    }


    public void dealDamage(int damage)
    {
        if (damage > HP)
        {
            if (gameObject.Equals(GameController.gameController.PC))
            {
                //GAME OVER
            }
            else
            {
                Destroy(gameObject);
                //ENEMY DEFEATED
            }
        }
        else
        {
            Debug.Log("Damage dealt: " + damage);
            HP -= damage;
        }
    }

}
