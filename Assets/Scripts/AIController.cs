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
    }

    // Update is called once per frame

    void CheckLineOfFire(GameObject enemy)
    {
        Vector3 direction = new Vector3();
        Vector3 origin = enemy.transform.position;

        if (enemy.transform.position.x > GameController.gameController.PC.transform.position.x) //sprawdza czy gracz jest po lewej czy prawej
        {

            enemy.transform.rotation = Quaternion.Euler(0,0,0);
            direction = Vector3.left;

            origin -= (new Vector3(enemy.GetComponent<BoxCollider2D>().size.x, 0));
        }
        else
        {
            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Vector3.right;
            origin += (new Vector3(enemy.GetComponent<BoxCollider2D>().size.x, 0));

        }

        RaycastHit2D hitinfo = Physics2D.Raycast(origin, direction);

        if(enemy.name=="Enemy1 (1)")
        {
            Debug.Log(hitinfo.collider);
        }

        //Tutaj Raycastować
        if (hitinfo.transform.name=="Player") //zrobić to eleganciej
        {
            Debug.Log(enemy.name);
            enemy.GetComponent<Weapon>().Shoot();
        }


    }
    
    public float speed = 3f;
    private Rigidbody2D rigidBody;
    public Transform startPos, endPos;
    private bool collision;
    void Move()
    {
        rigidBody.velocity = new Vector2(transform.localScale.x, 0) * speed;
        
    }
    void ChangeDirection()
    {
        collision = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Ground"));
        if (!collision)
        {
            Vector3 temp = transform.localScale;
            if (temp.x == 1.0)
            {
                temp.x = -1.0f;
            }
            else
                temp.x = 1.0f;
            transform.localScale = temp;
        }
    }
   
    //problemy: start pos i end pos ustawione dla wszystkich, rigidbody access do konkentnego (probkem, bo ten skrypt podpięty jest do gamecontrollera, a nie do wroga) i daltego get component tak działa
    void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            rigidBody = enemy.GetComponent<Rigidbody2D>();
            
            //if (enemy.GetComponentInChildren<SpriteRenderer>().isVisible)
            //{
               
            //    CheckLineOfFire(enemy);
           // }
            
            Move();
            ChangeDirection();
            
            
            
            //może tu spróbować umieścić to chodzenie

        }

    }
    
}