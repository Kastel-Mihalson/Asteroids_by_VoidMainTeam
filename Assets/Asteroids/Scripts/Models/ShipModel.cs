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

    public float MoveSpeed=> _moveSpeed;
    public float TurnSpeed=> _turnSpeed;
    public int MaxHP => _maxHP;
    public int MaxArmor=> _maxArmor;

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
        if (_currentArmor > 0)
        {
            _currentArmor -= damage;
            OnArmorChangedEvent?.Invoke(_currentArmor);
        }
        else
        {
            _currentHP -= damage;
            OnHpChangedEvent?.Invoke(_currentHP);
        }

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDiedEvent?.Invoke();
    }
}
