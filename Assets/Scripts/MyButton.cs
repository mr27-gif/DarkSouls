using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    public bool IsPressing=false;//��ͬ��getkey
    public bool OnPressed=false;//getkeydown
    public bool OnReleased = false;//getkeyup
    public bool IsExtending = false;//���ֺ�һ��ʱ����״̬
    public bool IsDelaying = false;//��סһ��ʱ���ſ���״̬

    public float extendingDuration = 0.15f;
    public float delayingDuration = 0.15f;

    private bool curState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();
    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        curState = input;

        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if (curState!=lastState)
        {
            if(curState==true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);//�ͷ����ٵ���1.0f ��������״̬
            }
        }
        lastState = curState;
        if (extTimer.state == MyTimer.STATE.RUN)
        {
            IsExtending = true;
        }
        if(delayTimer.state==MyTimer.STATE.RUN)
        {
            IsDelaying = true;
        }

    }

    private void StartTimer(MyTimer timer,float duration)
    {
        timer.duration = duration;
        timer.Go();
    }

}
