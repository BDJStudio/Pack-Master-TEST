using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBag : MonoBehaviour
{
    public ItemTwo bag;
    
    private DataBase db;
    private GameManager gm;
    private GameObject item;

    private int[,] grid;

    private void Start()
    {
        db = GameObject.Find("DataBase").GetComponent<DataBase>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        FillBagItems();

        for (int x = 0; x < bag.size.x; x++)
        {
            for (int y = 0; y < bag.size.y; y++)
            {
                grid[x, y] = 0;
            }
        }
    }

    public void FillBagItems()
    {
        for (int x = 0; x < bag.size.x; x++)
        {
            for (int y = 0; y < bag.size.y; y++)
            {
                int n = Random.Range(0, 1);

                item = db.itemsPrefabs[n];
                
                    Instantiate(item, new Vector3(transform.position.x + x, transform.position.y + y, 0), Quaternion.identity);
                
                grid[x, y] = 1;

                print(grid[x,y]);
            }
        }
    }

    public void PlaceFlyingItems(int placeX, int placeY)
    {
        for (int x = 0; x < item.GetComponent<ItemTwo>().size.x; x++)
        {
            for (int y = 0; y < item.GetComponent<ItemTwo>().size.y; y++)
            {
                gm.grid[placeX + x, placeY + y] = item.GetComponent<ItemTwo>();
            }
        }
    }

    public void CheckItem()
    {
        for (int x = 0; x < bag.size.x; x++)
        {
            for (int y = 0; y < bag.size.y; y++)
            {

            }
        }
    }
}
