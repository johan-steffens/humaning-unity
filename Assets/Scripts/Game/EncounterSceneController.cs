using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterSceneController : MonoBehaviour
{
    public Sprite[] positiveSprites;
    public Sprite[] negativeSprites;

    public Image title;

    public Image sliderFill;
    public Slider slider;

    public Color filledColor = Color.green;
    public Color emptyColor = Color.red;

    public Text weightValue;
    public Text fatPercentageValue;
    public Text genderValue;

    public int debugValue;

    private static EncounterSceneController instance;

    private Encounter encounter;
    private int totalSpaces = 0;
    private int currentSpaces = 0;

    private bool encounterRunning = false;
    private bool shouldMash = false;

    private Sprite currentSprite;

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
        // encounter = GameController.Encounter;
        encounter = new Encounter();
        encounter.weight = 500;
        encounter.fatPercentage = 12;
        encounter.gender = Game.Gender.MALE;

        Debug.Log("====================ENCOUNTER STARTED====================");
        Debug.Log("Encounter :: " + encounter);
        Debug.Log("=========================================================");

        weightValue.text = "" + String.Format("{0:.##}", encounter.weight) + "kg";
        fatPercentageValue.text = "" + encounter.fatPercentage + "%";
        genderValue.text = "" + (encounter.gender == Game.Gender.MALE ? "Male" : "Female");

        totalSpaces = encounter.CalculatePercentage();

        // Start encounter
        StartCoroutine(StartEncounterMashing());
    }

    void Update()
    {
        if(debugValue != 0f)
        {
            currentSpaces = debugValue;
        }

        slider.value = (float)currentSpaces / (float)totalSpaces;
        sliderFill.color =  Color.Lerp(emptyColor, filledColor, (float) currentSpaces / (float) totalSpaces);

        // Space pressed
        if(encounterRunning && Input.GetKeyDown(KeyCode.Space))
        {
            if(shouldMash)
            {
                currentSpaces++;
            } else
            {
                currentSpaces--;
            }
        }
    }

    void LateUpdate()
    {
        if(currentSprite != null)
        {
            title.sprite = currentSprite;
        }

        // Ensure sprites don't stretch
        title.SetNativeSize();
    }

    private IEnumerator StartEncounterMashing()
    {
        // Wait for 3 seconds initially
        yield return new WaitForSeconds(3);
        encounterRunning = true;

        // Run encounter
        while(encounterRunning)
        {
            // Switch value from before
            shouldMash = !shouldMash;

            // Set positive sprites
            if (shouldMash)
            {
                for (int i = 0; i < positiveSprites.Length; i++)
                {
                    currentSprite = positiveSprites[i];
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3.5f));
                }
            }
            // Set negative sprites
            else
            {
                for (int i = 0; i < negativeSprites.Length; i++)
                {
                    currentSprite = negativeSprites[i];
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3.5f));
                }
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 2.5f));
        }
    }
}
