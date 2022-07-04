 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Параметры оружия")]
    [Tooltip("Тип оружия")]
    [SerializeField] protected WeaponType weaponType;

    [Tooltip("время между выcтрелами/ударами")]
    [SerializeField] protected float coolDown;

    protected float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    protected Controller controller;

    /// <summary>
    /// Все типы оружия 
    /// </summary>
    protected enum WeaponType {
        AssaultRifle, // штурмовая винтовка
        SniperRifle, // снайперская винтовка
        Pistol, // пистолет
        Sword, // меч
    }

    protected void Start() 
    {
        controller = FindObjectOfType<Controller>();
        reloadTime = coolDown;
    }

}
