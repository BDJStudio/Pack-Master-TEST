using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{

    /*public Vector3 offset;
    public Vector3 mousePos;
    public int sizeItem,x;

    public int shapeId;

    public Bag bag;

    private Transform trans;
    private Camera cam;

    private Transform nowPos;

    public bool isActive;
    public List<Transform> pointsChild = new List<Transform>();

    private void Start()
    {
        trans = gameObject.transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnMouseDown()
    {
        isActive = true;
        pointsChild.Clear();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Input.mousePosition + offset;
        trans.position = cam.ScreenToWorldPoint(pos) + mousePos;

    }

    private void OnMouseUp()
    {
        //PlaceItem(sizeItem);
        isActive = false;
        pointsChild.Add(nowPos);
        trans.position = pointsChild[0].position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cell" && !collision.GetComponent<Cell>().locked && isActive)
        {
            pointsChild.Add(collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cell" && !collision.GetComponent<Cell>().locked)
        {
            //if(collision.transform.position.x < 1)
            nowPos = collision.transform;
        }
    }

    public void PlaceItem(int x)
    {
        switch (x)
        {
            case 1:
                trans.position = pointsChild[0].position;
                break;

            case 2:
                trans.position = (pointsChild[0].position + pointsChild[1].position) / 2;
                break;

            case 3:
                trans.position = (pointsChild[0].position + pointsChild[1].position + pointsChild[2].position) / 3;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cell")
        {
            pointsChild.RemoveAt(pointsChild.Count - 1);
        }
    }*/

    public Vector2Int size = Vector2Int.one;
    
    private Renderer mainRenerer;

    public Vector3 offset;

    public Vector3Int newPos;
    public Vector2 startPos;

    private Transform trans;
    private GameManager gm;
    private DataBase db;



    private void Start()
    {
        trans = gameObject.transform;

        gm = GameObject.Find("CreateBag").GetComponent<GameManager>();
        db = GameObject.Find("DataBase").GetComponent<DataBase>();

        mainRenerer = GetComponentInChildren<Renderer>();

        startPos = trans.position;
    }

    public void SetTransparent(bool available)
    {
        if (available)
            mainRenerer.material.color = Color.green;
        else
            mainRenerer.material.color = Color.red;
    }

    public void SetNormal()
    {
        mainRenerer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        //gm.StartPlacirgItem(this);
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
