    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected int solidLayer; // что пуля будет считать твердым (номер LayerMask)
    [SerializeField] protected GameTag ignore; // тэг, объекты которого пуля будет игнорировать
    [SerializeField] protected Notes.Effect effect;

    protected GameTag hit; // тэг, с объектами которого пуля будет взаимодействовать

    public enum GameTag {
        Player,
        Enemy
    }

    protected void Start() {
        Invoke("DestroyBullet", lifeTime); // вызов уничтожения пули по истечении lifeTime

        switch (ignore) {
            case GameTag.Player:
                hit = GameTag.Enemy;
                break;
            case GameTag.Enemy:
                hit = GameTag.Player;
                break;
        }
    }

    protected void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // перемещение пули
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == solidLayer) { // проверка на твёрдость слоя
            if(other.CompareTag(ignore.ToString())) {
                return;
            }
            
            if(other.CompareTag(hit.ToString())) { // проверка конкретного тега твёрдого объекта
                Debug.Log($"hit {hit.ToString()}");

                switch (hit) {
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
