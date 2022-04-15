using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//实现可以在Asset窗口创建资源文件的方法
[CreateAssetMenu(fileName ="New Item",menuName ="Bag/New Item")]

public class Item :ScriptableObject
{
    public string itemName;//资源名字
    public Sprite itemImage;//资源图片
    public int itemNum;
    public string type;
    [TextArea]
    public string itemInfo;

}
