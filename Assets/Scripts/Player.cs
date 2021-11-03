using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Referência ao rigidbody do gameObject
    Rigidbody2D rb;

    // Velocidade da movimentação horizontal
    [SerializeField]
    float speed;

    // Variavel que representa o animator
    Animator animator;
    
    // Força do pulo
    [SerializeField]
    float jumpForce;

    // Bool que controla se o jogador está em contato com o chão ou não
    string state;

    bool jumpBuffer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GlobalManager.player_Air;
        animator = GetComponent<Animator>();
        jumpBuffer = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Movimentação horizontal
        float h;
        h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        // O input deve ser feito no update
        if (Input.GetButtonDown("Jump")) 
        {
            jumpBuffer = true;
        }


        // Controle das animações
        if (rb.velocity.x > 0) transform.localScale = new Vector3(1,1,1);
        if (rb.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (state == GlobalManager.player_Ground) animator.SetBool("Ground", true);
        else if (state == GlobalManager.player_Air || state == GlobalManager.player_DoubleJump) 
            animator.SetBool("Ground", false);

        if (Input.GetKey(KeyCode.X))
        {
            state = GlobalManager.player_attack;
            animator.SetBool("Porrada", true);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            state = GlobalManager.player_Ground;
            animator.SetBool("Porrada", false);
       
        }


    }

    private void FixedUpdate()
    {

     
        if (jumpBuffer) 
        {
            // Pulo
            if (state == GlobalManager.player_Ground)
            {
                rb.AddForce(Vector2.up * jumpForce);
                state = GlobalManager.player_Air;
            }
            // Pulo duplo
            else if (state == GlobalManager.player_Air) 
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
                state = GlobalManager.player_DoubleJump;
                animator.Play("Zero_Pulandoo", 0, 0);
            }
        }
        jumpBuffer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            state = GlobalManager.player_Ground ; 
   
        }
        
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
        }


        if (state == GlobalManager.player_attack && collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

    }
}










