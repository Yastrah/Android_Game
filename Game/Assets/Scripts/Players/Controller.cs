using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("�������� ����������")]
    [Tooltip("������� ��������������")]
    public Joystick joystick;

    [HideInInspector] public bool holdShoot = false;

    /// <summary>
    /// �������, ����������� ��� ������� �� ������ �����
    /// </summary>
    public void ShootDown()
    {
        holdShoot = true;
    }

    /// <summary>
    /// �������, ����������� ��� ������� ������ �����
    /// </summary>
    public void ShootUp()
    {
        holdShoot = false;
    }
}
