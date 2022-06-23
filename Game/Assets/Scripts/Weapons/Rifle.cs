using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private GameObject bullet; // объект используемой пули
    [SerializeField] private Transform shotPoint; // дуло (точка, откуда вылетают пули)
    [SerializeField] private LayerMask solidLayer; // что пуля будет считать твердым

    private GameObject laserRotation;
    private Transform laserPoint;
    private bool inversion = false; // отражение по y
    private float rotZ; // угол поворота относительно объекта (Игрока, Противника)
    // private Vector3 difference; // координаты PLayer оттносительно Enemy

    private new void Start() {
        base.Start();
        
        laserRotation = Notes.findChildByName(GameObject.FindWithTag("Player"), "LaserRotation");
        laserPoint = Notes.findChildByName(GameObject.FindWithTag("Player"), "LaserPoint").transform;
    }

    private void Update() 
    {
        GameObject enemy = NearEnemy();
        if(enemy != null) {
            Vector2 difference = enemy.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if(controller.joystick.Horizontal != 0 || controller.joystick.Vertical != 0) { // если используется левый джостик
            rotZ = Mathf.Atan2(controller.joystick.Vertical, controller.joystick.Horizontal) * Mathf.Rad2Deg;
        }

        // if(player.rightJoystick.Horizontal != 0 || player.rightJoystick.Vertical != 0){ // если используется правый джостик
        //     rotZ = Mathf.Atan2(player.rightJoystick.Vertical, player.rightJoystick.Horizontal) * Mathf.Rad2Deg;
        // }
        // else if(player.leftJoystick.Horizontal != 0 || player.leftJoystick.Vertical != 0) { // если используется левый джостик
        //     rotZ = Mathf.Atan2(player.leftJoystick.Vertical, player.leftJoystick.Horizontal) * Mathf.Rad2Deg;
        // }
        
        if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); } // инверсия относительно Y
        else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот

        if(reloadTime <= 0 && controller.holdShoot) { // перезарядка + нажатие джостика для стрельбы
            Shoot();
        }
        else { reloadTime -= Time.deltaTime; }

        // if(reloadTime > 0) { reloadTime -= Time.deltaTime; }
    }

    private GameObject NearEnemy() {
        GameObject nearEnemy = null;
        float minDistance = 100;

        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Vector2 difference = enemy.transform.position - transform.position;
            float bufRotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            laserRotation.transform.rotation = Quaternion.Euler(0f, 0f, bufRotZ);

            RaycastHit2D hitInfo = Physics2D.Raycast(laserPoint.position, laserPoint.right, difference.magnitude, solidLayer);
            // RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, bufRotZ).eulerAngles, difference.magnitude, solidLayer);
            
            if(hitInfo.collider != null) {
                // Debug.Log(hitInfo.collider.name);
                if(hitInfo.collider.tag == "Enemy") {
                    if(nearEnemy == null || hitInfo.distance < minDistance) {
                        nearEnemy = enemy;
                        minDistance = hitInfo.distance;
                    }
                }
            }
        }

        return nearEnemy;
    }

    // public override void Fire() { // функцие вызывающаяся при нажатии на кнопку стрельбы
    //     Debug.Log("Fire");

    //     if(reloadTime <= 0) {
    //         Shoot();
    //     }
    // }

    private void Shoot() { // выстрел
        GameObject newBullet = Instantiate(bullet, shotPoint.position, transform.rotation); // создание объекта пули в месте shotPoint
        newBullet.GetComponent<Bullet>().setPatent(transform.parent.gameObject.name);
        reloadTime = coolDown; // обнуление времени межлу выстрелами
    }

    private void Flip() { // поворот спрайта относительно Y
        inversion = !inversion;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }

}
