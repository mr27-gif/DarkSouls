using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITipsPanel : MonoBehaviour
{
    public bool isPlay;

    // Start is called before the first frame update
    void Start()
    {
        isPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlay==true)
        {
            isPlay = false;
            StartCoroutine(DisplayTipsPanel());
        }
    }

    IEnumerator DisplayTipsPanel()
    {
        yield return new WaitForSeconds(1.8f);
        gameObject.transform.position = new Vector3(99999, 99999, 99999);
    }
}
