using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public float velocidad;
    public float fuerzaSalto;

    private float horizontal;
    private bool estaEnSuelo;
    public bool bloquearMovimiento = false;

    void Start()
    {
    Debug.Log("Hola mundo");
    }
    
    void Update()
    {
       if (bloquearMovimiento){ return; }
       CheckSuelo();
       CheckMovimiento();
       CheckJump();
       Voltear();

       anim.SetBool("IsJumping", !estaEnSuelo);
    }
    private void CheckMovimiento()
    {
        //0 = no moverse
        //1 = moverse a la DERECHA
        //-1 = moverse  la IZQUIERDA

        horizontal= Input.GetAxisRaw("Horizontal");
        rb.velocity= new Vector2(horizontal * velocidad,rb.velocity.y);

        anim.SetBool("IsMoving", horizontal !=0);
    }
    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
            {
                //saltar
                //Debug.Log("salta!!!");
                rb.velocity=new Vector2(rb.velocity.x,fuerzaSalto);
             };
    }

    private void CheckSuelo()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
        {
            estaEnSuelo=true;
        }
        else{
            estaEnSuelo = false;
        }
        //Debug.Log("esta saltando" + estaEnSuelo);       
    }

    public bool estaVolteandoADerecha = true;

    private void Voltear(){
        if ((horizontal < 0 && estaVolteandoADerecha) || (horizontal>0 && !estaVolteandoADerecha))
        {
           estaVolteandoADerecha = !estaVolteandoADerecha;
           transform.Rotate(0,180,0);
        }
    }
}
