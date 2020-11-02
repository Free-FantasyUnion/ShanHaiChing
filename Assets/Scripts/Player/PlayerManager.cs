using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = new PlayerManager();
    private PlayerManager()
    {

    }

    void Start()
    {

    }


    void Update()
    {

    }

    public static PlayerManager GetInstance()
    {
        return instance;
    }


}
