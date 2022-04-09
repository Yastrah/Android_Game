using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public int solidObjectsLayer; // что пуля будет считать твердым

    private void Start() {
        Invoke("DestroyBullet", lifeTime); // вызов уничтожения пули по истечении lifeTime
    }

    private void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == solidObjectsLayer) {
            DestroyBullet();
        }
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }

}
