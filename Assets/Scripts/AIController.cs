using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    List<GameObject> enemies = new List<GameObject>(); //Jakoś inaczej tę deklarację klasy, enemy nie jest typem, więc trzeba inaczej (ale nie wiem jeszcze jak)
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameObject.Find("Enemies").transform.childCount; i++)
        { 
           enemies.Add(GameObject.Find("Enemies").transform.GetChild(i).gameObject); 
        }
        
        ground = LayerMask.GetMask("Ground");
    }

    
    public float speed = 3f;
    private Rigidbody2D rigidBody;
    private LayerMask ground;
    float enemywidth;
    float distance;
    float rayLength = 1f;
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

        Debug.Log("Checking");
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
            Debug.Log(enemy.name);
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
        Vector2 LinecastPos = enemy.transform.position - enemy.transform.right * enemywidth; 
        Debug.DrawLine(LinecastPos, LinecastPos + Vector2.down);
        Vector3 currentrotation = enemy.transform.eulerAngles;
        if (currentrotation.y==180) //If the enemy is turned right
        {
            if (!Physics2D.Linecast(LinecastPos, LinecastPos + Vector2.down, ground)) //if the linecast on his right hits nothing (not the ground)
            {
                currentrotation.y = 0; //the enemy turns left
            }
        }
        else //if the enemy is turned left
        {
            if (!Physics2D.Linecast(LinecastPos, LinecastPos + Vector2.down, ground)) //if the linecast on his left hits nothing
            {
                currentrotation.y = 180; //the enemy turns right
            }
        }
        
        enemy.transform.eulerAngles = currentrotation; //realization
    }
  
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            
            rigidBody = enemy.GetComponent<Rigidbody2D>();
            enemywidth = enemy.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
            distance = enemy.transform.position.y - GameController.gameController.PC.transform.position.y;
            boxCollider = enemy.GetComponent<BoxCollider2D>();
            if (enemy.GetComponentInChildren<SpriteRenderer>().isVisible&&Distance()<2) //player notonly visible but also nearby in terms of y axis
            {

                CheckLineOfFire(enemy);
            }
            else //I think it is needed to calculate some distance because otherwise enemy wouldn't know what to do - if he should patrol or shoot.
            {
                Move(enemy);
                ChangeDirection(enemy);
            }
            
            //Debug.Log(enemy.name);
            
        }

    }
    
}