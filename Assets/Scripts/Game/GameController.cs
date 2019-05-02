public static class GameController
{
    private static Encounter encounter;
    public static Encounter Encounter
    {
        get
        {
            return encounter;
        }
        set
        {
            encounter = value;
        }
    }

    private static bool gameStarted = false;
    public static bool HasGameStarted
    {
        get
        {
            return gameStarted;
        }
        set
        {
            gameStarted = value;
        }
    }
}
