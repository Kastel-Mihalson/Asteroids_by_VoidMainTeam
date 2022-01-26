using System;
using UnityEngine;

public abstract class ShipView : MonoBehaviour, IInteractiveObject, IShip
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
        Interact(other);
    }

    public abstract void Interact(Collider other);

    public abstract void Die();

    public void GetDamage(int damage)
    {
        OnDamagedEvent?.Invoke(damage);
        
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
