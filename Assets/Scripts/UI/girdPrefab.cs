using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class girdPrefab : MonoBehaviour
{
    public Image childImg;
    public Text childNumText;
    public string type;
    public string prefabName;

    public UIActorManager UIam;
    public BagManager bagManager;

    private void Awake()
    {
        UIam = GameObject.Find("actorContext").GetComponent<UIActorManager>();
        bagManager = GameObject.Find("GM").GetComponent<BagManager>();
    }

    public void OnClickEvent()
    {
        if (UIam == null)
        {
            print("Î´¸ø UIActorManager ¸³Öµ");
        }
        else if(type!="yao")
        {
            UIam.AddActorItem(BagDisplayUI.FindItem(prefabName));
            BagDisplayUI.updateItemToUI();
            BagDisplayUI.updateActorItemToUI();
        }
        else
        {
            UIam.UseBagItem(BagDisplayUI.FindItem(prefabName));
            UIam.gm.testwm.am.sm.AddHp(50);
            bagManager.uiGM.moveBuddle("+ 50HP");
            BagDisplayUI.updateItemToUI();
        }
    }

}
