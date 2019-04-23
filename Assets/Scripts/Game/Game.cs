using UnityEngine;
using UnityEditor;

public class Game
{
    public enum Gender
    {
        MALE, FEMALE
    }

    public enum Size
    {
        SMALL, MEDIUM, LARGE
    }

    public enum State
    {
        MENU, CATCHING, ENCOUNTER_QUERY, ENCOUNTER
    }
}