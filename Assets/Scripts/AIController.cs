using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    //Po pierwsze to jest komentarz do samego GameObjectu Enemy. Najlepiej zrób po prostu kopię GO(GameObject) Player, zmień jej
    //sprita, i zapisz jako prefab. To ułatwi ich kopiowanie wrzucanie na mapę bez problemów z kopiowaniem itp. 

    //Po drugie polecam zrobić pusty GO o nazwie "Enemies" i wszystkich przeciwników dodawać jako children tego obiektu,
    //wtedy bardzo prostu będzie znaleźć wzsystkich przeciwników w sposób np jak
    //public List<GameObject> enemies = new List<GameObject>();
    //KONTYNUACJA W STARCIE


    public GameObject enemy = GameObject.Find("Enemy");
    //Jak zwrócisz uwagę to w tym momencie GameController ma odwołanie do GO gracza, w ten sposób możesz uniknąć kosztownego
    //dla systemu GameObject.Find
    //public GameObject player = GameController.gameController.PlayerCharacter;
    public GameObject player = GameObject.Find("Player");
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < GameObject.Find("Enemies").transform.childCount; i++)
        //{
        //    enemies.Add(GameObject.Find("Enemies").transform.GetChild(i).gameObject);
        //}

        //W ten sposób gdy na planszy będzie wiele przeciwników AIController będzie przechowywał Listę wszystkich przeciwników
        //i zapewniał łatwy dostęp do ich wszystkich

    }





    // Update is called once per frame
    void Update()
    {
        //tu będziesz chciała 

        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        if (distance <= 30)
        {
            // W tym miejscu nie chcesz żeby przeciwnik strzelał, pamiętaj że dystans to jedno ale ponadto, jeśli przeciwnik jest 
            //w zasięgu widzenia kamery to chcemy żeby zaczął raycastować przed i za siebie i strzelać dopiero jak promień trafi w 
            //gracza. Ewentualnie możemy chcieć żeby przestawał strzelać z opóźnieniem. Tzn, żeby nadal strzelał przez sekundę, czy
            // po tym jak gracz zniknie z linii strzału. Ogólnie to wszystkie funkcje AI takie jak sprawdzanie czy gracz jest na
            //linii strzału polecam robić w innych funkcjach które są wywoływane stąd, czyli jeśli przeciwnik jest w zasiegu kamery.
            //Pamiętaj że to sprawdzanie dystansu musisz robić w pętli dla wszystkich przeciwników, któzy żyja. Nwm czy mieliście
            //ale polecam pętlę foreach(TypElementuKolekcji nazwa in NazwaKolekcji)
            //np. foreach(GameObject enemy in enemies)
            //{
            //if(Distanc.player.transform.position,enemy.transform.position<=30){coś tam, coś tam}
            //}
            Shoot(); 
            
        }
        

        //Nie wiem, może da się coś zrobić, żeby Shoot dla Weapon działało też tu, public przy weapon nie zadziałało
    }


    //np. funkcja 
    //private void CheckLineOfFire(GameObject enemy)
    //{
        //sprawdza co ma, jeśli gracz jest na linii strzału woła 
        //strzał.
        //Coś w stylu 
        //enemy.GetComponent<Weapon>().Shoot();
        
    //}


    // pamiętaj też że musisz mieć fukncję która obróci przeciwnika w stronę z której wykrył gracza, żeby nie strzelał do przodu
    //jak gracz jest za nim

}

