using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes
{
    public enum Effect { // перечисление возможных эффектов, где None - нет эффектов
        None,
        Slowness,
    }

    // public enum BulletEffect { // перечисление возможных эффектов, где None - нет эффектов
    //     None,
    //     Slowness,
    //     Ice,
    // }


    public static GameObject findChildWithTag(GameObject parent, string tag) { // ищет один объект наследник(лежащие внутри родителя) с определённым тегом.
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach(Transform t in children) {
            if(t.gameObject.tag == tag) {
                return t.gameObject;
            }
        }
        return null;
    }

    public static GameObject findActiveChildWithTag(GameObject parent, string tag) { // ищет один АКТИВНЫЙ объект наследник(лежащие внутри родителя) с определённым тегом
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach(Transform t in children) {
            if(t.gameObject.tag == tag && t.gameObject.activeSelf) {
                return t.gameObject;
            }
        }
        return null;
    }

    public static List<GameObject> findChildrenWithTag(GameObject parent, string tag) { // ищет все объекты наследники(лежащие внутри родителя) с определённым тегом.
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        List<GameObject> childrenWithTag = new List<GameObject>(); 
        foreach(Transform t in children) {
            if(t.gameObject.tag == tag) {
                childrenWithTag.Add(t.gameObject);
            }
        }
        return childrenWithTag;
    }
}
