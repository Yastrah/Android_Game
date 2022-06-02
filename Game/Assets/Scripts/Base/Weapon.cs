 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponType type; // тип оружия
    [SerializeField] protected float coolDown; // время между выcтрелами/ударами в СОТЫХ СЕКУНДЫ
    [SerializeField] protected ControlType controller; // тип управления (для кого скрипт)

    protected float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    protected Player player;

    public enum ControlType { // тип управления оружием
        Joystick,
        FollowPlayer
    }

    protected enum WeaponType {
        AssaultRifle, // штурмовая винтовка
        SniperRifle, // снайперская винтовка
        Pistol, // пистолет
        Sword, // меч
    }

    protected void Start() {
        player = FindObjectOfType<Player>();
        reloadTime = coolDown;
    }

}
