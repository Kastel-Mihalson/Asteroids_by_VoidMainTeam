using System;

public abstract class ShipModel
{
    public event Action OnDiedEvent;
    public event Action<int> OnHpChangedEvent;
    public event Action<int> OnArmorChangedEvent;

    private float _moveSpeed;
    private float _turnSpeed;
    private int _currentHP;
    private int _maxHP;
    private int _currentArmor;
    private int _maxArmor;

    public float MoveSpeed
    {
        get => _moveSpeed;
        private set => _moveSpeed = value;
    }
    public float TurnSpeed
    {
        get => _turnSpeed;
        private set => _turnSpeed = value;
    }

    public int MaxHP
    {
        get => _maxHP;
        private set => _maxHP = value;
    }

    public int MaxArmor
    {
        get => _maxArmor;
        private set => _maxArmor = value;
    }

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

    public ShipModel(ShipData data)
    {
        _moveSpeed = data.MoveSpeed;
        _turnSpeed = data.TurnSpeed;
        _maxHP = data.HP;
        _maxArmor = data.Armor;
        _currentHP = _maxHP;
        _currentArmor = _maxArmor;
    }

    public void RecieveDamage(int damage)
    {
        if (CurrentArmor > 0)
        {
            CurrentArmor -= damage;
            OnArmorChangedEvent?.Invoke(CurrentArmor);
        }
        else
        {
            CurrentHP -= damage;
            OnHpChangedEvent?.Invoke(CurrentHP);
        }

        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDiedEvent?.Invoke();
    }
}
