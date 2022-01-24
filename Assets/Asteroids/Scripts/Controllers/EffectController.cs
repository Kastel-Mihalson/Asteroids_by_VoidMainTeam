using UnityEngine;

public sealed class EffectController
{
    private const string EFFECT = "Effect";
    private static EffectData _effectData;
    private static GameObject _root;
    private static float _yPosition = 2f;

    public EffectController(EffectData effectData)
    {
        _effectData = effectData;
        _root = new GameObject($"[{EFFECT}]");
;    }

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
        effectGameObject.transform.position = new Vector3(root.position.x, _yPosition, root.position.z);
        effectGameObject.transform.rotation = root.rotation;
        effectGameObject.transform.SetParent(_root.transform);
        float lifeTime = _effectData.Effects.GetEffectTime(effect);
        Object.Destroy(effectGameObject, lifeTime);
    }
}