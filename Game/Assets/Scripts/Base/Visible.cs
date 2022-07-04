using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour
{
    /// <summary>
    /// ������� ���������� ��� ��������� ������� �� ����� �� �����  
    /// </summary>
    private void OnBecameVisible() {
        if(transform.parent.gameObject.GetComponent<Enemy>()) {
            transform.parent.gameObject.GetComponent<Enemy>().isVisible = true;
        }
    }

    /// <summary>
    /// ������� ���������� ��� ������������ ������� �� ���� �����
    /// </summary>
    private void OnBecameInvisible() {
        if(transform.parent.gameObject.GetComponent<Enemy>()) {
            transform.parent.gameObject.GetComponent<Enemy>().isVisible = false;
        }
    }
}
