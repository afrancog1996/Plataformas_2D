using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int monedas = 0;

    public PlayerMovement playerMovement;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteBlanco;
    public Sprite spriteTriste;

    public Animator anim;

    private int vidas = 3;
    private bool esInmune = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Colision con:"+ other.name); 
        if(other.tag == Constantes.TAG_MONEDA)
        {
            other.gameObject.SetActive(false);

            monedas++;
            Debug.Log("Monedas;" + monedas);  
        }else if(other.tag == Constantes.TAG_ENEMIGO){
            Debug.Log("Enemigo!!!");
            if(!esInmune){
                HacerDaño();
            }
        }
    }

    private void HacerDaño()
    {
        esInmune = true;
        vidas--;
        if(vidas <=0)
        {
            //murio 
        }
        

        playerMovement.bloquearMovimiento = true;
        playerMovement.rb.velocity = Vector2.zero;
        
        anim.enabled = false;
        spriteRenderer.sprite = spriteBlanco;
        
        if(playerMovement.estaVolteandoADerecha){
            playerMovement.rb.AddForce(new Vector2(-70, 330));
        }else{
            playerMovement.rb.AddForce(new Vector2(70, 330));
        }
        
        InvokeRepeating("Parpadeo", 0, 0.1f);
        Invoke("QuitarSpriteBlanco", 0.4f);

        Invoke("RegresoNormalidad",1);
    }

    private void Parpadeo()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    private void QuitarSpriteBlanco()
    {
        spriteRenderer.sprite = spriteTriste;
    }

    private void RegresoNormalidad()
    {
        esInmune = false;

        spriteRenderer.enabled = true;

        playerMovement.rb.velocity = Vector2.zero;
        playerMovement.bloquearMovimiento = false;
        CancelInvoke("Parpadeo");

        anim.enabled = true;
    }
}
