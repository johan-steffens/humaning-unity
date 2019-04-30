using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceEncounter : MonoBehaviour
{
    public Game.Size size = Game.Size.LARGE;

    // Start is called before the first frame update
    void Start()
    {
        // Start in random direction
        int modX = Random.Range(0, 2);
        int modY = Random.Range(0, 2);
        GetComponent<Rigidbody2D>().velocity = new Vector2((modX == 0 ? -1 : 1) * Random.Range(0, 10), (modX == 0 ? -1 : 1) * -Random.Range(0, 10));
    }

    public Encounter GenerateEncounter()
    {
        Encounter encounter = new Encounter();
        encounter.size = size;
        encounter.fatPercentage = Random.Range(Game.Limits.fatPercentageMin * GetMinFatModifierBySize(), Game.Limits.fatPercentageMax / GetMaxFatModifierBySize());
        encounter.gender = Random.Range(0, 2) == 0 ? Game.Gender.MALE : Game.Gender.FEMALE;
        encounter.weight = Random.Range((float) Game.Limits.weightMin * GetMinWeightModifierBySize(), (float) Game.Limits.weightMax / GetMaxWeightModifierBySize());
        encounter.CalculateStars();
        return encounter;
    }

    private int GetMaxFatModifierBySize() 
    {
        if(size == Game.Size.LARGE)
        {
            return 1;
        } else if(size == Game.Size.MEDIUM)
        {
            return 5;
        }
        else // Game.Size.SMALL
        {
            return 7;
        }
    }

    private int GetMinFatModifierBySize()
    {
        if (size == Game.Size.LARGE)
        {
            return 5;
        }
        else if (size == Game.Size.MEDIUM)
        {
            return 1;
        }
        else // Game.Size.SMALL
        {
            return 1;
        }
    }

    private float GetMaxWeightModifierBySize()
    {
        if (size == Game.Size.LARGE)
        {
            return 3;
        }
        else if (size == Game.Size.MEDIUM)
        {
            return 1;
        }
        else // Game.Size.SMALL
        {
            return 1;
        }
    }

    private float GetMinWeightModifierBySize()
    {
        if (size == Game.Size.LARGE)
        {
            return 1;
        }
        else if (size == Game.Size.MEDIUM)
        {
            return 4;
        }
        else // Game.Size.SMALL
        {
            return 5;
        }
    }


}
