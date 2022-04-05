using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + pesosAmount + "pesos!", 25, Color.yellow, transform.position, Vector3.up * 5, 3.0f) ;
            GameManager.instance.pesos = GameManager.instance.pesos + pesosAmount;
        }
    }
}
