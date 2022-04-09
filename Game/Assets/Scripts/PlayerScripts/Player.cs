using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public float speed;
    public GameObject texture;
    
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput; // вектор объекта. Считывает в каком направлении объект движется
    private Animator anim;
    private Gun gun;


    private void Start()
    {
        texture.SetActive(false); // отключение статичной текстуры
        //gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        gun = findChildrenWithTag(gameObject, "Gun")[0].GetComponent<Gun>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // получение анимаций
        //print(transform.childCount);
    }

    private void Update() // вся логика перед новым кадром 
    {
        moveInput = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical); // считывае горизонтальное и вертикальное движение джостика
        
        anim.SetBool("isRunning", moveInput.x != 0 || moveInput.y != 0); // отслеживание состояния
        
        if((!facingRight && moveInput.x > 0) || (facingRight && moveInput.x < 0)) { Flip(); } // проверка на необходимость развернуть спрайт
    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;
    }

    // private void OnTriggerEnter2D(Collider2D other) {}
    // private void OnCollisionEnter2D(Collision2D other) {}

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        Vector3 GunScaler = gun.transform.localScale;
        GunScaler.x *= -1;
        gun.transform.localScale = GunScaler;
    }

    private List<GameObject> findChildrenWithTag(GameObject parent, string tag) {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        List<GameObject> childrenWithTag = new List<GameObject>(); 
        foreach(Transform t in children) {
            if(t.gameObject.tag == tag) {
                childrenWithTag.Add(t.gameObject);
            }
        }
        return childrenWithTag;
    }
    

}