using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [Header("Параметры пули")]
    [Tooltip("Еффект пули")]
    [SerializeField] protected Notes.Effect effect;

    [Header("Объекты взаимодействия")]
    [Tooltip("Тег объекта, которому пуля будет наносить урон")]
    [SerializeField] protected GameTag target;

    [Tooltip("Слой, с которым пуля будет взаимодействовать")]
    [SerializeField] protected LayerMask solidLayer;

    protected int damage;
    protected float speed;
    protected float criticaShot;
    protected float lifeTime;
    protected string parentName = ""; // имя объекта, пустившего пулю

    /// <summary>
    /// Перечень тегов возможных целей
    /// </summary>
    public enum GameTag
    {
        Player,
        Enemy
    }

    /// <summary>
    /// Функция, задающая значения при создании пули
    /// </summary>
    public void Init(BulletData data, string parentName)
    {
        damage = data.Damage;
        speed = data.Speed;
        criticaShot = data.CriticalShot;
        lifeTime = data.LifeTime;
        this.parentName = parentName;
    }

    protected void Start()
    {
        // вызов уничтожения пули по истечении lifeTime
        Invoke("DestroyBullet", lifeTime);
    }

    protected void Update()
    {
        // перемещение пули
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        // проверка на твёрдость слоя
        if (other.gameObject.layer == Math.Log(solidLayer.value, 2))
        {
            // проверка на колайдер объекта выпустившего пулю 
            if (other.gameObject.name == parentName)
            {
                return;
            }

            // проверка на колайдер объекта, с котором отключено взаимодействие
            if (other.CompareTag("Non-Trigger"))
            {
                return;
            }

            // проверка конкретного тега твёрдого объекта
            if (other.CompareTag(target.ToString()))
            {
                // Debug.Log($"hit {target.ToString()}");

                switch (target)
                {
                    case GameTag.Player:
                        // Проверка на критическое попадание
                        if(UnityEngine.Random.value <= criticaShot) {
                            // вызов функции получения урона
                            other.GetComponent<Player>().TakeDamage(damage*2, effect);
                            CriticalShot();
                        }
                        else
                        {
                            // вызов функции получения урона
                            other.GetComponent<Player>().TakeDamage(damage, effect);
                        }
                        break;

                    case GameTag.Enemy:
                        // Проверка на критическое попадание
                        if (UnityEngine.Random.value <= criticaShot)
                        {
                            // вызов функции получения урона
                            other.GetComponent<Enemy>().TakeDamage(damage * 2, effect);
                            CriticalShot();
                        }
                        else
                        {
                            // вызов функции получения урона
                            other.GetComponent<Enemy>().TakeDamage(damage, effect);
                        }
                        break;
                }

            }

            DestroyBullet();
        }
    }

    /// <summary>
    /// Действия при критическом попадании
    /// </summary>
    protected void CriticalShot()
    {

    }

    /// <summary>
    /// Уничтожения пули
    /// </summary>
    protected void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
