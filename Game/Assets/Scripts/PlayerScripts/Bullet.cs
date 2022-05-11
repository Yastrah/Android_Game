using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public int solidLayer; // что пуля будет считать твердым (номер LayerMask)
    public Notes.BulletEffect effect;

    private void Start() {
        Invoke("DestroyBullet", lifeTime); // вызов уничтожения пули по истечении lifeTime
    }

    private void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // перемещение пули
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == solidLayer) { // проверка на твёрдость слоя
            if(other.CompareTag("Player")) {
                return;
            }
            
            if(other.CompareTag("Enemy")) { // проверка конкретного тега твёрдого объекта
                Debug.Log("hit Enemy");
                other.GetComponent<Enemy>().TakeDamage(damage, effect); // вызов функции получения урона
            }
            
            DestroyBullet();
        }
    }

    private void DestroyBullet() { // функция уничтожения пули. добавить эффект
        Destroy(gameObject);
    }

}
