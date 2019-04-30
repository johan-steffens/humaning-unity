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
        MENU, HELP, SCORES, CATCHING, ENCOUNTER_QUERY, ENCOUNTER
    }

    public class Limits
    {
        public const int weightMin = 35;
        public const int weightMax = 180;

        public const float genderMin = 0.78f;
        public const float genderMax = 1f;

        public const int fatPercentageMin = 5;
        public const int fatPercentageMax = 75;
    }
}