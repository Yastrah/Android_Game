using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    // [SerializeField] private BehaviourType behaviour; // тип поведения
    [SerializeField] private GunType gunType; // тип оружия
    [SerializeField] private GameObject bullet; // объект используемой пули
    [SerializeField] private Transform shotPoint; // дуло (точка, откуда вылетают пули)
    [SerializeField] private LayerMask solidLayer; // что пуля будет считать твердым
    [SerializeField] private float coolDown; // время между выcтрелами/ударами

    
    private GameObject weapon;
    private GameObject laserRotation;
    private Transform laserPoint;
    private Vector2 difference; // расстояние до Player
    private float rotZ; // поворот оружия/laserPoint
    private bool inversion = false; // отражение по y
    private float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    private bool onSight; // игрок находится на прицеле (между ними нет препятствий)
    // UnityEngine.Random
    
    // private enum BehaviourType {
    //     Default,
    // }

    private enum GunType {
        Static,
        Dynamic
    }

    private new void Start() {
        base.Start();

        laserRotation = Notes.findChildByName(gameObject, "LaserRotation");
        laserPoint = Notes.findChildByName(gameObject, "LaserPoint").transform;
        weapon = Notes.findChildWithTag(gameObject, "Weapon");
    }

    private new void Update() {
        base.Update();

        if(isPushing) {
            push.OnPushStay(ref move, ref pushSpeed);

            if(pushSpeed <= 0) {
                isPushing = false;
                push = null;
                move = new Vector2(0, 0);
            }            
        }
        else {
            // move = new Vector2(0, 0);

            switch (status)
            {
                case StatusType.Passive:
                    base.PassiveBehaviour();
                    break;
                case StatusType.Aggressive:
                    AggressiveBehaviour();

                    switch (gunType)
                    {
                        case GunType.Static:
                            StaticGun();
                            break;
                        case GunType.Dynamic:
                            DynamicGun();
                            break;
                    }

                    break;
            }
        }
        

        // move = new Vector2(0f, 0f); // в наследниках
        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FixedUpdate() // итоговые изменения НА ЭКРАНЕ 
    {
        if(isPushing) {
            rb.velocity = new Vector2(move.x, move.y) * pushSpeed; // присваивание скорости
        }
        else {
            rb.velocity = new Vector2(move.x, move.y) * speed; // присваивание скорости
        }
        // rb.velocity = new Vector2(player.transform.position.x, player.transform.position.y) * speed;
        // rb.velocity = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        // difference = player.transform.position - transform.position;
        // Vector2 moveInput;
        // moveInput.x = difference.x / (System.Math.Abs(difference.x) + System.Math.Abs(difference.y));
        // moveInput.y = difference.y / (System.Math.Abs(difference.x) + System.Math.Abs(difference.y));
        // Debug.Log(moveInput);
        // rb.velocity = new Vector2(moveInput.x, moveInput.y) * speed;
    }

    private void AggressiveBehaviour() { // агрессивное поведение
        difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        laserRotation.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        RaycastHit2D hitInfo = Physics2D.Raycast(laserPoint.position, laserPoint.right, difference.magnitude, solidLayer);

        if(hitInfo.collider != null) {
            // Debug.Log(hitInfo.collider.name);
            if(hitInfo.collider.name == "Player") {
                onSight = true;

                // write
            }

            else {
                onSight = false;

                // write
            }
        }
        else {
            onSight = false;
            Debug.Log("hitInfo: no collider");
            return;
        }

    }

    private void StaticGun() {
        // write
    }

    private void DynamicGun() {

        if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); } // инверсия относительно Y
        else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
        
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот
        
        if(reloadTime <= 0) { // проверка перезарядки
            if(onSight) {
                Shoot();
            }
            else {
                reloadTime = coolDown;
            }
            
        }
        else { reloadTime -= Time.deltaTime; }


        void Flip() { // поворот спрайта относительно Y
            inversion = !inversion;
            Vector3 Scaler = weapon.transform.localScale;
            Scaler.y *= -1;
            weapon.transform.localScale = Scaler;
        }
    }

    private void Shoot() {
        Instantiate(bullet, shotPoint.position, weapon.transform.rotation); // создание объекта пули в месте shotPoint
        reloadTime = coolDown; // обнуление времени межлу выстрелами
    }
}
