using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ʵ�ֿ�����Asset���ڴ�����Դ�ļ��ķ���
[CreateAssetMenu(fileName ="New Item",menuName ="Bag/New Item")]

public class Item :ScriptableObject
{
    public string itemName;//��Դ����
    public Sprite itemImage;//��ԴͼƬ
    public int itemNum;
    public string type;
    [TextArea]
    public string itemInfo;

}
