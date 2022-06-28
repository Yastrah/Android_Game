using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes
{
    public enum Effect { // перечисление возможных эффектов, где None - нет эффектов
        None,
        Stun,
    }

    public enum WarriorType {
        Sniper
    }

    // public enum BulletEffect { // перечисление возможных эффектов, где None - нет эффектов
    //     None,
    //     Slowness,
    //     Ice,
    // }

    // [HideInInspector]
    // [SerializeField]

    // GameObject obj = Resources.Load("Prefabs/Enemy/Enemies/PunkEnemy") as GameObject;
    // Debug.Log(obj.name);

    // OnBecameVisible()
    // OnBecameInvisible()
    // public bool IsVisible() {
    //         return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), GetComponent<BoxCollider2D>().bounds);
    // }

    // Occlusion Culling

    // private void OnTriggerEnter2D(Collider2D other) {}
    // Debug.Log("информация"); // вывод вспомогательной информации в консоль
    // transform.eulerAngles = new Vector3(0, 180, 0); // альтернативный разворот

    // Debug.Log(texture.activeSelf); // проверка на активность объекта
    // Debug.Log(texture.activeInHierarchy); // относительно всей сцены
    // Debug.Log(difference.magnitude);
    // Debug.ClearDeveloperConsole();
    // Debug.LogError();
    // Debug.LogException();
    // Debug.LogWarning();
    // Quaternion.identity - Этот кватернион соответствует «отсутствию вращения» — объект идеально выровнен с мировыми или родительскими осями.

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

    public static GameObject findChildByName(GameObject parent, string name) {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach(Transform t in children) {
            if(t.gameObject.name == name) {
                return t.gameObject;
            }
        }
        return null;
    }
}
