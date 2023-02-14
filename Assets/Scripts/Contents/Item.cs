using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item", order = 0)]
public class Item : ScriptableObject
{
    public enum category
    {
        cube,
        sphere,
        cylinder,
        capsule
    }
    public GameObject character;
    public string lat;
    public string lon;
    public category _Category;

    public Item(string lat, string lon, category category, GameObject character)
    {
        this.lat = lat;
        this.lon = lon;
        this._Category = category;
        this.character = character;
    }

}
