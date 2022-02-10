using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public sealed class LevelData : ScriptableObject
{
    [SerializeField] private List<AsteroidLevelConfiguration> _asteroids;

    public List<AsteroidLevelConfiguration> Asteroids => _asteroids;
}