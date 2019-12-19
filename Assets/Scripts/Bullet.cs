using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

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
        if (hitTarget != null) 
        {
            hitTarget.dealDamage(damage); //zadajemy obrażenia
        }
        
        Destroy(gameObject); //niszczy pocisk po trafieniu w coś
    }
}
