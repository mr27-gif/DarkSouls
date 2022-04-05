using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MainBagItem", menuName = "Bag/New MainBagItem")]

public class MainBagItem : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
