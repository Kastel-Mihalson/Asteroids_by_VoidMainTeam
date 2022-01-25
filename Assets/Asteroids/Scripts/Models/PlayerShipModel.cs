public sealed class PlayerShipModel : ShipModel
{
    private int _score;


    public PlayerShipModel(ShipData data) : base(data)
    {
        _score = 0;
    }    

    public void ChangeScore(int score)
    {
        _score += score;
    }
}
