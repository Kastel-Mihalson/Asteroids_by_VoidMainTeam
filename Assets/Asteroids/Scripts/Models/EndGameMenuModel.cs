public sealed class EndGameMenuModel
{
    public const string WIN_GAME = "<color=#17D133>YOU WIN :)</color>";
    public const string LOSE_GAME = "<color=#D13817>YOU LOSE :(</color>";

    private int _score;

    public int Score
    {
        get => _score;
        set => _score = value;
    }
}