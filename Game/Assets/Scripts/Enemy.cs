using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject texture;
    public bool facingRight = true;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    private Gun gun;


    private void Start()
    {
        texture.SetActive(false); // отключение статичной текстуры
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // получение анимаций
    }

    private void Update() {
        moveInput = new Vector2(0f, 0f);

        anim.SetBool("isRunning", moveInput.x != 0 || moveInput.y != 0);


    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
