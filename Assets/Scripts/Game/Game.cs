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

    public class Limits
    {
        public const int weightMin = 50;
        public const int weightMax = 500;

        public const float genderMin = 0.78f;
        public const float genderMax = 1f;

        public const int fatPercentageMin = 5;
        public const int fatPercentageMax = 80;
    }
}