using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject enemy = GameObject.Find("Enemy");
    public GameObject player = GameController.gameController.PC;
    List<enemy> enemies = new List<enemy>(); //Jakoś inaczej tę deklarację klasy, enemy nie jest typem, więc trzeba inaczej (ale nie wiem jeszcze jak)
    private Vector3 cvcedistance = focusObject.transform.position - enemy.transform.position;
    public float pcedistance = Vector3.Distance(player.transform.position, enemy.transform.position);
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
        //sprawdzam czy odległość gracza od enemy jest odpowiednia
        //sprawdza co ma, jeśli gracz jest na linii strzału woła  
        //strzał. 
        //Coś w stylu  
        //enemy.GetComponent<Weapon>().Shoot(); 
        //tutaj powinny być te Raycasty
        if (pcedistance<=30)
        {
            //Tutaj Raycastować
            if (Physics.RayCast(transform.position, Vector3.right, 30)||Physics.RayCast(transform.position, Vector3.left, 30)) //zrobić to eleganciej
            {
                enemy.GetComponent<Weapon>().Shoot();
            }
        }

    } 
    void Update()
    {
        if (cvcedistance<=CameraController.followDistance)
        {
            foreach(GameObject enemy in enemies)
            {
                CheckLineOfFire(enemy);
            }
        }
        //float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        
        //Nie wiem, może da się coś zrobić, żeby Shoot dla Weapon działało też tu, public przy weapon nie zadziałało
    }
    
}

