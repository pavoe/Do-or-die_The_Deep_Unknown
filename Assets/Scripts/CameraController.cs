using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusObject;

    private Vector2 focusPosition;


    private Vector3 Velocity;
    //0-player, 1-right, 2-down, 3-left, 4-up
    public int CameraMode = 0;


    // Update is called once per frame
    void Update()
    {
        if (GameController.menu.gameOver.activeSelf == false && GameController.menu.quitMenuAfterGameOver.activeSelf == false && GameController.menu.bridgeScreen.activeSelf == false)
        {
            
            switch(CameraMode)
            {
                case 0:
                    focusPosition = focusObject.transform.position;
                    break;
                case 1:
                    focusPosition = new Vector2(focusObject.transform.position.x+(Camera.main.orthographicSize*Camera.main.aspect),focusObject.transform.position.y);
                    break;
                case 2:
                    focusPosition = new Vector2(focusObject.transform.position.x , focusObject.transform.position.y - Camera.main.orthographicSize);
                    break;
                case 3:
                    focusPosition = new Vector2(focusObject.transform.position.x -(Camera.main.orthographicSize * Camera.main.aspect), focusObject.transform.position.y);
                    break;
                case 4:
                    focusPosition = new Vector2(focusObject.transform.position.x, focusObject.transform.position.y + Camera.main.orthographicSize);
                    break;
            }

                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(focusPosition.x,focusPosition.y,transform.position.z),ref Velocity, 0.2f);
        }
    }


    public void ChangeCameraMode(int i)
    {
        if (i >= 0 && i <= 4)
        {
            CameraMode = i;
        }
        else
        {
            throw new Exception("You've done fucked up mate.");
        }
    }
}
