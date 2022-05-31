using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject texture;
    
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    private Weapon weapon;
    private Player player;


    private void Start()
    {
        texture.SetActive(false); // отключение статичной текстуры
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // получение анимаций
        weapon = Notes.findChildWithTag(gameObject, "Weapon").GetComponent<Weapon>();
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        moveInput = new Vector2(0f, 0f);
        anim.SetBool("isRunning", moveInput.x != 0 || moveInput.y != 0);

        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if(player.transform.position.x > transform.position.x && !facingRight) { // поворот в сторону игрока
            Flip();
        }
        else if(player.transform.position.x < transform.position.x && facingRight){
            Flip();
        }
        

        if(health <= 0) { // действие при смерти противника
            Debug.Log("Enemy dead");
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;
        //rb.velocity = new Vector2(player.transform.position.x, player.transform.position.y) * speed;
    }


    public void TakeDamage(int damage, Notes.Effect effect) {
        health -= damage;
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        Vector3 GunScaler = weapon.transform.localScale;
        GunScaler.x *= -1;
        weapon.transform.localScale = GunScaler;
    }


}
