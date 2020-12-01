using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Vector2Int GridSize;
    public GameObject cellPrefab;
    public int countBag;

    public ItemTwo[,] grid;
    public ItemTwo flyingItem;
    public ItemTwo placeItem;

    private Camera cam;
    public bool available;
    private Vector3Int newPos;
    private DataBase db;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        db = GameObject.Find("DataBase").GetComponent<DataBase>();

        //создание сетки
        grid = new ItemTwo[GridSize.x, GridSize.y];

        PlaceBag(countBag);
        //FillBagItems();
    }

    // функция ствящая префаб чемодана на сетку
    public void PlaceBag(int countBag)
    {
        switch (countBag)
        {
            case 2:
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x), Mathf.RoundToInt(placeItem.transform.position.y));
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x + 3), Mathf.RoundToInt(placeItem.transform.position.y));
                break;

            case 3:
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x), Mathf.RoundToInt(placeItem.transform.position.y));
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x + 3), Mathf.RoundToInt(placeItem.transform.position.y));
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x), Mathf.RoundToInt(placeItem.transform.position.y - 3));
                break;
            
            case 4:
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x), Mathf.RoundToInt(placeItem.transform.position.y));
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x + 3), Mathf.RoundToInt(placeItem.transform.position.y));
                PlaceFlyingItems2(Mathf.RoundToInt(placeItem.transform.position.x), Mathf.RoundToInt(placeItem.transform.position.y - 2));
                break;
        }
    }

    //кнопка создания итемов
    public void CreateItems(int countMax)
    {
        for (int i = 0; i < countMax; i++)
        {
            int x = Random.Range(0, db.itemsPrefabs.Length);

            Instantiate(db.itemsPrefabs[x], new Vector3(i, .5f, 0), Quaternion.identity);
        }
    }

    // ссылка на объект который мы берем
    public void StartPlacirgItem(ItemTwo itemPrefab)
    {
        flyingItem = itemPrefab;
    }
    
    // функция расположения итема в чемодн
    public void PlacesItem()
    {
        if (available)
        {
            flyingItem.transform.position = newPos;
            PlaceFlyingItems(newPos.x, newPos.y);
        }
        else
        {
            flyingItem.transform.position = flyingItem.startPos;
            flyingItem.SetNormal();
            flyingItem = null;
        }
    }

    public void MovingItems()
    {
        Vector3 pos = Input.mousePosition + flyingItem.offset;

        var groundPlane = new Plane(Vector3.forward, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worlPosition = ray.GetPoint(position);

            newPos.x = Mathf.RoundToInt(worlPosition.x);
            newPos.y = Mathf.RoundToInt(worlPosition.y);

            available = true;

            // проверка границ чемодна
            if (newPos.x < 0 || newPos.x > GridSize.x - placeItem.size.x) available = false;
            if (newPos.y < 0 || newPos.y > GridSize.y - placeItem.size.y) available = false;

            if (available && IsPlaceTaken(newPos.x, newPos.y)) available = false;

            flyingItem.transform.position = cam.ScreenToWorldPoint(pos);
            flyingItem.SetTransparent(available);
        }
    }

    //чтение вносящего массива
    public bool IsPlaceTaken(int placeX,int placeY)
    {
        for (int x = 0; x < flyingItem.size.x; x++)
        {
            for (int y = 0; y < flyingItem.size.y; y++)
            {
                if (grid[placeX + x, placeY + y] == null || grid[placeX + x, placeY + y] != placeItem) return true;
            }
        }

        return false;
    }

    //массив вносящий предметы в сетку
    public void PlaceFlyingItems(int placeX,int placeY)
    {
        for(int x = 0; x < flyingItem.size.x; x++)
        {
            for (int y = 0; y < flyingItem.size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingItem;
            }
        }

        flyingItem.SetNormal();
    }
    
    // функция вносящая чемодан в сетку
    public void PlaceFlyingItems2(int placeX,int placeY)
    {
        for(int x = 0; x < placeItem.size.x; x++)
        {
            for (int y = 0; y < placeItem.size.y; y++)
            {
                grid[placeX + x, placeY + y] = placeItem;
            }
        }
    }

    // функция удаления предмета из чемодана
    public void DelPlaceFlyingItems(int placeX, int placeY)
    {
        try
        {
            for (int x = 0; x < flyingItem.size.x; x++)
            {
                for (int y = 0; y < flyingItem.size.y; y++)
                {
                    if (IsPlaceTaken(Mathf.RoundToInt(flyingItem.transform.position.x), Mathf.RoundToInt(flyingItem.transform.position.y)))
                        grid[placeX + x, placeY + y] = placeItem;
                }
            }
            flyingItem.SetNormal();
        }
        catch
        {

        }
    }

    public void ExitLVL(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
