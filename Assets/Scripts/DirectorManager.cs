using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{
    public PlayableDirector pd;

    public TimelineAsset frontStab;
    public TimelineAsset openBox;

    public ActorManager attacker;
    public ActorManager victim;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        //pd.playableAsset = Instantiate(frontStab);



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pd.Play();
        }
    }

    public bool IsPlaying()
    {
        if (pd.state == PlayState.Playing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayFrontStab(string timelineName, ActorManager attacker, ActorManager victim)//���Ŷ�Ӧ�ĵ���Ƭ��
    {
        //if(pd.state==PlayState.Playing)
        //{
        //    return;
        //}
        if (timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);//ʵ��������Ƭ��

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;//��ȡ���������ϵĶ���Ƭ��
            foreach (var track in timeline.GetOutputTracks())
            {
                if(track.name=="Attacker Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        pd.SetReferenceValue(myclip.myCamera.exposedName, attacker);//�ֵ�
                    }
                }
                else if (track.name == "Victim Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        //myclip.myCamera.exposedName = GetInstanceID().ToString();
                        pd.SetReferenceValue(myclip.myCamera.exposedName, victim);//�ֵ�
                    }
                }
                else if (track.name == "Attacker Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if (track.name == "Victim Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            pd.Evaluate();
            pd.Play();
        }

        else if(timelineName=="openBox")
        {
            pd.playableAsset = Instantiate(openBox);//ʵ��������Ƭ��

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;//��ȡ���������ϵĶ���Ƭ��
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        pd.SetReferenceValue(myclip.myCamera.exposedName, attacker);//�ֵ�
                    }
                }
                else if (track.name == "Box Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        //myclip.myCamera.exposedName = GetInstanceID().ToString();
                        pd.SetReferenceValue(myclip.myCamera.exposedName, victim);//�ֵ�
                    }
                }
                else if (track.name == "Player Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if (track.name == "Box Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            pd.Evaluate();
            pd.Play();
        }

    }


}
