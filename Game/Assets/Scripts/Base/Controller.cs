using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Joystick joystick;
    public bool holdShoot = false;

    public void ShootDown()
    {
        holdShoot = true;
    }

    public void ShootUp()
    {
        holdShoot = false;
    }
}
