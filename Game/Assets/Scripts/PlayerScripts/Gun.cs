using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint; // дуло (место, откуда вылетают пули)
    
    public float reload;
    public float offset;

    private float reloadTime;
    private float rotZ;
    private Vector3 difference;
    private Player player;
    private bool inversion = false;

    private void Start() {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = transform.parent.gameObject.GetComponent<Player>();
    }

    private void Update() {
        if(Mathf.Abs(player.rightJoystick.Horizontal) > 0.1f || Mathf.Abs(player.rightJoystick.Vertical) > 0.1f){ // если используется правый джостик
            rotZ = Mathf.Atan2(player.rightJoystick.Vertical, player.rightJoystick.Horizontal) * Mathf.Rad2Deg;
        }
        else if(Mathf.Abs(player.leftJoystick.Horizontal) > 0.1f || Mathf.Abs(player.leftJoystick.Vertical) > 0.1f) { // если используется левый джостик
            rotZ = Mathf.Atan2(player.leftJoystick.Vertical, player.leftJoystick.Horizontal) * Mathf.Rad2Deg;
        }
        
        if((rotZ + offset > 90 || rotZ + offset < -90) && inversion == false) { Flip(); } // инверсия относительно Y
        else if(rotZ + offset < 90 && rotZ + offset > -90 && inversion == true) { Flip(); }
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset); // поворот

        if(reloadTime <= 0 && (player.rightJoystick.Horizontal != 0 || player.rightJoystick.Vertical != 0)) { Shoot(); } // перезарядка + нажатие джостика для стрельбы
        else { reloadTime -= Time.deltaTime; }
    }

    private void Shoot() {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        reloadTime = reload;
    }

    private void Flip() {
        inversion = !inversion;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }


}
