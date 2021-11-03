using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    // Game Object do qual a camera irá seguir
    public GameObject target;

    // Posição x do Game Object que está sendo seguido
    float targetX;

    [SerializeField]
    // Smooth da camera
    float smooth;

    // Velocidade do smooth da camera
    float velocity;

    // Posição da qual a camera irá
    float newPosX;

    void FixedUpdate()
    {
        targetX = target.transform.position.x;
        float mov = 0;
        float delta = targetX - transform.position.x;
        if (delta > GlobalManager.limit_Right)
        {
            mov = delta - GlobalManager.limit_Right;
        }

        if (delta < GlobalManager.limit_Left)
        {
            mov = delta - GlobalManager.limit_Left;
        }
        transform.Translate(mov, 0, 0);

        newPosX = Mathf.SmoothDamp(transform.position.x, transform.position.x + mov,
            ref velocity, smooth);

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

    }

}















