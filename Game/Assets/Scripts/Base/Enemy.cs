using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    
    protected Rigidbody2D rb;
    protected GameObject objAnimation; // объект со всей отрисовкой и анимациями
    protected Animator anim;
    protected Player player;
    protected Vector2 move;
    protected StatusType status;
    // protected Weapon weapon;

    private bool facingRight = true;

    protected enum StatusType {
        Aggressive,
        Passive
    }

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objAnimation = Notes.findChildByName(gameObject, "Animation");
        anim = objAnimation.GetComponent<Animator>(); // получение анимаций
        // weapon = Notes.findChildWithTag(gameObject, "Weapon").GetComponent<Weapon>();
        player = FindObjectOfType<Player>();
    }

    protected void Update()
    {
        DrawLogic(); // проверка логики отрисовки

        if(health <= 0) { Death(); } // обработка смерти
    }


    protected void PassiveBehaviour() { // пассивное поведение
        // write
    }

    public void TakeDamage(int damage, Notes.Effect effect) {
        health -= damage;
    }

    protected void Death() { // действия при смерти противника
        Debug.Log("Enemy dead");
        Destroy(gameObject);
    }

    protected void DrawLogic() { // проверка состояния для изменения активной анимации и отрисовки спрайта
        if(player.transform.position.x > transform.position.x && !facingRight) { // поворот в сторону игрока
            Flip();
        }
        else if(player.transform.position.x < transform.position.x && facingRight){
            Flip();
        }

        anim.SetBool("isRunning", move.x != 0 || move.y != 0);
    }

    protected void Flip() { // разворот спрайта относительно Y
        facingRight = !facingRight;

        Vector3 Scaler = objAnimation.transform.localScale;
        Scaler.x *= -1;
        objAnimation.transform.localScale = Scaler;
    }

}
