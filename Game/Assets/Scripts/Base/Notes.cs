using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes
{
    /// <summary>
    /// Перечисление возможных эффектов, где None - нет эффектов
    /// </summary>
    public enum Effect
    {
        None,
        Stun,
    }

    /// <summary>
    /// Все возможные типы воинов
    /// </summary>
    public enum WarriorType
    {
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

    /// <summary>
    /// Ищет один объект наследник(лежащие внутри родителя) с определённым тегом.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns>GameObject объекта с нужным тегом</returns>
    public static GameObject findChildWithTag(GameObject parent, string tag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach(Transform t in children)
        {
            if(t.gameObject.tag == tag)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Ищет один АКТИВНЫЙ объект наследник(лежащие внутри родителя) с определённым тегом
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns>GameObject активного объекта с нужным тегом</returns>
    public static GameObject findActiveChildWithTag(GameObject parent, string tag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach (Transform t in children)
        {
            if (t.gameObject.tag == tag && t.gameObject.activeSelf)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Ищет все объекты наследники(лежащие внутри родителя) с определённым тегом.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns>List<GameObject> наследников с нужным тегом</returns>
    public static List<GameObject> findChildrenWithTag(GameObject parent, string tag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        List<GameObject> childrenWithTag = new List<GameObject>();
        foreach (Transform t in children)
        {
            if (t.gameObject.tag == tag)
            {
                childrenWithTag.Add(t.gameObject);
            }
        }
        return childrenWithTag;
    }

    /// <summary>
    /// Ищет объект наследник, по его имени
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <returns>GameObject объекта с нужным именем</returns>
    public static GameObject findChildByName(GameObject parent, string name)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach (Transform t in children)
        {
            if (t.gameObject.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
