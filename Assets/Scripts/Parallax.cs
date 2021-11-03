using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    // Posição da camera
    Transform cam;

    [SerializeField]
    // Velocidade da movimentação da imagem
    float mov;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(cam.position.x * mov, transform.position.y);   
    }
}
