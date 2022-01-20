using UnityEngine;

public sealed class EffectController
{
    private static EffectData _effectData;

    public EffectController(EffectData effectData)
    {
        _effectData = effectData;
    }

    public static void Init(EffectManager effect, Transform root)
    {
        // TODO pool
        GameObject prefab = _effectData.Effects.GetEffectPrefab(effect);
        GameObject effectGameObject = Object.Instantiate(prefab);        
        effectGameObject.transform.position = root.transform.position;
        effectGameObject.transform.rotation = root.transform.rotation;
        float lifeTime = _effectData.Effects.GetEffectTime(effect);
        Object.Destroy(effectGameObject, lifeTime);
    }
}