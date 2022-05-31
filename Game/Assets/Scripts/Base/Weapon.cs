using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float coolDown; // время между выcтрелами/ударами в СОТЫХ СЕКУНДЫ
    [SerializeField] protected ControlType controller; // тип управления (для кого скрипт)

    protected float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    protected Player player;

    public enum ControlType { // тип управления оружием
        Joystick,
        FollowPlayer
    }

    protected void Start() {
        player = FindObjectOfType<Player>();
        reloadTime = coolDown;
    }

}
