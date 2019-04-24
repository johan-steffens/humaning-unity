using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterSceneController : MonoBehaviour
{

    public Text wText;
    public Text eText;
    public Text aText;
    public Text sText;
    public Text dText;
    public Text zText;
    public Text xText;

    public Color hint = Color.blue;
    public Color activated = Color.green;
    public Color error = Color.red;
    public Color deactivated = Color.black;

    public Text weightValue;
    public Text fatPercentageValue;
    public Text genderValue;

    private static EncounterSceneController instance;

    private EncounterSceneController()
    {
        instance = this;
    }

    public static EncounterSceneController GetInstance()
    {
        if (instance == null)
        {
            instance = new EncounterSceneController();
        }
        return instance;
    }

    void Start()
    {
        Encounter encounter = GameController.Encounter;
        Debug.Log("====================ENCOUNTER STARTED====================");
        Debug.Log("Encounter :: " + encounter);
        Debug.Log("=========================================================");

        weightValue.text = "" + String.Format("{0:.##}", encounter.weight) + "kg";
        fatPercentageValue.text = "" + encounter.fatPercentage + "%";
        genderValue.text = "" + (encounter.gender == Game.Gender.MALE ? "Male" : "Female");
    }

    void Update()
    {
        Color wColor = hint;
        Color eColor = deactivated;
        Color aColor = deactivated;
        Color sColor = deactivated;
        Color dColor = deactivated;
        Color zColor = deactivated;
        Color xColor = deactivated;

        // W Key
        if (Input.GetKey(KeyCode.W))
        {
            wColor = activated;
        }
        // E Key
        else if (Input.GetKey(KeyCode.E))
        {
            eColor = activated;
        }
        // A Key
        else if (Input.GetKey(KeyCode.A))
        {
            aColor = activated;
        }
        // S Key
        else if (Input.GetKey(KeyCode.S))
        {
            sColor = activated;
        }
        // D Key
        else if (Input.GetKey(KeyCode.D))
        {
            dColor = activated;
        }
        // Z Key
        else if (Input.GetKey(KeyCode.Z))
        {
            zColor = activated;
        }
        // X Key
        else if (Input.GetKey(KeyCode.X))
        {
            xColor = activated;
        }

        // Set colors
        wText.color = wColor;
        eText.color = eColor;
        aText.color = aColor;
        sText.color = sColor;
        dText.color = dColor;
        zText.color = zColor;
        xText.color = xColor;
    }
}
