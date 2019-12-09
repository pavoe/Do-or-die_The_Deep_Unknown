using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int score;
    private int scoreLimit;

    public static GameObject CurrentLevel { get; private set; }

    


    public float timeStopSpeed = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = GameObject.Find("WarpedCave");
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }
}
