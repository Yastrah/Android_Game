using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("�������� �������")]
    [Tooltip("�������� ������")]
    [SerializeField] private Sprite weaponSprite;
    public Sprite WeaponSprite { get { return weaponSprite; } }

    [Tooltip("�������� ������")]
    [SerializeField] private string weaponName;
    public string Name { get { return weaponName; } }


    [Header("��������� ������")]
    [Tooltip("����� ������")]
    [SerializeField] private Weapon.WeaponType weaponClass;
    public Weapon.WeaponType WeaponClass { get { return weaponClass; } }

    [Tooltip("����� ����� ��c�������/�������")]
    [SerializeField] private float coolDown;
    public float CoolDown { get { return coolDown; } }


    [Header("��������� ����")]
    [SerializeField] BulletData bulletSettings;
    public BulletData BulletSettings { get { return bulletSettings; } }


    [Header("��������������")]
    [Tooltip("����������� ����")]
    [SerializeField] private int price;
    public int Price { get { return price; } }

    [Tooltip("��������")]
    [Multiline(10)]
    [SerializeField] private string description;
    public string Description { get { return description; } }


    /*[Tooltip("����")]
    [SerializeField] private int damage1;

    [Tooltip("����")]
    [SerializeField] private int damage2;*/

}
