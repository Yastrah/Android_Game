using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Основные объекты")]
    [Tooltip("Основной спрайт")]
    [SerializeField] private Sprite weaponSprite;
    public Sprite WeaponSprite { get { return weaponSprite; } }

    [Tooltip("Название оружия")]
    [SerializeField] private string weaponName;
    public string Name { get { return weaponName; } }


    [Header("Параметры оружия")]
    [Tooltip("Класс оружия")]
    [SerializeField] private Weapon.WeaponType weaponClass;
    public Weapon.WeaponType WeaponClass { get { return weaponClass; } }

    [Tooltip("время между выcтрелами/ударами")]
    [SerializeField] private float coolDown;
    public float CoolDown { get { return coolDown; } }


    [Header("Параметры пули")]
    [SerializeField] BulletData bulletSettings;
    public BulletData BulletSettings { get { return bulletSettings; } }


    [Header("Дополнительное")]
    [Tooltip("Стандартная цена")]
    [SerializeField] private int price;
    public int Price { get { return price; } }

    [Tooltip("Описание")]
    [Multiline(10)]
    [SerializeField] private string description;
    public string Description { get { return description; } }


    /*[Tooltip("Урон")]
    [SerializeField] private int damage1;

    [Tooltip("Урон")]
    [SerializeField] private int damage2;*/

}
