using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [Header("Объекты")]
    [Tooltip("Слой, который считается твёрдым")]
    [SerializeField] private LayerMask solidLayer;

    [Tooltip("Префаб используемой пули")]
    [SerializeField] private GameObject bullet;

    [Tooltip("Дуло (точка, откуда вылетают пули)")]
    [SerializeField] private Transform shotPoint;

    private GameObject laserRotation;
    private Transform laserPoint;
    private bool inversion = false; // отражение по y
    private float rotZ; // угол поворота относительно объекта (Игрока, Противника)

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
        
        // Инверсия относительно Y
        if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); }
        else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот

        // Проверка на перезарядку + нажатие кнопки стрельбы
        if (reloadTime <= 0 && controller.holdShoot) {
            Shoot();
        }
        else { reloadTime -= Time.deltaTime; }

    }

    /// <summary>
    /// Функция поиска ближайшего к игроку противника, перед которым нет преграды, и находещегося в зоне видимости камеры
    /// </summary>
    /// <returns>GameObject ближайшего доступного противника</returns>
    private GameObject NearEnemy() {
        GameObject nearEnemy = null;
        float minDistance = 100;

        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Vector2 difference = enemy.transform.position - transform.position;
            float bufRotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            laserRotation.transform.rotation = Quaternion.Euler(0f, 0f, bufRotZ);

            RaycastHit2D hitInfo = Physics2D.Raycast(laserPoint.position, laserPoint.right, difference.magnitude, solidLayer);
            
            if(hitInfo.collider != null) {
                // Debug.Log(hitInfo.collider.name);
                if(hitInfo.collider.tag == "Enemy") {
                    if((nearEnemy == null || hitInfo.distance < minDistance) && hitInfo.collider.gameObject.GetComponent<Enemy>().isVisible) {
                        nearEnemy = enemy;
                        minDistance = hitInfo.distance;
                    }
                }
            }
        }

        return nearEnemy;
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    private void Shoot() {
        // Создание объекта пули в месте shotPoint и присваивание имени родителя
        GameObject newBullet = Instantiate(bullet, shotPoint.position, transform.rotation);
        newBullet.GetComponent<Bullet>().Init(weaponData.BulletSettings, transform.parent.gameObject.name);

        // обнуление времени межлу выстрелами
        reloadTime = coolDown;
    }

    /// <summary>
    /// Поворот спрайта относительно Y
    /// </summary>
    private void Flip() {
        inversion = !inversion;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }

}
