 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponType weaponType; // тип оружия
    [SerializeField] protected float coolDown; // время между выcтрелами/ударами

    protected float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    protected Controller controller;

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

    // public virtual void Fire() { // функцие вызывающаяся при нажатии на кнопку стрельбы
    //     Debug.Log(1);
    // }

}
