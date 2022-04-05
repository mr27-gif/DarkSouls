using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void startGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void retrunGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); 
    }
}
