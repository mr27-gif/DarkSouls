using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagDisplayUI : MonoBehaviour
{
    static BagDisplayUI bagDisplayUI;
    private void Awake()
    {
        if (bagDisplayUI != null)
        {
            Destroy(this);
        }
        bagDisplayUI = this;

        mainBagItem = (MainBagItem)Resources.Load("Bag/MainBagItem");
        mainActorItem=(MainBagItem)Resources.Load("Bag/MainActorItem");
    }

    private void OnEnable()
    {
        updateItemToUI();
    }

    public MainBagItem mainBagItem;
    public girdPrefab girdprefab;
    public GameObject Mybag;

    public MainBagItem mainActorItem;
    public UIActorManager UIam;
    private static Color whiteColor = new Color(255, 255, 255, 255);

    public static void insertItemToUI(Item item)
    {
        if (item.itemNum != 0)
        {
            girdPrefab gird = Instantiate(bagDisplayUI.girdprefab, bagDisplayUI.Mybag.transform);
            gird.childImg.sprite = item.itemImage;
            gird.childNumText.text = item.itemNum.ToString();
            gird.type = item.type;
            gird.prefabName = item.itemName;
        }
    }

    public static Item FindItem(string itemName)
    {
        for (int i = bagDisplayUI.mainBagItem.itemList.Count - 1; i >= 0; i--)
        {
            if (bagDisplayUI.mainBagItem.itemList[i].itemName == itemName)
            {
                return bagDisplayUI.mainBagItem.itemList[i];

            }
        }
        return null;
    }

    public static void updateItemToUI()
    {
        for (int i = 0; i < bagDisplayUI.Mybag.transform.childCount; i++)
        {
            Destroy(bagDisplayUI.Mybag.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < bagDisplayUI.mainBagItem.itemList.Count; i++)
        {
            insertItemToUI(bagDisplayUI.mainBagItem.itemList[i]);
        }

    }

    public static void SetImageSprite(Image image, Sprite sprite, Color color)
    {
        image.sprite = sprite;
        image.color = whiteColor;
    }

    public static void SetNullImageSprite(Image image)
    {
        image.sprite = null;
        image.color = whiteColor;
    }

    public static void updateActorItemToUI()
    {
        SetNullImageSprite(bagDisplayUI.UIam.weapongird);
        SetNullImageSprite(bagDisplayUI.UIam.dungird);
        SetNullImageSprite(bagDisplayUI.UIam.tougird);
        SetNullImageSprite(bagDisplayUI.UIam.bodygird);
        SetNullImageSprite(bagDisplayUI.UIam.hutuigird);
        SetNullImageSprite(bagDisplayUI.UIam.xiegird);
        SetNullImageSprite(bagDisplayUI.UIam.xiangliangird);


        for (int i = bagDisplayUI.mainActorItem.itemList.Count - 1; i >= 0; i--)
        {
            switch (bagDisplayUI.mainActorItem.itemList[i].type)
            {
                case "weapon":
                    SetImageSprite(bagDisplayUI.UIam.weapongird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
                case "dun":
                    SetImageSprite(bagDisplayUI.UIam.dungird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
                case "tou":
                    SetImageSprite(bagDisplayUI.UIam.tougird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
                case "body":
                    SetImageSprite(bagDisplayUI.UIam.bodygird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
                case "hutui":
                    SetImageSprite(bagDisplayUI.UIam.hutuigird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
                case "xie":
                    SetImageSprite(bagDisplayUI.UIam.xiegird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
                case "xianglian":
                    SetImageSprite(bagDisplayUI.UIam.xiangliangird,
                        bagDisplayUI.mainActorItem.itemList[i].itemImage, whiteColor);
                    break;
            }
        }
    }


}
