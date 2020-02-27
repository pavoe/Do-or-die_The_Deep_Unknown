using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    List<GameObject> enemies = new List<GameObject>(); 
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameObject.Find("Enemies").transform.childCount; i++)
        { 
           enemies.Add(GameObject.Find("Enemies").transform.GetChild(i).gameObject);
           enemies[i].transform.Find("ReloadArt").GetComponent<Renderer>().enabled = false;
        }
        
        ground = LayerMask.GetMask("Ground");
    }

    
    public float speed = 3f;
    private Rigidbody2D rigidBody;
    private LayerMask ground;
    float enemywidth;
    float distance;
    
    private BoxCollider2D boxCollider;
    float Distance() //Only positive numbers
    {
        if (distance > 0)
        {
            return distance;
        }
        else
            return -distance;
    }

    void CheckLineOfFire(GameObject enemy)
    {
        Vector3 direction;
        Vector3 origin = enemy.transform.position;

        //Debug.Log("Checking");
        if (enemy.transform.position.x > GameController.gameController.PC.transform.position.x) //Check if the player is on his left or right
        {
            enemy.transform.rotation = Quaternion.Euler(0,0,0);
            direction = Vector3.left;
            origin -= (new Vector3(enemy.transform.localScale.x * enemy.GetComponent<BoxCollider2D>().size.x, 0)); 
        }
        else
        {
            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Vector3.right;
            origin += (new Vector3(enemy.transform.localScale.x*enemy.GetComponent<BoxCollider2D>().size.x, 0));

        }

        RaycastHit2D hitinfo = Physics2D.Raycast(origin, direction);
        
        if (hitinfo.transform.name=="Player") 
        {
            //Debug.Log(enemy.name);
            enemy.GetComponent<Weapon>().Shoot();
        }


    }
   
    void Move(GameObject enemy)
    { 
        Vector2 myvelocity = rigidBody.velocity;
        myvelocity.x = -enemy.transform.right.x * speed;
        rigidBody.velocity = myvelocity;
        
    }
    
    void ChangeDirection(GameObject enemy)
    {
        Vector2 LinecastPos = enemy.transform.position - enemy.transform.right*enemywidth*1.1f; 
        Debug.DrawLine(LinecastPos, LinecastPos + new Vector2(0, -1.1f));
        Vector3 currentrotation = enemy.transform.eulerAngles;


        
        if (!Physics2D.Linecast(LinecastPos, LinecastPos + new Vector2(0,-1.1f), ground) || Physics2D.Linecast(LinecastPos, LinecastPos,ground)) //if the linecast on his right hits nothing (not the ground)
        {
            if (currentrotation.y == 180) //If the enemy is turned right
            {
                currentrotation.y = 0; //the enemy turns left
            }
            else
            {
                currentrotation.y = 180;
            }
            
        }

        enemy.transform.eulerAngles = currentrotation; //realization
    }
  
    void Update()
    {
        
        foreach (GameObject enemy in enemies.ToArray())
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
            }
            else
            {
                rigidBody = enemy.GetComponent<Rigidbody2D>();
                enemywidth = enemy.GetComponentInChildren<BoxCollider2D>().bounds.extents.x;
                distance = enemy.transform.position.y - GameController.gameController.PC.transform.position.y;
                boxCollider = enemy.GetComponent<BoxCollider2D>();
                if (enemy.GetComponentInChildren<SpriteRenderer>().isVisible && Distance() < 2) //player notonly visible but also nearby in terms of y axis
                {

                    CheckLineOfFire(enemy);
                }
                else //I think it is needed to calculate some distance because otherwise enemy wouldn't know what to do - if he should patrol or shoot.
                {
                    Move(enemy);
                    ChangeDirection(enemy);
                }

                if (enemy.GetComponent<Weapon>().Ammo == 0&&!enemy.GetComponent<Weapon>().Reloading)
                {
                    enemy.GetComponent<Weapon>().Reload();
                }
            }
            
            
        }

    }
    
}