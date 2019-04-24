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
}
