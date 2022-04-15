using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;//全局唯一

    private DataBase weaponDB;
    private WeaponFactory weaponFact;

    public ActorManager playerAM;
    public WeaponManager testwm;
    public UIGameManager uigm;

    public bool isGameOver;

    void Awake()
    {
        isGameOver = false;
        CheckGameObject();
        CheckSingle();
        uigm = GetComponent<UIGameManager>();
        testwm=GameObject.Find("PlayerHandle").GetComponent<ActorManager>().wm;
        Time.timeScale = 0;
    }

    void Start()
    {
        InitWeaponDB();
        IninWeaponFactory();

        GameInit();
        addWeapon("R", "mace", false);
    }

    void Update()
    {
        if(isGameOver)
        {
            isGameOver = false;
            if (playerAM.sm.HP>0)
            {
                //you win
                uigm.endGameLoadPanel();
            }
            else
            {
                
            }
        }
    }

    public void GameInit()
    {
        BagItemManager.InitBagAndActor();//初始化背包和人物装备
        BagDisplayUI.updateActorItemToUI();
        BagDisplayUI.updateItemToUI();
    }

    public void addWeapon(string hand,string weaponName,bool DualHand)//DualHand为true时双手持武器
    {
        testwm.UnloadWeapon(hand);
        Collider col = weaponFact.CreateWeapon(weaponName, hand, testwm);
        testwm.UpdateCollider(hand, col);
        testwm.ChangeDualHands(DualHand);
    }

    /// <summary>
    /// /
    /// </summary>

    private void IninWeaponFactory()
    {
        weaponFact = new WeaponFactory(weaponDB);
    }

    private void InitWeaponDB()
    {
        weaponDB = new DataBase();
    }

    private void CheckGameObject()
    {
        if(tag=="GM")
        {
            return;
        }
        Destroy(this);
    }

    private void CheckSingle()
    {
        if(instance==null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);//防止切换环境消失掉
            return;
        }
        Destroy(this);
    }


}
