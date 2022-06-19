 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponType weaponType; // тип оружия
    [SerializeField] protected float coolDown; // время между выcтрелами/ударами

    protected float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    protected Player player;

    protected enum WeaponType {
        AssaultRifle, // штурмовая винтовка
        SniperRifle, // снайперская винтовка
        Pistol, // пистолет
        Sword, // меч
    }

    protected void Start() 
    {
        player = FindObjectOfType<Player>();
        reloadTime = coolDown;
    }

}
