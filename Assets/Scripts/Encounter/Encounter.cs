using UnityEngine;
using UnityEditor;

public class Encounter
{

    public Game.Size size;
    public float weight;
    public Game.Gender gender;
    public int fatPercentage;
    public int stars;

    public void CalculateStars()
    {
        float genderModifier = 1f;
        if(gender == Game.Gender.FEMALE)
        {
            genderModifier = 0.78f;
        }

        // Calculate value out of 100 for encounter
        int totalValue = Mathf.RoundToInt(weight / fatPercentage * genderModifier);

        Debug.Log("TOTAL VALUE = " + totalValue);

        // Assign stars based on total value
        stars = 1;
        if(totalValue >= 25 && totalValue <= 49)
        {
            stars = 2;
        } else if(totalValue >= 50)
        {
            stars = 3;
        }        
    }

    public override string ToString()
    {
        return "Encounter {"
            + "size=" + size + ", "
            + "weight=" + weight + ", "
            + "gender=" + gender + ", "
            + "fatPercentage=" + fatPercentage + ", "
            + "stars=" + stars + ", ";
    }
}