using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet; // объект используемой пули
    public Transform shotPoint; // дуло (место, откуда вылетают пули)
    
    public float reload; // время между вычтрелами в ДЕСЯТЫХ СЕКУНДЫ

    private float reloadTime;
    private float rotZ;
    private Player player;
    private bool inversion = false;

    private void Start() {
        player = transform.parent.gameObject.GetComponent<Player>(); // получение скрипта родителя
        reloadTime = reload / 10;
    }

    private void Update() {
        if(player.rightJoystick.Horizontal != 0 || player.rightJoystick.Vertical != 0){ // если используется правый джостик
            rotZ = Mathf.Atan2(player.rightJoystick.Vertical, player.rightJoystick.Horizontal) * Mathf.Rad2Deg;
        }
        else if(player.leftJoystick.Horizontal != 0 || player.leftJoystick.Vertical != 0) { // если используется левый джостик
            rotZ = Mathf.Atan2(player.leftJoystick.Vertical, player.leftJoystick.Horizontal) * Mathf.Rad2Deg;
        }
        
        if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); } // инверсия относительно Y
        else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот

        if(reloadTime <= 0 && (player.rightJoystick.Horizontal != 0 || player.rightJoystick.Vertical != 0)) { Shoot(); } // перезарядка + нажатие джостика для стрельбы
        else { reloadTime -= Time.deltaTime; }
    }

    private void Shoot() { // выстрел
        Instantiate(bullet, shotPoint.position, transform.rotation); // создание объекта пули в месте shotPoint
        reloadTime = reload / 10; // обнуление времени межлу выстрелами
    }

    private void Flip() { // поворот спрайта относительно Y
        inversion = !inversion;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }


}
