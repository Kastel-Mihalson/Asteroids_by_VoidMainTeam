using UnityEngine;
using System;

public sealed class ShipModel
{
    public event Action OnDied;
    private float _moveSpeed;
    private float _turnSpeed;
    private float _currentHP;
    private float _maxHP;
    private float _currentArmor;
    private float _maxArmor;

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
        }
        else
        {
            _currentHP -= damage;
        }

        Debug.Log($"hp = {_currentHP}, ar = {_currentArmor}");

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDied?.Invoke();
    }
}
