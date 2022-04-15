using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject myBagUI;
    public bool IsPlay=false;

    public MainBagItem mainItem;

    public ActorManager am;
    public InteractionManager im;
    public UIGameManager uiGM;

    private void Awake()
    {
        im = am.im;
        uiGM = GetComponent<UIGameManager>();
    }

    void Start()
    {
        BagDisplayUI.updateItemToUI();
        myBagUI.SetActive(IsPlay);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPlay =!IsPlay;
            myBagUI.SetActive(IsPlay);
            Cursor.lockState = IsPlay ? CursorLockMode.Confined : CursorLockMode.Locked;
        }

        if(Input.GetKey(KeyCode.E))
        {
            if(im.overlapEvastms.Count!=0)
            {
                print("!=0");
                if(im.overlapEvastms[0].eventName== "openBox"&&
                    im.overlapEvastms[0].active==false&& im.overlapEvastms[0].item!=null)//已经开盖且还有东西
                {
                    print("kaigai而且有东西");
                    if (!mainItem.itemList.Contains(im.overlapEvastms[0].item))
                    {
                        mainItem.itemList.Add(im.overlapEvastms[0].item);
                    }
                        uiGM.displayTipsPanel("你获得了"+im.overlapEvastms[0].item.itemInfo);
                    im.overlapEvastms[0].item.itemNum += 1;
                    im.overlapEvastms[0].item = null;
                    BagDisplayUI.updateItemToUI();
                }
            }  
        }

    }

    public void RemoveOrReduceBagitem(string itemName)
    {
        for (int i = mainItem.itemList.Count - 1; i >= 0; i--)
        {
           if(mainItem.itemList[i].itemName==itemName)
            {
                mainItem.itemList[i].itemNum -= 1;
                if (mainItem.itemList[i].itemNum == 0)
                {
                    mainItem.itemList.RemoveAt(i);
                }
            }
        }
    }

}
