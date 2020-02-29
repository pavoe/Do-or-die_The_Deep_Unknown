using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public int MaxHP { get; private set; }
    public int HP { get; private set; }

    private float speed = 8f;
    private float jumpForce = 19f;
    private float rayLength = 0.02f;
    private LayerMask ground;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private BoxCollider2D innerCollider;

    private float shiftSlowDownDelay;

    //USE "I" to activate invincible mode;
    public bool INVINCIBLECHEAT = false;


    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }



    // Start is called before the first frame update
    void Start()
    {
        
        //TEMPORARY FOR TESTING
        MaxHP = 100;
        HP = MaxHP;

        ground = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //innerCollider = transform.Find("InnerCollider").GetComponent<BoxCollider2D>();

        if (OnLandEvent == null)
        { OnLandEvent = new UnityEvent(); }

    }

    // Update is called once per frame
    void Update()
    {
        if (shiftSlowDownDelay > 0f)
        {
            shiftSlowDownDelay -= Time.deltaTime;
        }
        if (isGrounded()==true)
        {
            OnLandEvent.Invoke();
        }
       
    }


    public void Move(float moveInput)
    {

        if (Mathf.Abs(rb.velocity.x) < speed)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(Math.Min(rb.velocity.x + moveInput * speed, speed * moveInput), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Math.Max(rb.velocity.x + moveInput * speed, speed * moveInput), rb.velocity.y);

            }
        }

        if ((moveInput == 0 && Math.Abs(rb.velocity.x)>0||Mathf.Abs(rb.velocity.x)>speed)&&shiftSlowDownDelay<=0f)
        {
            rb.velocity -= new Vector2(Math.Sign(rb.velocity.x) * Mathf.Abs(rb.velocity.x), 0);
        }

        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (moveInput < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
    public void ExecuteShift(bool right)
    {
        if (shiftSlowDownDelay <= 0)
        {
            shiftSlowDownDelay = 0.35f;
            if (right)
            {
                rb.velocity = new Vector2(16f, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-16f, rb.velocity.y);
            }
        }
    }

    public void AIMove(float moveInput)
    {

      rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (moveInput< 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
}

/// <summary>
/// A function used to Make a character Jump if a jump can be made
/// </summary>
public void Jump()
    {
     

        if (isGrounded() && /*(!innerCollider.IsTouching(GameObject.Find("Environment").GetComponent<CompositeCollider2D>())
            && !innerCollider.IsTouching(GameObject.Find("Platforms").GetComponent<CompositeCollider2D>()))&&*/ (rb.velocity.y<=0))
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
        Vector2 posL = boxPos - new Vector3(boxCollider.size.x / 2, boxCollider.size.y/2);
        Vector2 posR = boxPos + new Vector3(boxCollider.size.x / 2, -boxCollider.size.y/2);
        Vector2 direction = Vector2.down;
        Debug.DrawLine(posL, posL + direction * rayLength);
        Debug.DrawLine(posR, posR + direction * rayLength);
        RaycastHit2D hitL = Physics2D.Raycast(posL, direction, rayLength, ground);
        RaycastHit2D hitR = Physics2D.Raycast(posR, direction, rayLength, ground);

        return hitL.collider != null || hitR.collider != null;
    }


    public void dealDamage(int damage)
    {
        if (!INVINCIBLECHEAT)
        {
            if (damage > HP)
            {
                if (gameObject.Equals(GameController.gameController.PC))
                {
                    GameController.AllowGameControlls = false;
                    GameController.menu.showGameOverScreen();
                    //GAME OVER
                }
                else
                {
                    GameController.Kills++;
                    GameController.gameController.SpawnCollectibles(transform.position);
                    Destroy(gameObject);
                    //ENEMY DEFEATED
                }
            }
            else
            {


                //Debug.Log("Damage dealt: " + damage);
                HP -= damage;

                if (gameObject.Equals(GameController.gameController.PC))
                {
                    //Debug.Log("HP Change Registered");
                    GameController.gameController.RefreshCharacterHealthDisplay();
                }

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Abyss1") 
            GameController.menu.showGameOverScreen();

        if (collision.gameObject.name == "Passageway")
        {
            Debug.Log("FinishingLevel");
            GameController.EndTime = Time.time;
            GameController.menu.showBridgeScreen();
        }
        if(collision.gameObject.name== "HealthDrop")
        {
            
            HP+=25;
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
            GameController.gameController.RefreshCharacterHealthDisplay();
            Destroy(collision.gameObject);
        }
    }
            
}
