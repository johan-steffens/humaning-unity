using UnityEngine;
using UnityEditor;

public class Encounter
{

    public Game.Size size;
    public float weight;
    public Game.Gender gender;
    public int fatPercentage;
    public int stars;

    public int CalculatePercentage()
    {
        float genderModifier = 1f;
        if (gender == Game.Gender.FEMALE)
        {
            genderModifier = 0.78f;
        }

        // Calculate value out of 100 for encounter
        return Mathf.RoundToInt(weight / fatPercentage * genderModifier);
    }

    public void CalculateStars()
    {
        int totalValue = CalculatePercentage();
        Debug.Log("TOTAL VALUE = " + totalValue);

        // Assign stars based on total value
        stars = 1;
        if(totalValue >= 12 && totalValue <= 27)
        {
            stars = 2;
        } else if(totalValue >= 28)
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