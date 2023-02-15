using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public enum frame
    {
        card,
        cut,
        noCut,
        blueNeon
    }

    public GameObject character;
    public string lat;
    public string lon;
    public category _category;
    public frame _frame;

    public Item(string lat, string lon, category category, frame frame, GameObject character)
    {
        this.lat = lat;
        this.lon = lon;
        this._category = category;
        this._frame = frame;
        this.character = character;
    }
    
}
