using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Ёлементы управлени€")]
    [Tooltip("ƒжостик перередвижени€")]
    public Joystick joystick;

    [HideInInspector] public bool holdShoot = false;

    /// <summary>
    /// ‘ункци€, вызываема€€ при нажатии на кнопку атаки
    /// </summary>
    public void ShootDown()
    {
        holdShoot = true;
    }

    /// <summary>
    /// ‘ункци€, вызываема€€ при отжатии кнопки атаки
    /// </summary>
    public void ShootUp()
    {
        holdShoot = false;
    }
}
