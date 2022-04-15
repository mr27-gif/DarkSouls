using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class WeaponFactory 
{
    private DataBase weaponDB;
    public WeaponFactory(DataBase _weaponDB)
    {
        weaponDB = _weaponDB;
    }

    public GameObject CreateWeapon(string weaponName,Vector3 pos,Quaternion rot)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj= GameObject.Instantiate(prefab, pos, rot);

        WeaponData wdata= obj.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.wepondataBase[weaponName]["ATK"].floatValue;

        return obj;
    }

    public Collider CreateWeapon(string weaponName, string side,WeaponManager wm)//实例化预制体到weaponHandleL/R 返回武器collider
    {
        WeaponController wc;
        if(side=="L")
        {
            wc = wm.wcL;
        }
        else if(side=="R")
        {
            wc = wm.wcR;
        }
        else
        {
            return null;
        }
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj= GameObject.Instantiate(prefab);

        //实例化预制体到weaponHandleL/R
        obj.transform.parent = wc.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        WeaponData wdata = obj.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.wepondataBase[weaponName]["ATK"].floatValue;
        wc.wData = wdata;

        return obj.GetComponent<Collider>();
    }

}
