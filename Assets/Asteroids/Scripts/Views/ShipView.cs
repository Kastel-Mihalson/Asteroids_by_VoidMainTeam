using System;
using UnityEngine;


public class ShipView : MonoBehaviour, IInteractiveObject, IShip
{
    public event Action<int> OnDamagedEvent;
    public GameObject UI_lose;

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
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            if(interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                int? damage = bulletView.GetBulletDamage();
                if (damage != null)
                {
                    OnDamagedEvent?.Invoke((int)damage);
                }
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, _effectTime);

    }
}
