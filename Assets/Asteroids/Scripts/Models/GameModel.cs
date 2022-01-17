using System.Collections.Generic;
using UnityEngine;

public sealed class GameModel
{
    private float _leftScreenBorder;
    private float _rightScreenBorder;
    private float _topScreenBorder;
    private float _bottomScreenBorder;
    private List<string> _startLoadedPrefabNames;
    private Vector3 _movement;

    public float LeftScreenBorder
    {
        get => _leftScreenBorder;
        set => _leftScreenBorder = value;
    }
    public float RightScreenBorder
    {
        get => _rightScreenBorder;
        set => _rightScreenBorder = value;
    }
    public float TopScreenBorder
    {
        get => _topScreenBorder;
        set => _topScreenBorder = value;
    }
    public float BottomScreenBorder
    {
        get => _bottomScreenBorder;
        set => _bottomScreenBorder = value;
    }
    public List<string> StartLoadedPrefabNames => _startLoadedPrefabNames;
    public Vector3 Movement
    {
        get => _movement;
        set => _movement = value;
    }

    public GameModel(GameData gameData)
    {
        _startLoadedPrefabNames = gameData.StartLoadedPrefabNames;
    }
}
