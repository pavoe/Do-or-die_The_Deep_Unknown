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
    private void CheckLineOfFire(GameObject enemy)
    {
        Vector3 direction = new Vector3();
        Vector3 origin = enemy.transform.position;

        if (enemy.transform.position.x > GameController.gameController.PC.transform.position.x) //sprawdza czy gracz jest po lewej czy prawej
        {
            direction = Vector3.left;
            origin -= (new Vector3(enemy.GetComponent<BoxCollider2D>().size.x, 0));
        }
        else
        {
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
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                CheckLineOfFire(enemy);
            }
        }

    }
    
}