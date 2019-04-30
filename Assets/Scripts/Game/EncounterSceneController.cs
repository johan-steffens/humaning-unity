using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterSceneController : MonoBehaviour
{

    public Animator leftPanelAnimator;
    public Animator rightPanelAnimator;

    public GameObject encounterFrame;
    public GameObject successFrame;
    public GameObject failFrame;

    public Sprite[] positiveSprites;
    public Sprite[] negativeSprites;

    public Image title;

    public Image sliderFill;
    public Slider slider;

    public Color filledColor = Color.green;
    public Color emptyColor = Color.red;

    public Text timer;

    public InputField nameInput;
    public Text weightValue;
    public Text fatPercentageValue;
    public Text genderValue;

    public int debugValue;

    private static EncounterSceneController instance;

    private Encounter encounter;
    private float time = 30f;
    private int totalSpaces = 0;
    private int currentSpaces = 0;

    private bool encounterRunning = false;
    private bool shouldMash = false;

    private Sprite currentSprite;

    public static EncounterSceneController GetInstance()
    {
        return instance;
    }

    public void OnBackButtonPressed()
    {
        StartCoroutine(TransitionToMain());
    }

    public void OnSubmitButtonPressed()
    {
        if(nameInput.text != "")
        {
            // Generate score
            Score score = new Score();
            score.name = nameInput.text;
            score.catches = String.Format("{0:.##}", encounter.weight) + "kg" + " - " + encounter.fatPercentage + "% - " + (encounter.gender == Game.Gender.MALE ? "Male" : "Female");
            score.value = encounter.CalculatePercentage();

            // Add score
            Scores scores = new Scores();
            scores.AddScore(score);
        }
        StartCoroutine(TransitionToMain());
    }

    void Start()
    {

        instance = this;
        encounter = GameController.Encounter;

        if(encounter == null)
        {
            encounter = new Encounter();
            encounter.gender = Game.Gender.MALE;
            encounter.size = Game.Size.LARGE;
            encounter.weight = UnityEngine.Random.Range(35, 150);
            encounter.fatPercentage = UnityEngine.Random.Range(5, 75);
        }

        Debug.Log("====================ENCOUNTER STARTED====================");
        Debug.Log("Encounter :: " + encounter + ", value :: " + encounter.CalculatePercentage());
        Debug.Log("=========================================================");

        weightValue.text = "" + String.Format("{0:.##}", encounter.weight) + "kg";
        fatPercentageValue.text = "" + encounter.fatPercentage + "%";
        genderValue.text = "" + (encounter.gender == Game.Gender.MALE ? "Male" : "Female");

        // Calculations based on encounter
        totalSpaces = encounter.CalculatePercentage() * 10 + (10 - encounter.CalculatePercentage());
        time = 20 + (encounter.CalculatePercentage() * 2);
        if(totalSpaces < 10)
        {
            totalSpaces = 10;
        }

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
        if (encounterRunning) {
            // Update time
            time -= Time.deltaTime;
            timer.text = "" + Mathf.RoundToInt(time);

            // Check time
            if(time <= 0)
            {
                encounterRunning = false;
                encounterFrame.SetActive(false);
                failFrame.SetActive(true);
            }

            // Check user input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (shouldMash)
                {
                    currentSpaces++;
                }
                else
                {
                    currentSpaces--;
                }
            }

            // Check whether user has caught human within timeframe
            if(currentSpaces > totalSpaces)
            {
                encounterRunning = false;
                encounterFrame.SetActive(false);
                successFrame.SetActive(true);
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
        timer.text = "3";
        yield return new WaitForSeconds(1);
        timer.text = "2";
        yield return new WaitForSeconds(1);
        timer.text = "1";
        yield return new WaitForSeconds(1);
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
                positiveSprites = ShuffleArray(positiveSprites);
            }
            // Set negative sprites
            else
            {
                for (int i = 0; i < negativeSprites.Length; i++)
                {
                    currentSprite = negativeSprites[i];
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3.5f));
                }
                negativeSprites = ShuffleArray(negativeSprites);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 2.5f));
        }
    }

    private Sprite[] ShuffleArray(Sprite[] array)
    {
        System.Random random = new System.Random();
        Sprite[] shuffled = array.OrderBy(x => random.Next()).ToArray();
        return shuffled;
    }

    public IEnumerator TransitionToMain()
    {
        leftPanelAnimator.SetTrigger("exit");
        rightPanelAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainScene");
    }
}
