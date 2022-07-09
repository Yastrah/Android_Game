using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Параметры противника")]
    [Tooltip("Тип противника")]
    [SerializeField] protected Notes.WarriorType enemyType;

    [Tooltip("Здоровье противника")]
    [SerializeField] protected int health;

    [Tooltip("Скорость противника")]
    [SerializeField] protected float speed;

    [Tooltip("Урон игроку от столкновения с ним")]
    [SerializeField] protected int pushDamage;
    
    protected Rigidbody2D rb;
    protected GameObject objAnimation; // объект со всей отрисовкой и анимациями
    protected Animator anim;
    protected Player player;
    protected Vector2 move;
    protected StatusType status;

    protected Push push;
    protected bool isPushing = false;
    protected float pushSpeed;
    private Dictionary<string, float> pushInfo;

    [HideInInspector] public bool isVisible = false;
    private bool facingRight = true;

    /// <summary>
    /// Модель поведения противника
    /// </summary>
    protected enum StatusType
    {
        Aggressive,
        Passive
    }

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objAnimation = Notes.findChildByName(gameObject, "Animation");
        anim = objAnimation.GetComponent<Animator>(); // получение анимаций
        player = FindObjectOfType<Player>();
        pushInfo = new Dictionary<string, float>() { ["length"] = 0.6f, ["speed"] = 3.5f };
    }

    protected void Update()
    {
        if (isVisible)
        {
            status = StatusType.Aggressive;
        }
        else
        {
            status = StatusType.Passive;
        }

        // проверка логики отрисовки
        DrawLogic();

        // обработка смерти
        if (health <= 0) { Death(); }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        // Обработка прикосновения к игроку
        if (other.gameObject.tag == "Player")
        {
            player.TakeDamage(pushDamage, Notes.Effect.None);
            // Debug.Log("push Player");
            isPushing = true;
            push = new Push(pushInfo, transform, other.transform, ref pushSpeed);
        }
    }

    /// <summary>
    /// Пассивная модель поведения противника
    /// </summary>
    protected void PassiveBehaviour()
    {
        // write
    }

    /// <summary>
    /// Получние урона противником
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="effect"></param>
    public void TakeDamage(int damage, Notes.Effect effect)
    {
        health -= damage;
    }

    /// <summary>
    /// Обработка смерти противника
    /// </summary>
    protected void Death()
    { // действия при смерти противника
        // Debug.Log("Enemy dead");
        Destroy(gameObject);
    }

    /// <summary>
    /// Проверка состояния и обработка всей отрисовки: анимаций, поворотов и т.п. 
    /// </summary>
    protected void DrawLogic()
    {
        // поворот в сторону игрока
        if (player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }

        // отслеживание состояния для анимаций run\idle
        anim.SetBool("isRunning", move.x != 0 || move.y != 0);
    }

    /// <summary>
    /// разворот спрайта относительно оси Y
    /// </summary>
    protected void Flip()
    {
        facingRight = !facingRight;

        Vector3 Scaler = objAnimation.transform.localScale;
        Scaler.x *= -1;
        objAnimation.transform.localScale = Scaler;
    }

}
