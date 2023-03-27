using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public Sprite icon;
    public float fireRate = 0.5f;
    public abstract bool Use(Transform transform);
}
