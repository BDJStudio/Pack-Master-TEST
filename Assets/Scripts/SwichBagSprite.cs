using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwichBagSprite : MonoBehaviour
{
    public Sprite[] spritesSmall;
    public Sprite[] spritesLong;

    public int sizeBag;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("enter",true);
    }

    public void SwichSprite()
    {

        if (sizeBag > 0)
        {
            anim.SetBool("enter", true);
            int x = Random.Range(0, spritesLong.Length);
            GetComponentInChildren<SpriteRenderer>().sprite = spritesLong[x];
        }
        else if (sizeBag == 0)
        {
            anim.SetBool("enter", true);
            int x = Random.Range(0, spritesSmall.Length);
            GetComponentInChildren<SpriteRenderer>().sprite = spritesSmall[x];
        }
    }

    public void CloseAnim()
    {
        anim.SetTrigger("exit");
        anim.SetBool("enter", false);
    }
}
