using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseController : MonoBehaviour
{


	// Start is called before the first frame update
	void Start()
    {
    }


	// Update is called once per frame
	void Update()
    {

        if (GameController.AllowGameControlls)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GameController.gameController.PC.GetComponent<Character>().Jump();
            }
            float moveInput = Input.GetAxis("Horizontal");
            GameController.gameController.PC.GetComponent<Character>().Move(moveInput);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameController.gameController.PC.GetComponent<Weapon>().Shoot();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.gameController.PC.GetComponent<Weapon>().Reload();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    GameController.gameController.PC.GetComponent<Character>().ExecuteShift(true);
                }
                else
                {
                    GameController.gameController.PC.GetComponent<Character>().ExecuteShift(false);
                }

            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameController.gameController.PC.GetComponent<Character>().INVINCIBLECHEAT = !GameController.gameController.PC.GetComponent<Character>().INVINCIBLECHEAT;
            }


            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Camera.main.GetComponent<CameraController>().ChangeCameraMode(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Camera.main.GetComponent<CameraController>().ChangeCameraMode(2);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Camera.main.GetComponent<CameraController>().ChangeCameraMode(3);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Camera.main.GetComponent<CameraController>().ChangeCameraMode(4);
            }
            if(Input.GetKeyUp(KeyCode.RightArrow)|| Input.GetKeyUp(KeyCode.DownArrow)|| Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.UpArrow))
            {
                Camera.main.GetComponent<CameraController>().ChangeCameraMode(0);
            }
        }
       


        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.menu.pauseMenu.activeSelf)
            {
                GameController.menu.btnResumeInPauseMenu();
            }
            else
            {
                GameController.menu.showPauseMenu();
            }
            
        }

    }
    
    
   

}
