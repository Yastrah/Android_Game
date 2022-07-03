using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int health;
    
    [HideInInspector] public Controller controller;
    
    private Rigidbody2D rb;
    private Vector2 moveInput; // вектор объекта. Считывает в каком направлении объект движется
    private GameObject objAnimation; // объект со всей отрисовкой и анимациями
    private Animator anim;
    private Inventory inventory;

    private Notes.Effect effect;
    private bool facingRight = true;

    private Push push;
    private bool isPushing = false;
    private float pushSpeed;
    private Dictionary<string, float> pushInfo = new Dictionary<string, float>() { ["length"] = 1.4f, ["speed"] = 5f };

    private void Start()
    {
        controller = GetComponent<Controller>();
        rb = GetComponent<Rigidbody2D>();
        objAnimation = Notes.findChildByName(gameObject, "Animation");
        anim = objAnimation.GetComponent<Animator>(); // получение анимаций
        inventory = GetComponent<Inventory>();
    }

    private void Update() // вся логика перед новым кадром 
    {
        if(isPushing) {
            push.OnPushStay(ref moveInput, ref pushSpeed);

            if(pushSpeed <= 0.1) {
                isPushing = false;
                push = null;
            }
        }
        else {
            moveInput = new Vector2(controller.joystick.Horizontal, controller.joystick.Vertical); // считывае горизонтальное и вертикальное движение джостика
        }

        DrawLogic(); // проверка логики отрисовки

        if(health <= 0) { Death(); } // обработка смерти
    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        if(inventory.isOpen == true) {
            rb.velocity = new Vector2(0f, 0f);
        }
        else if(isPushing) {
            rb.velocity = new Vector2(moveInput.x, moveInput.y) * pushSpeed; // присваивание скорости
        }
        else {
            rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed; // присваивание скорости
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log(other.gameObject.name);
        // if(isPushing) {
        //     isPushing = false;
        //     push = null;
        // }

        if(other.gameObject.tag == "Enemy") {
            isPushing = true;
            push = new Push(pushInfo, transform, other.transform, ref pushSpeed);
        }
        
    }

    // public void Fire() {
    //     Notes.findActiveChildWithTag(gameObject, "Weapon").GetComponent<Weapon>().Fire();
    // }

    public void TakeDamage(int damage, Notes.Effect effect) {
        health -= damage;
    }

    private void Death() { // действия при смерти игрока
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Debug.Log("Player dead");
        gameObject.SetActive(false);
    }

    private void DrawLogic() { // проверка состояния для изменения активной анимации и отрисовки спрайта
        if((!facingRight && moveInput.x > 0) || (facingRight && moveInput.x < 0)) { Flip(); } // проверка на необходимость развернуть спрайт
        anim.SetBool("isRunning", moveInput.x != 0 || moveInput.y != 0); // отслеживание состояния для анимаций run\idle
    }

    private void Flip() { // разворот спрайта относительно Х
        facingRight = !facingRight;

        Vector3 Scaler = objAnimation.transform.localScale;
        Scaler.x *= -1;
        objAnimation.transform.localScale = Scaler;
    }
    
}