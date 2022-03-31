using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;

    public WeaponData wData;

    private void Awake()
    {
        wData = GetComponentInChildren<WeaponData>();
    }
    public float GetATK()
    {
        return wData.ATK+wm.am.sm.ATK;
    }
}
