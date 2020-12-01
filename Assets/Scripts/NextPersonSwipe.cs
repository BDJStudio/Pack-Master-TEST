using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextPersonSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public RandomSample randomSample;
    public bool nextTru;

    public Image img;
    public Text price;
    public Text money;
    public int saveMoney;
    
    public GameObject Swipetext;
    public SwichBagSprite swichBag;
    public int sizeBag;
    public int countryID;

    public int sceneID;
    private DataBase db;

    public void Start()
    {
        randomSample = randomSample.GetComponent<RandomSample>();
        db = GameObject.Find("DataBase").GetComponent<DataBase>();

        nextTru = false;

        countryID = Random.Range(0, db.items.Count);
        Swipetext.GetComponent<Text>().color = new Color(0, 0, 0, 0);

        CreateStatsPerson(countryID);

        swichBag.sizeBag = Random.Range(0, 2);
        
    }


    //фукция меняющая персонажа
    public void NextPercon()
    {
        countryID = Random.Range(0, db.items.Count);
        swichBag.sizeBag = Random.Range(0, 2);
print(swichBag.sizeBag);
        if (swichBag.sizeBag == 0)
        {
            sceneID = Random.Range(1, 2);
        }
        else if (swichBag.sizeBag > 0)
        {
            sceneID = Random.Range(3, 4);
        }

        Swipetext.GetComponent<Text>().color = new Color(0, 0, 0, 1);
        nextTru = true;
    }

    //спавним страну и прайс
    public void CreateStatsPerson(int x)
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            money.text = PlayerPrefs.GetInt("Money").ToString();
        }
        else
            print("No save");
        
        price.text = (Random.Range(10, 100) + db.items[x].plusPrice).ToString();
        img.sprite = db.items[x].imgFlag;
    }

    public void StartScene()
    {
        money.text = (int.Parse(money.text) + int.Parse(price.text)).ToString();

        PlayerPrefs.SetInt("Money", int.Parse(money.text));
        SceneManager.LoadScene(sceneID);
    }

    //функция свайпов
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (nextTru)
        {
            if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
            {
                if (eventData.delta.x > 0)
                {
                    // Движение вправо
                }
                else
                {
                    // Движение влево
                    Swipetext.GetComponent<Text>().color = new Color(0, 0, 0, 0);
                    
                    randomSample.RandomizeCharacter();
                    CreateStatsPerson(countryID);

                    swichBag.SwichSprite();
                    
                    nextTru = false;

                    PlayerPrefs.SetInt("countryID", countryID); //сохранение данных
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
