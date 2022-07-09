using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Параметры")]
    [Tooltip("Объект с параметрами")]
    public WeaponData weaponData;

    protected WeaponType weaponClass;
    protected float coolDown;

    protected float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    protected Controller controller;

    /// <summary>
    /// Все типы оружия 
    /// </summary>
    public enum WeaponType {
        AssaultRifle, // штурмовая винтовка
        SniperRifle, // снайперская винтовка
        Pistol, // пистолет
        Sword, // меч
    }

    protected void Start() 
    {
        controller = FindObjectOfType<Controller>();
        reloadTime = coolDown;

        coolDown = weaponData.CoolDown;
        weaponClass = weaponData.WeaponClass;
    }

}
