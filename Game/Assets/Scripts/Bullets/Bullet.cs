using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [Header("Параметры пули")]
    [Tooltip("Время, после которого пуля уничтожается")]
    [SerializeField] protected float lifeTime;

    [Tooltip("Скорость пули")]
    [SerializeField] protected float speed;

    [Tooltip("Урон пули")]
    [SerializeField] protected int damage;

    [Tooltip("Еффект пули")]
    [SerializeField] protected Notes.Effect effect;

    [Header("Объекты взаимодействия")]
    [Tooltip("Слой, с которым пуля будет взаимодействовать")]
    [SerializeField] protected LayerMask solidLayer;

    [Tooltip("Тег объекта, которому пуля будет наносить урон")]
    [SerializeField] protected GameTag target;

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
    /// Имя объекта, выпустившего пулю
    /// </summary>
    public string PatentName
    {
        get
        {
            return this.parentName;
        }

        set
        {
            this.parentName = value;
        }
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
                        // вызов функции получения урона
                        other.GetComponent<Player>().TakeDamage(damage, effect);
                        break;
                    case GameTag.Enemy:
                        // вызов функции получения урона
                        other.GetComponent<Enemy>().TakeDamage(damage, effect);
                        break;
                }

            }

            DestroyBullet();
        }
    }

    /// <summary>
    /// Уничтожения пули
    /// </summary>
    protected void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
