using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Параметры оружия")]
    [Tooltip("Время между выcтрелами")]
    [SerializeField] private float coolDown;

    [Tooltip("Настройки пули")]
    [SerializeField] private BulletData bulletSettings;


    [Header("Объекты")]
    [Tooltip("Объект оружия, если оно представлено отдельным объектом")]
    [SerializeField] private GameObject weapon;

    [Tooltip("Дуло (точка, откуда вылетают пули)")]
    [SerializeField] private Transform shotPoint;

    [Tooltip("Префаб используемой пули")]
    [SerializeField] private GameObject bullet;

    [Tooltip("Слой, который считается твёрдым")]
    [SerializeField] private LayerMask solidLayer;

    //private GameObject weapon;
    private GameObject laserRotation;
    private Transform laserPoint;
    private Vector2 difference; // расстояние до Player
    private float rotZ; // поворот оружия/laserPoint
    private bool inversion = false; // отражение по y
    private float reloadTime; // вспомогательная переменная для хранения времени до выстрела/удара
    private bool onSight; // игрок находится на прицеле (между ними нет препятствий)
    // UnityEngine.Random

    private new void Start()
    {
        base.Start();

        laserRotation = Notes.findChildByName(gameObject, "LaserRotation");
        laserPoint = Notes.findChildByName(gameObject, "LaserPoint").transform;
    }

    private new void Update()
    {
        base.Update();

        if (isPushing)
        {
            push.OnPushStay(ref move, ref pushSpeed);

            if (pushSpeed <= 0.1)
            {
                isPushing = false;
                push = null;
                move = new Vector2(0, 0);
            }
        }
        else
        {
            switch (status)
            {
                case StatusType.Passive:
                    base.PassiveBehaviour();
                    break;
                case StatusType.Aggressive:
                    AggressiveBehaviour();

                    if(weapon)
                    {
                        DynamicGun();
                    }
                    else
                    {
                        StaticGun();
                    }

                    break;
            }
        }

        // move = new Vector2(0f, 0f); // в наследниках
        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (isPushing)
        {
            rb.velocity = new Vector2(move.x, move.y) * pushSpeed; // присваивание скорости
        }
        else
        {
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

    /// <summary>
    /// Обработка агрессивного поведения
    /// </summary>
    private void AggressiveBehaviour()
    {
        difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        laserRotation.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        RaycastHit2D hitInfo = Physics2D.Raycast(laserPoint.position, laserPoint.right, difference.magnitude, solidLayer);

        if (hitInfo.collider)
        {
            // Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.name == "Player")
            {
                onSight = true;

                // write
            }

            else
            {
                onSight = false;

                // write
            }
        }
        else
        {
            onSight = false;
            Debug.Log("hitInfo: no collider");
            return;
        }

    }

    /// <summary>
    /// Обработка оружия, не имеющего своего объекта (не следящего за игроком)
    /// </summary>
    private void StaticGun()
    {
        // write
    }

    /// <summary>
    /// Обработка динамического оружия, со своим объектом
    /// </summary>
    private void DynamicGun()
    {
        // Инверсия относительно Y
        if ((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); }
        else if (rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }

        // Поворот
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        // Проверка перезарядки
        if (reloadTime <= 0)
        {
            if (onSight)
            {
                Shoot();
            }
            else
            {
                reloadTime = coolDown;
            }

        }
        else { reloadTime -= Time.deltaTime; }

        // Поворот спрайта относительно Y
        void Flip()
        {
            inversion = !inversion;
            Vector3 Scaler = weapon.transform.localScale;
            Scaler.y *= -1;
            weapon.transform.localScale = Scaler;
        }
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    private void Shoot()
    {
        // Создание объекта пули в месте shotPoint и присваивание имени родителя
        GameObject newBullet = Instantiate(bullet, shotPoint.position, weapon.transform.rotation);
        newBullet.GetComponent<Bullet>().Init(bulletSettings, gameObject.name);

        // Обнуление времени межлу выстрелами
        reloadTime = coolDown;
    }

}
