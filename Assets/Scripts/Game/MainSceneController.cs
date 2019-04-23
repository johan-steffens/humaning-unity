using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    private Game.State state = Game.State.CATCHING;

    public GameObject player;
    public GameObject encounterFrame;

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

    public void OnEncounterFrameAccept()
    {
        SetState(Game.State.ENCOUNTER);
    }

    public void OnEncounterFrameDeny()
    {
        SetState(Game.State.CATCHING);
    }

    public void SetState(Game.State state)
    {
        preStateChange(state);
        this.state = state;
        postStateChange();
    }

    public Game.State GetState()
    {
        return state;
    }

    private void preStateChange(Game.State changeToState)
    {
        // When a human is caught and user is queried whether they want to attempt to cat or not
        if(state == Game.State.CATCHING && changeToState == Game.State.ENCOUNTER_QUERY)
        {
            encounterFrame.SetActive(true);
        }
        // User has denied catching the human
        else if(state == Game.State.ENCOUNTER_QUERY && changeToState == Game.State.CATCHING)
        {
            encounterFrame.SetActive(false);
        }
        // User has accepted catching the human
        else if(state == Game.State.ENCOUNTER_QUERY && changeToState == Game.State.ENCOUNTER)
        {

        }
    }

    private void postStateChange()
    {
        // Todo: notify listeners
    }

}
