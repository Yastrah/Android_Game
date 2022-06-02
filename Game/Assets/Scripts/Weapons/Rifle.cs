using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private GameObject bullet; // объект используемой пули
    [SerializeField] private Transform shotPoint; // дуло (точка, откуда вылетают пули)

    private bool inversion = false; // отражение по y
    private float rotZ; // угол поворота относительно объекта (Игрока, Противника)
    private Vector3 difference; // координаты PLayer оттносительно Enemy

    private void Update() {
        switch (controller)
        {
            case ControlType.Joystick: // скрипт относится к weapon игрока
                if(player.rightJoystick.Horizontal != 0 || player.rightJoystick.Vertical != 0){ // если используется правый джостик
                    rotZ = Mathf.Atan2(player.rightJoystick.Vertical, player.rightJoystick.Horizontal) * Mathf.Rad2Deg;
                }
                else if(player.leftJoystick.Horizontal != 0 || player.leftJoystick.Vertical != 0) { // если используется левый джостик
                    rotZ = Mathf.Atan2(player.leftJoystick.Vertical, player.leftJoystick.Horizontal) * Mathf.Rad2Deg;
                }
                
                if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); } // инверсия относительно Y
                else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
                
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот

                if(reloadTime <= 0 && (player.rightJoystick.Horizontal != 0 || player.rightJoystick.Vertical != 0)) { // перезарядка + нажатие джостика для стрельбы
                    Shoot();
                }
                else { reloadTime -= Time.deltaTime; }

                break; //---------------------------------------------------------------------------------------------------------------------// 

            case ControlType.FollowPlayer: // скрипт относится к weapon противника
                difference = player.transform.position - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); } // инверсия относительно Y
                else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
                
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот
                
                if(reloadTime <= 0) { // проверка перезарядки
                    Shoot();
                }
                else { reloadTime -= Time.deltaTime; }

                break; //---------------------------------------------------------------------------------------------------------------------//
        }

        
    }

    private void Shoot() { // выстрел
        Instantiate(bullet, shotPoint.position, transform.rotation); // создание объекта пули в месте shotPoint
        reloadTime = coolDown; // обнуление времени межлу выстрелами
    }

    private void Flip() { // поворот спрайта относительно Y
        inversion = !inversion;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }

}
