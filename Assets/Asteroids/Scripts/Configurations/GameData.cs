using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField]
    private List<string> _startLoadedPrefabNames = new List<string>
    {
        "PlayerV2",
        "Bullet",
        "Asteroid"
    };

    public List<string> StartLoadedPrefabNames => _startLoadedPrefabNames;
}
