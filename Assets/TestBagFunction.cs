using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBagFunction : MonoBehaviour
{
    public GameObject myBagUI;
    public bool IsPlay=false;

    public Item item;
    public MainBagItem mainItem;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPlay =! IsPlay;
            myBagUI.SetActive(IsPlay);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            if(!mainItem.itemList.Contains(item))
            {
                mainItem.itemList.Add(item);
            }
            item.itemNum += 1;
        }

    }

}
