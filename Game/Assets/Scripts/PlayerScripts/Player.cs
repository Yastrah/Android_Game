using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public GameObject texture; // статичная текстура

    public float speed;
    public int health;
    
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput; // вектор объекта. Считывает в каком направлении объект движется
    private Animator anim;
    private Weapon gun;
    private Inventory inventory;
    private Notes.Effect effect;


    private void Start()
    {
        texture.SetActive(false); // отключение статичной текстуры
        gun = Notes.findActiveChildWithTag(gameObject, "Weapon").GetComponent<Weapon>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // получение анимаций
        inventory = GetComponent<Inventory>();
        // Debug.Log(texture.activeSelf); // проверка на активность объекта
        // Debug.Log(texture.activeInHierarchy); // относительно всей сцены
    }

    private void Update() // вся логика перед новым кадром 
    {
        moveInput = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical); // считывае горизонтальное и вертикальное движение джостика
        
        DrawLogic(); // проверка логики отрисовки

        if(health <= 0) { Death(); } // обработка смерти
    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        if(inventory.isOpen == true) {
            rb.velocity = new Vector2(0f, 0f);
        }
        else {
            rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed; // присваивание скорости
        }
    }

    // private void OnTriggerEnter2D(Collider2D other) {}
    // private void OnCollisionEnter2D(Collision2D other) { Debug.Log(other.gameObject.name); }
    // Debug.Log("информация"); // вывод вспомогательной информации в консоль
    // transform.eulerAngles = new Vector3(0, 180, 0); // альтернативный разворот

    public void TakeDamage(int damage, Notes.Effect effect) {
        health -= damage;
    }

    private void Death() { // действия при смерти игрока
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("Player dead");
    }

    private void DrawLogic() { // проверка состояния для изменения активной анимации и отрисовки спрайта
        if((!facingRight && moveInput.x > 0) || (facingRight && moveInput.x < 0)) { Flip(); } // проверка на необходимость развернуть спрайт
        anim.SetBool("isRunning", moveInput.x != 0 || moveInput.y != 0); // отслеживание состояния для анимаций run\idle
    }

    private void Flip() { // разворот спрайта относительно Х
        facingRight = !facingRight;

        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        Vector3 GunScaler = gun.transform.localScale;
        GunScaler.x *= -1;
        gun.transform.localScale = GunScaler;
    }


    
}