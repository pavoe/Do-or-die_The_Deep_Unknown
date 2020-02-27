using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 40;
    public Rigidbody2D rb;
    float sanityTime;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed; //przemieszczanie się pocisku
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name); //wyświetlanie w konsoli informacji o tym, na jaki collider natrafia pocisk

        //ZADAWANIE OBRAŻEŃ WROGOWI
        Character hitTarget = hitInfo.GetComponent<Character>();
        //Character innerColliderHit = hitInfo.GetComponentInParent<Character>();
        //if(innerColliderHit != null)
        //{
        //    hitTarget = innerColliderHit;
        //}
        if (hitTarget != null) 
        {
            
            hitTarget.dealDamage(damage); //zadajemy obrażenia
            if (transform.parent.gameObject.Equals(GameController.gameController.PC))
            {
                sanityTime = Time.time;
                GameController.Hits++;
            }
            Destroy(gameObject);
        }
        else
        {
            if (transform.parent.gameObject.Equals(GameController.gameController.PC))
            {
                if (Time.time - sanityTime > 0.1f)
                {
                    GameController.Misses++;
                }
            }
        }
        
        Destroy(gameObject); //niszczy pocisk po trafieniu w coś
    }
}
