using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterSceneController : MonoBehaviour
{
    public Image sliderFill;
    public Slider slider;

    public Color filledColor = Color.green;
    public Color emptyColor = Color.red;

    public Text weightValue;
    public Text fatPercentageValue;
    public Text genderValue;

    public float debugValue;

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
        slider.value = debugValue;
        sliderFill.color =  Color.Lerp(emptyColor, filledColor, (float) debugValue / 1f);
    }
}
