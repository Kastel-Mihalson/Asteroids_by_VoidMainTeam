using System;
using UnityEngine;


public class ShipView : MonoBehaviour, IInteractiveObject, IShip
{
    public event Action<int> OnDamaged;

    private GameObject _explosionEffect;
    private float _effectTime = 2f;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    public Transform BulletSpawnPoint => gameObject.GetComponentInChildren<BulletSpawnMarker>().transform;

    private void Start()
    {
        _explosionEffect = Resources.Load("Explosion/Explosion") as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = 10;
        OnDamaged?.Invoke(damage); // how to get DAMAGE from other collider? DamageProvider?
    }

    public void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, _effectTime);
    }
}
