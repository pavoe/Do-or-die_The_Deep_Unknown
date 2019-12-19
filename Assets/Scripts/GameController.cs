using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static  GameController gameController { get; private set; }



    public float timeStopSpeed = 0.01f;
    /// <summary>
    /// Holds the reference to the Level the player is currently on.
    /// </summary>
    public static GameObject ActiveLevel { get; protected set; }

    /// <summary>
    /// This holds the reference to the player character Game Object
    /// </summary>
    public  GameObject PlayerCharacter;


    // Start is called before the first frame update
    void Start()
    {
        gameController = this;
        // TEMPORARY!!!
        ActiveLevel = GameObject.Find("Level1");
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }
}
