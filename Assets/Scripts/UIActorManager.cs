using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActorManager : MonoBehaviour
{
    public Image weapongird;
    public Image dungird;
    public Image tougird;
    public Image bodygird;
    public Image hutuigird;
    public Image xiegird;
    public Image xiangliangird;

    public MainBagItem mainActorItem;
    public GameManager gm;
    private Color nullColor = new Color(255, 255, 255, 80);

    public void Start()
    {
        BagDisplayUI.updateActorItemToUI();
    }
    public void GirdClickEvent(string typename)
    {
        string itemName = null;
        for (int i = mainActorItem.itemList.Count - 1; i >= 0; i--)
        {
            if (mainActorItem.itemList[i].type == typename)
            {
                itemName = mainActorItem.itemList[i].itemName;
                mainActorItem.itemList.RemoveAt(i);
            }
        }
        if (itemName != null)
        {
            
            if (itemName == "dun")
            {
                gm.testwm.am.wm.whL.gameObject.SetActive(false);
                gm.testwm.am.ac.leftIsShield = false;
            }
            BagDisplayUI.FindItem(itemName).itemNum++;
        }
        BagDisplayUI.updateActorItemToUI();
        BagDisplayUI.updateItemToUI();
    }

    public void AddActorItem(Item item)
    {
        if(item.type=="weapon")
        {
            gm.addWeapon("R", item.itemName, false);
        }
        else if (item.type=="dun")
        {
            gm.testwm.am.wm.whL.gameObject.SetActive(true);
            gm.testwm.am.ac.leftIsShield = true;
        }
        bool canFind = false;
        for (int i = mainActorItem.itemList.Count - 1; i >= 0; i--)
        {
            if (mainActorItem.itemList[i].type == item.type)
            {
                if (mainActorItem.itemList[i].itemName == item.itemName)//已经穿戴了这个装备
                {
                    canFind = true;
                    break;
                }
                else//已经穿戴了别的装备
                {
                    mainActorItem.itemList[i].itemNum++;
                    mainActorItem.itemList[i] = null;
                    mainActorItem.itemList[i] = item;
                    item.itemNum--;
                    canFind = true;
                    break;
                }
            }
        }

        if (canFind == false)
        {
            mainActorItem.itemList.Add(item);
            item.itemNum--;
        }
    }

    public void UseBagItem(Item item)
    {
        item.itemNum--;
    }

}
