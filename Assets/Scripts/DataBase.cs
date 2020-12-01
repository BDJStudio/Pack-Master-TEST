using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public List<Items> items = new List<Items>();
    
    public GameObject[] itemsPrefabs;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}


[System.Serializable]
public class Items
{
    public int id;
    public Sprite imgFlag;
    public string country;

    public int plusPrice;

    public Sprite[] items1x1Sprite;
    public Sprite[] items1x2Sprite;
    public Sprite[] items1x3Sprite;
    public Sprite[] items2x2Sprite;
    public Sprite[] items3x1Sprite;
}
