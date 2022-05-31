using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    private new void Start() 
    {
        Invoke("DestroyBullet", lifeTime); // вызов уничтожения пули по истечении lifeTime
        base.Start();
    }
}
