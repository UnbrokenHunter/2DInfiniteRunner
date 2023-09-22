using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObjects/New Skin", order = 1)]
public class Skin : ScriptableObject
{
    public string name;
    public Sprite image;
    public GameObject skinObj;
    public int cost;
    public bool purchased = false;
}
