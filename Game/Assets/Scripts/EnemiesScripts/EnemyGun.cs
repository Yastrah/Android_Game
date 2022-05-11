using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bullet; // объект используемой пули
    public Transform shotPoint; // дуло (место, откуда вылетают пули)

    public float reload; // время между выстрелами в ДЕСЯТЫХ СЕКУНДЫ

    private float reloadTime;
    private float rotZ;
    private Vector3 difference;
    private Player player;
    private bool inversion = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        reloadTime = reload / 10;
    }

    private void Update() {
        difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if((rotZ > 90 || rotZ < -90) && inversion == false) { Flip(); } // инверсия относительно Y
        else if(rotZ < 90 && rotZ > -90 && inversion == true) { Flip(); }
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // поворот
        
        if(reloadTime <= 0) {
            Shoot();
        }
        else { reloadTime -= Time.deltaTime; }
    }


    private void Flip() { // поворот спрайта относительно Y
        inversion = !inversion;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }

    private void Shoot() { // выстрел
        Instantiate(bullet, shotPoint.position, transform.rotation); // создание объекта пули в месте shotPoint
        reloadTime = reload / 10; // обнуление времени между выстрелами
    }
}
