using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTwo : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;
    public int itemSize;
    public int countryID;
    public Vector3 offset;
    public Vector3Int newPos;
    public Vector2 startPos;
    
    private Renderer mainRenerer;
    private SpriteRenderer spriteRenderer;
    private Transform trans;
    private GameManager gm;
    private DataBase db;

    

    private void Start()
    {
        trans = gameObject.transform;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        db = GameObject.Find("DataBase").GetComponent<DataBase>();

        mainRenerer = GetComponentInChildren<Renderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        startPos = trans.position;

        countryID = PlayerPrefs.GetInt("countryID");

        CreateItemSprite(countryID);
    }

    // функция выбора спрайта предметов в зависимости от страны и их размера
    public void CreateItemSprite(int countryID)
    {
        int y;

        switch (itemSize)
        {
            case 0:
                y = Random.Range(0, db.items[countryID].items1x1Sprite.Length);
                spriteRenderer.sprite = db.items[countryID].items1x1Sprite[y];
                break;
            
            case 1:
                y = Random.Range(0, db.items[countryID].items1x2Sprite.Length);
                spriteRenderer.sprite = db.items[countryID].items1x2Sprite[y];
                break;
            
            case 2:
                y = Random.Range(0, db.items[countryID].items1x3Sprite.Length);
                spriteRenderer.sprite = db.items[countryID].items1x3Sprite[y];
                break;
            
            case 3:
                y = Random.Range(0, db.items[countryID].items2x2Sprite.Length);
                spriteRenderer.sprite = db.items[countryID].items2x2Sprite[y];
                break;
            
            case 4:
                y = Random.Range(0, db.items[countryID].items3x1Sprite.Length);
                spriteRenderer.sprite = db.items[countryID].items3x1Sprite[y];
                break;
        }
    }

    // визуал
    public void SetTransparent(bool available)
    {
        if (available)
            mainRenerer.material.color = Color.green;
        else
            mainRenerer.material.color = Color.red;
    }

    public void SetNormal()
    {
        if(mainRenerer != null) mainRenerer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        gm.StartPlacirgItem(this);
        gm.DelPlaceFlyingItems(Mathf.RoundToInt(trans.position.x), Mathf.RoundToInt(trans.position.y));
    }

    private void OnMouseDrag()
    {
        gm.MovingItems();
    }

    private void OnMouseUp()
    {
        gm.PlacesItem();
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if ((x + y) % 2 == 0)
                    Gizmos.color = new Color(240, 240, 240, .5f);
                else
                    Gizmos.color = new Color(0, 0, 0, .5f);

                Gizmos.DrawCube(transform.position + new Vector3(x, y, 0), new Vector3(1, 1, .1f));
            }
        }
    }
}
