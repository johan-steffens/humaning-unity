﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    private Game.State state = Game.State.MENU;

    public GameObject player; 
    public GameObject menuFrame;
    public GameObject hudFrame;
    public GameObject encounterFrame;
    public GameObject helpFrame;
    public GameObject scoreFrame;

    public Animator leftPanelAnimator;
    public Animator rightPanelAnimator;

    public Image leftStar;
    public Image middleStar;
    public Image rightStar;

    public Sprite emptyStar;
    public Sprite filledStar;

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

    public void OnMenuClicked()
    {
        SetState(Game.State.MENU);
    }

    public void OnMenuPlayClicked()
    {
        SetState(Game.State.CATCHING);
    }

    public void OnMenuHelpClicked()
    {
        SetState(Game.State.HELP);
    }

    public void OnMenuScoresClicked()
    {
        SetState(Game.State.SCORES);
    }

    public void OnScoresBackClicked()
    {
        SetState(Game.State.MENU);
    }

    public void OnMenuExitClicked()
    {
        #if UNITY_EDITOR
        if (Application.isEditor) 
        {
            UnityEditor.EditorApplication.isPlaying = false;
        } 
        #endif
        Application.Quit();
    }

    public void OnHelpBackClicked()
    {
        SetState(Game.State.MENU);
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
        // When the play button is clicked in the menu
        if(state == Game.State.MENU && changeToState == Game.State.CATCHING)
        {
            hudFrame.SetActive(true);
            menuFrame.SetActive(false);
        }
        // When the scores button is clicked in the menu
        else if (state == Game.State.MENU && changeToState == Game.State.SCORES)
        {
            scoreFrame.SetActive(true);
            menuFrame.SetActive(false);
        }
        // When the help button is clicked in the menu
        else if (state == Game.State.MENU && changeToState == Game.State.HELP)
        {
            helpFrame.SetActive(true);
            menuFrame.SetActive(false);
        }
        // When the back button is clicked in the help or scores frame
        else if ((state == Game.State.HELP || state == Game.State.SCORES) && changeToState == Game.State.MENU)
        {
            helpFrame.SetActive(false);
            scoreFrame.SetActive(false);
            menuFrame.SetActive(true);
        }
        // When a human is caught and user is queried whether they want to attempt to cat or not
        else if(state == Game.State.CATCHING && changeToState == Game.State.ENCOUNTER_QUERY)
        {
            hudFrame.SetActive(false);
            encounterFrame.SetActive(true);
        }
        // User has denied catching the human
        else if(state == Game.State.ENCOUNTER_QUERY && changeToState == Game.State.CATCHING)
        {
            hudFrame.SetActive(true);
            encounterFrame.SetActive(false);
        }
        // User has accepted catching the human
        else if(state == Game.State.ENCOUNTER_QUERY && changeToState == Game.State.ENCOUNTER)
        {
            StartCoroutine(TransitionToEncounter(0));
        }
        // Menu button was clicked
        else if(changeToState == Game.State.MENU)
        {
            hudFrame.SetActive(false);
            menuFrame.SetActive(true);
        }
    }

    private void postStateChange()
    {
        if(state == Game.State.ENCOUNTER_QUERY)
        {
            Encounter encounter = GameController.Encounter;

            // Set second star
            if (encounter.stars >= 2)
            {
                middleStar.sprite = filledStar;
            } else
            {
                middleStar.sprite = emptyStar;
            }

            // Set third star
            if(encounter.stars >= 3)
            {
                rightStar.sprite = filledStar;
            } else
            {
                rightStar.sprite = emptyStar;
            }
        }
    }

    public IEnumerator TransitionToEncounter(int parameter)
    {
        leftPanelAnimator.SetTrigger("exit");
        rightPanelAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("EncounterScene");
    }

}
