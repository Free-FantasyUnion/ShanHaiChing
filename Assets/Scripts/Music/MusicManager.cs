using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
     public static AudioClip clikEffect;
     public static AudioClip chooseLevel;
     public static AudioClip BGM;
     public static AudioClip absorbGenki;
     public static AudioClip[] atk;

     static AudioSource source;

    
    private void Start()
    {
        source = this.GetComponent<AudioSource>();

        clikEffect = Resources.Load<AudioClip>("Musics/BGM&Effects/点击按钮");
        absorbGenki = Resources.Load<AudioClip>("Musics/BGM&Effects/吸收元气");
        chooseLevel = Resources.Load<AudioClip>("Musics/BGM&Effects/关卡选择");

        atk = Resources.LoadAll<AudioClip>("Musics/BGM&Effects/atk");
        
    }


    //TODO: 加载音乐直接调用静态函数即可

    public static void PlayMusic(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void ClickMusic()
    {
        PlayMusic(clikEffect);
    }
    public void ChooseLevel()
    {
        PlayMusic(chooseLevel);
    }
    public void GatherGenki()
    {
        PlayMusic(absorbGenki);
    }


}
