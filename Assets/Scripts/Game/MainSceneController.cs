using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public Game.State currentState = Game.State.CATCHING;

    public GameObject player;

    private static MainSceneController instance;

    private MainSceneController() {
        instance = this;
    }

    public static MainSceneController GetInstance()
    {
        if(instance == null)
        {
            instance = new MainSceneController();
        }
        return instance;
    }



}
