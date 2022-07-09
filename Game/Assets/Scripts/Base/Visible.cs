using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour
{
    /// <summary>
    /// Функция вызываемая при появлении объекта на одной из камер  
    /// </summary>
    private void OnBecameVisible() {
        if(transform.parent.gameObject.GetComponent<Enemy>()) {
            transform.parent.gameObject.GetComponent<Enemy>().isVisible = true;
        }
    }

    /// <summary>
    /// Функция вызываемая при исчезновении объекта со всех камер
    /// </summary>
    private void OnBecameInvisible() {
        if(transform.parent.gameObject.GetComponent<Enemy>()) {
            transform.parent.gameObject.GetComponent<Enemy>().isVisible = false;
        }
    }
}
