using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIGameManager : MonoBehaviour
{
    private bool isDisplay;
    private UITipsPanel uiTipsPanel;

    private Text tipsText;
    public Image tipsPanel;
    public GameObject buddle;
    public Text buddleText;

    public Image loadPanel;
    public GameObject[] loadpanelChildren;

    private Vector3 outScreenPosion;

    void Awake()
    {
        uiTipsPanel = tipsPanel.gameObject.GetComponent<UITipsPanel>();
        tipsText = tipsPanel.gameObject.GetComponentInChildren<Text>();
        buddleText = buddle.gameObject.GetComponentInChildren<Text>();
        isDisplay = false;
        outScreenPosion = new Vector3(99999, 99999, 99999);
    }
    void Start()
    {
        loadpanelChildren[0].gameObject.SetActive(false);
        loadpanelChildren[1].gameObject.SetActive(false);

        tipsPanel.gameObject.transform.position = outScreenPosion;
        buddle.gameObject.transform.position = outScreenPosion;
        StartCoroutine(DisplayTipsPanel());
    }

    void FixedUpdate()
    {

    }

    IEnumerator DisplayTipsPanel()
    {
        yield return new WaitForSeconds(3.0f);
        tipsPanel.gameObject.transform.localPosition = new Vector3(0, -87, 0);
        uiTipsPanel.isPlay = true;
    }

    public void displayTipsPanel(string context)
    {
        tipsText.text = context;
        tipsPanel.gameObject.transform.localPosition = new Vector3(0, -87, 0);
        uiTipsPanel.isPlay = true;
    }

    public void moveBuddle(string context)
    {
        buddle.gameObject.transform.localPosition = new Vector3(0, -58, 0);
        buddleText.text = context;
        buddle.transform.DOLocalMoveY(10.0f, 1);
        buddleText.DOFade(0, 1).OnComplete(
            () =>
            {
                //动画执行完后
                buddle.gameObject.transform.position = outScreenPosion;
            }
            );
    }

    public void endGameLoadPanel()
    {
        setLoadPanel(true);
        Cursor.lockState = CursorLockMode.Confined;

       loadpanelChildren[0].gameObject.SetActive(true);
        loadpanelChildren[1].gameObject.SetActive(true);
    }

    public void setLoadPanel(bool isShow)
    {
        if(isShow)
        {
            loadPanel.gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            loadPanel.gameObject.transform.position = outScreenPosion;
        }
    }

    public void loadPanelBtnStartGame()
    {
        loadPanel.gameObject.transform.position = outScreenPosion;
        loadPanel.gameObject.GetComponent<Button>().enabled = false;
        loadpanelChildren[2].gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void loadPanel_BtnRetrun()
    {
        SceneManager.LoadScene("start");
    }

    public void loadPanel_BtnEnd()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); 
    }
}
