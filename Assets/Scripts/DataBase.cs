using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class DataBase
{
    private string weaponDatabaseFileName="weaponData";
    public readonly JSONObject wepondataBase;

    public DataBase()
    {
        TextAsset weaponContent = Resources.Load(weaponDatabaseFileName) as TextAsset;
        wepondataBase = new JSONObject(weaponContent.text);
        //print(abc["Falchion"]["DEF"].floatValue);
    }
}
    
