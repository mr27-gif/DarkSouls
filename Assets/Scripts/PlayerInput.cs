using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [Header("==== Key settiings ====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyJump;

    public MyButton buttonUp = new MyButton();
    public MyButton buttonDown= new MyButton();
    public MyButton buttonLeft = new MyButton();
    public MyButton buttonRight = new MyButton();
    public MyButton buttonJump = new MyButton();
    public MyButton buttonLAttack = new MyButton();
    public MyButton buttonRAttack = new MyButton();
    public MyButton buttonC = new MyButton();

    [Header("==== Output signals ====")]
    public float Dup;
    public float Dright;
    public float Dmag;//距离
    public Vector3 Dvec;//方向
    public float Jup;
    public float Jright;

    public bool run=false;
    public bool defense=false;
    public bool action=false;
    public bool jump=false;
    //public bool attack;
    public bool roll=false;
    public bool lockon=false;
    public bool lb=false;
    public bool lt=false;
    public bool rb=false;
    public bool rt=false;
    public bool jumpattack = false;

    public bool switchDualHand=false;


    [Header("==== Others ====")]

    public bool inputEnabled = true;

    public float targetDup;
    public float targetDright;
    public float velocityDup;
    public float velocityDright;

    void Update()
    {
        buttonUp.Tick(Input.GetKey(keyUp));
        buttonDown.Tick(Input.GetKey(keyDown));
        buttonLeft.Tick(Input.GetKey(keyLeft));
        buttonRight.Tick(Input.GetKey(keyRight));
        buttonJump.Tick(Input.GetKey(keyJump));
        buttonLAttack.Tick(Input.GetKey(KeyCode.LeftControl));
        buttonRAttack.Tick(Input.GetKey(KeyCode.LeftShift));
        buttonC.Tick(Input.GetKey(KeyCode.Y));

        Jup = Input.GetAxis("Mouse Y")*3f;
        Jright = Input.GetAxis("Mouse X")*2.5f;

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if(inputEnabled==false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        //方形映射到圆形
        Vector2 CircleVec2 = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = CircleVec2.x;
        float Dup2 = CircleVec2.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec= Dright * transform.right + Dup * transform.forward;
        //23.5.45
        run = (buttonJump.IsPressing&&!buttonJump.IsDelaying)|| buttonJump.IsExtending;
        jump = buttonJump.OnPressed&&buttonJump.IsExtending;
        roll = buttonJump.OnReleased && buttonJump.IsDelaying;
        action = buttonC.OnPressed;

        lt = buttonLAttack.OnPressed;
        rb = Input.GetMouseButtonDown(0);
        rt = buttonRAttack.OnPressed;
        lb = Input.GetMouseButtonDown(1);

        jumpattack= buttonJump.IsPressing&& Input.GetMouseButtonDown(0);

        defense = Input.GetMouseButton(1);
        lockon = Input.GetMouseButtonDown(2);

        switchDualHand = Input.GetKeyDown(KeyCode.Tab);

    }

    public Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
