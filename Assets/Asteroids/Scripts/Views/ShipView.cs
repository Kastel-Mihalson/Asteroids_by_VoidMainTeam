using System;
using UnityEngine;


public class ShipView : MonoBehaviour, IInteractiveObject, IShip
{
    public event Action<int> OnDamagedEvent;

    public EndGameMenuView loseMenu;
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    public Transform BulletSpawnPoint => gameObject.GetComponentInChildren<BulletSpawnMarker>().transform;
    private int gameObjectLayer;

    private void Awake()
    {
        gameObjectLayer = gameObject.layer;
        loseMenu = FindObjectOfType<EndGameMenuView>();
    }

    private void Start()
    {
        if (loseMenu)
        {
            loseMenu.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            if (interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                int? damage = bulletView.GetBulletDamage();
                if (damage != null)
                {
                    OnDamagedEvent?.Invoke((int)damage);
                }
                AudioController.Play(AudioClipManager.ShipHitting);
                EffectController.Create(EffectManager.ShipHitting, gameObject.transform);
            }
            else if (interactiveObject is IAsteroid)
            {
                var asteroidView = (AsteroidView)interactiveObject;
                int? damage = asteroidView.GetAsteroidDamage();
                if (damage != null)
                {
                    OnDamagedEvent?.Invoke((int)damage);
                }
            }
        }
    }

    public void Die()
    {
        if (loseMenu)
        {
            loseMenu.SetScreenActive(true);
            loseMenu.SetGameEndParams(gameObjectLayer);
        }

        AudioController.Play(AudioClipManager.ShipExplosion);
        EffectController.Create(EffectManager.ShipExplosion, gameObject.transform);
        Destroy(gameObject);
    }
}
