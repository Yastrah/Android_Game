using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletData
{
    [Tooltip("����� ����")]
    [SerializeField] private BulletType bulletClass;
    public BulletType BulletClass { get { return bulletClass; } }

    [Tooltip("����������� ����")]
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    [Tooltip("����������� ��������")]
    [SerializeField] private int speed;
    public int Speed { get { return speed; } }

    [Tooltip("���� ������������ �����")]
    [Range(0f, 1f)]
    [SerializeField] protected float criticaShot;
    public float CriticalShot { get { return criticaShot; } }

    [Tooltip("����� ���������������")]
    [SerializeField] private float lifeTime;
    public float LifeTime { get { return lifeTime; } }

    /// <summary>
    /// �������� ��������� ����� ����
    /// </summary>
    public enum BulletType
    {
        Default, // ����������� ����
        Explosion, // ������������ ���� (��� ������������ ���������� �����)
        Splitting // ������������� ����
    }
}
