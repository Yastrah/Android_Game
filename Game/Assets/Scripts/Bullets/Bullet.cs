using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected LayerMask solidLayer; // что пуля будет считать твердым (номер LayerMask)
    [SerializeField] protected GameTag target; // тэг, объекты которого пуля будет игнорировать
    [SerializeField] protected Notes.Effect effect;

    protected string parentName = ""; // имя объекта, пустившего пулю
    
    public enum GameTag {
        Player,
        Enemy
    }

    public string PatentName {
        get {
            return this.parentName;
        }

        set {
            this.parentName = value;
        }
    }

    protected void Start() {
        Invoke("DestroyBullet", lifeTime); // вызов уничтожения пули по истечении lifeTime
    }

    protected void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // перемещение пули
    }
    
    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == Math.Log(solidLayer.value, 2)) { // проверка на твёрдость слоя
            if(other.gameObject.name == parentName) { // проверка на колайдер объекта выпустившего пулю 
                return;
            }

            if(other.CompareTag("Non-Trigger")) {
                return;
            }
            
            if(other.CompareTag(target.ToString())) { // проверка конкретного тега твёрдого объекта
                Debug.Log($"hit {target.ToString()}");

                switch (target) {
                    case GameTag.Player:
                        other.GetComponent<Player>().TakeDamage(damage, effect); // вызов функции получения урона
                        break;
                    case GameTag.Enemy:
                        other.GetComponent<Enemy>().TakeDamage(damage, effect); // вызов функции получения урона
                        break;
                }
                
            }
            
            DestroyBullet();
        }
    }

    protected void DestroyBullet() { // функция уничтожения пули. добавить эффект
        Destroy(gameObject);
    }
}
