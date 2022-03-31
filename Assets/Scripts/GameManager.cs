using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;//全局唯一

    private DataBase weaponDB;
    private WeaponFactory weaponFact;

    public WeaponManager testwm;
    void Awake()
    {
        CheckGameObject();
        CheckSingle();
       
    }

    void Start()
    {
        InitWeaponDB();
        IninWeaponFactory();

        Collider col= weaponFact.CreateWeapon("Mace","R", testwm);
        testwm.UpdateCollider("R", col);
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 150, 30), "R:Sword"))
        {
            testwm.UnloadWeapon("R");
            Collider col = weaponFact.CreateWeapon("Sword", "R", testwm);
            testwm.UpdateCollider("R", col);
            testwm.ChangeDualHands(false);
        }
         if (GUI.Button(new Rect(10, 50, 150, 30), "R:Falchion"))
        {
            testwm.UnloadWeapon("R");
            Collider col = weaponFact.CreateWeapon("Falchion", "R", testwm);
            testwm.UpdateCollider("R", col);
            testwm.ChangeDualHands(true);
        }
         if(GUI.Button(new Rect(10, 90, 150, 30), "R:Mace"))
        {
            testwm.UnloadWeapon("R");
            Collider col = weaponFact.CreateWeapon("Mace", "R", testwm);
            testwm.UpdateCollider("R", col);
            testwm.ChangeDualHands(false);
        }
         if (GUI.Button(new Rect(10, 130, 150, 30), "R:Clear all weapons"))
        {
            
        }

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
            DontDestroyOnLoad(gameObject);//防止切换环境消失掉
            return;
        }
        Destroy(this);
    }
}
