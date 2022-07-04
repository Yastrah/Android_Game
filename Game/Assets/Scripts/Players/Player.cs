using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [Header("Параметры игрока")]
    [Tooltip("Здоровье игрока")]
    [SerializeField] private int health;

    [Tooltip("Скорость игрока")]
    [SerializeField] private float speed;

    [HideInInspector] public Controller controller;
    
    private Vector2 moveInput; // вектор объекта. Считывает в каком направлении объект движется
    private Rigidbody2D rb;
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

    private void Update()
    {
        if(isPushing)
        {
            // обработка отталкивания
            push.OnPushStay(ref moveInput, ref pushSpeed);

            if(pushSpeed <= 0.1)
            {
                isPushing = false;
                push = null;
            }
        }
        else
        {
            // считывает горизонтальное и вертикальное движение джостика
            moveInput = new Vector2(controller.joystick.Horizontal, controller.joystick.Vertical);
        }

        // проверка логики отрисовки
        DrawLogic();

        // обработка смерти
        if (health <= 0) { Death(); }
    }

    private void FixedUpdate()
    {
        if(inventory.isOpen == true)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else if(isPushing)
        {
            rb.velocity = new Vector2(moveInput.x, moveInput.y) * pushSpeed;
        }
        else
        {
            rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Обработка прикосновения к противнику
        if (other.gameObject.tag == "Enemy")
        {
            isPushing = true;
            push = new Push(pushInfo, transform, other.transform, ref pushSpeed);
        }
        
    }

    /// <summary>
    /// Получние урона игроком
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="effect"></param>
    public void TakeDamage(int damage, Notes.Effect effect)
    {
        health -= damage;
    }

    /// <summary>
    /// Обработка смерти игрока
    /// </summary>
    private void Death()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Debug.Log("Player dead");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Проверка состояния и обработка всей отрисовки: анимаций, поворотов и т.п. 
    /// </summary>
    private void DrawLogic()
    {
        // проверка на необходимость развернуть спрайт
        if ((!facingRight && moveInput.x > 0) || (facingRight && moveInput.x < 0)) { Flip(); }

        // отслеживание состояния для анимаций run\idle
        anim.SetBool("isRunning", moveInput.x != 0 || moveInput.y != 0); 
    }

    /// <summary>
    /// разворот спрайта относительно оси Y
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 Scaler = objAnimation.transform.localScale;
        Scaler.x *= -1;
        objAnimation.transform.localScale = Scaler;
    }
    
}