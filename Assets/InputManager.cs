using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class InputManager : MonoBehaviour
{
    public Transform playerTransform;
    public Scrollbar barraCooldownDash;
    public Camera camara;
    public int velocidad;

    private Vector3 destino;
    private float lastClickTime = 0f;  
    private float doubleClickTime = 0.3f;
    private float lastSpaceTime = 0f;
    private float spaceCooldown = 1f;

    void Start()
    {
        destino = playerTransform.position;
        camara = Camera.main;
    }

    void Update()
    {
        barraCooldownDash.size = Time.time - lastSpaceTime;
        //Doble click para mover
        playerTransform.position = new Vector3(playerTransform.position.x,playerTransform.position.y,0);
        Vector3 direccion = (destino - playerTransform.position).normalized;
        playerTransform.Translate(direccion * Time.deltaTime * velocidad, Space.World);
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < doubleClickTime) 
            {
                Ray ray = camara.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    destino = hit.point;
                }
            }

            lastClickTime = Time.time; 
        }

        //Dash
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSpaceTime > spaceCooldown)
            {
                playerTransform.position += direccion.normalized * 2.5f;
                lastSpaceTime = Time.time;
            }
        }
    }
}


