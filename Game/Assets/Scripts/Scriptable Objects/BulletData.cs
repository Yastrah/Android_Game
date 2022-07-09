using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletData
{
    [Tooltip("Класс пули")]
    [SerializeField] private BulletType bulletClass;
    public BulletType BulletClass { get { return bulletClass; } }

    [Tooltip("Стандартный урон")]
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    [Tooltip("Стандартная скорость")]
    [SerializeField] private int speed;
    public int Speed { get { return speed; } }

    [Tooltip("Шанс критического урона")]
    [Range(0f, 1f)]
    [SerializeField] protected float criticaShot;
    public float CriticalShot { get { return criticaShot; } }

    [Tooltip("Время самоуничтожения")]
    [SerializeField] private float lifeTime;
    public float LifeTime { get { return lifeTime; } }

    /// <summary>
    /// Перечень возможных типов пуль
    /// </summary>
    public enum BulletType
    {
        Default, // Стандартная пуля
        Explosion, // Взрывающаяся пуля (при столкновении образуется взрыв)
        Splitting // Разделяющаяся пуля
    }
}
