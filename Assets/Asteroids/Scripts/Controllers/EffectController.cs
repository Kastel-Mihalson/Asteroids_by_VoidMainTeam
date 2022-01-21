using UnityEngine;

public sealed class EffectController
{
    private const string EFFECT = "Effect";
    private static EffectData _effectData;
    private static GameObject _root;

    public EffectController(EffectData effectData)
    {
        _effectData = effectData;
        _root = new GameObject($"[{EFFECT}]");
    }

    public static void Create(EffectManager effect, Transform root)
    {
        GameObject prefab = _effectData.Effects.GetEffectPrefab(effect);
        if (prefab == null)
        {
            Debug.Log($"{effect} is missing in {nameof(EffectData)}.");
            return;
        }

        // TODO pool
        GameObject effectGameObject = Object.Instantiate(prefab);        
        effectGameObject.transform.position = root.transform.position;
        effectGameObject.transform.rotation = root.transform.rotation;
        effectGameObject.transform.SetParent(_root.transform);
        float lifeTime = _effectData.Effects.GetEffectTime(effect);
        Object.Destroy(effectGameObject, lifeTime);
    }
}