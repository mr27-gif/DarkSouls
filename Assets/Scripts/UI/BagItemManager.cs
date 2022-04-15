using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItemManager : MonoBehaviour
{
    static BagItemManager bagItemManager;
    public List<Item> allBagItem;
    public MainBagItem mainBagItem;
    public MainBagItem actorBagItem;

    private void Awake()
    {
        if(bagItemManager!=null)
        {
            Destroy(this);
        }
        bagItemManager = this;
    }

    public static void InitBagAndActor()
    {
        InitAllBagItem();
        InitMainBagItem();
        InitMainActorItem();
    }

    public static void InitAllBagItem()
    {
        print(bagItemManager.allBagItem);
        foreach (Item item in bagItemManager.allBagItem)
        {
            item.itemNum = 1;
            if(item.itemName=="falchion")
            {
                item.itemNum = 0;
            }
        }
    }

    public static void InitMainBagItem()
    {
        for (int i = bagItemManager.mainBagItem.itemList.Count - 1; i >= 0; i--)
        {
            bagItemManager.mainBagItem.itemList.RemoveAt(i);
        }

        foreach (Item item in bagItemManager.allBagItem)
        {
            if(item.name!="falchion")
            {
                bagItemManager.mainBagItem.itemList.Add(item);
            }
        }
    }

    //初始化人物身上的装备
    public static void InitMainActorItem()
    {
        
        for (int i = bagItemManager.actorBagItem.itemList.Count - 1; i >= 0; i--)
        {
            bagItemManager.actorBagItem.itemList.RemoveAt(i);
        }

        foreach (Item item in bagItemManager.allBagItem)
        {
            if (item.name == "mace")
            {
                bagItemManager.actorBagItem.itemList.Add(item);
                item.itemNum--;
            }
            if(item.name=="dun")
            {
                bagItemManager.actorBagItem.itemList.Add(item);
                item.itemNum--;
            }
        }
    }

}
