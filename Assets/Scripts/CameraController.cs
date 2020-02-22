using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusObject;

    private Vector2 focusPosition;

    public float followDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.menu.gameOver.activeSelf == false && GameController.menu.quitMenuAfterGameOver.activeSelf == false && GameController.menu.bridgeScreen.activeSelf == false)
        {
            focusPosition = focusObject.transform.position;
            Vector3 distance = focusPosition - (Vector2)transform.position;
            if (distance.magnitude > followDistance)
            {
                Vector3 moveDistance = Vector2.ClampMagnitude(distance, distance.magnitude - followDistance);
                transform.position += new Vector3(moveDistance.x, moveDistance.y, 0);
            }
        }
    }
}
