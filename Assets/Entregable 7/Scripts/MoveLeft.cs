using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //determinam velocitat dels obstacles
    private float speed = 10f;

    //cridam a la variable GameOver
    private PlayerController playerControllerScript;

    //limit dels obstacles
    private float leftLim = -30f;
    
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Si GameOver=True els objectes es mouran cap a l'esquerra
        if (!playerControllerScript.GameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        //Destruim els objectes que passin el limit
        if (transform.position.x < leftLim)
        {
            Destroy(gameObject);
        }

    }
}
