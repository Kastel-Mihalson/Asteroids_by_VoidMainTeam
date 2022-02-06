public abstract class ShipModel
{
    private float _moveSpeed;
    private float _turnSpeed;
    private int _currentHP;
    private int _maxHP;
    private int _currentArmor;
    private int _maxArmor;
    private int _score;

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public int MaxHP => _maxHP;
    public int MaxArmor => _maxArmor;
    public int CurrentHP
    {
        get => _currentHP;
        set => _currentHP = value;
    }
    public int CurrentArmor
    {
        get => _currentArmor;
        set => _currentArmor = value;
    }

    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public ShipModel(ShipData data)
    {
        _moveSpeed = data.MoveSpeed;
        _turnSpeed = data.TurnSpeed;
        _maxHP = data.HP;
        _maxArmor = data.Armor;
        _currentHP = _maxHP;
        _currentArmor = _maxArmor;
    }
}
