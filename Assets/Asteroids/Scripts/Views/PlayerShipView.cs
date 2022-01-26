using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerShipView : ShipView
{
    //public event Action<int> OnScoreChangedEvent;

    private PlayerHUDView _hudView;
    private EndGameMenuView _loseMenu;

    public override void Interact(Collider other)
    {
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            int? damage = null;

            if (interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                damage = bulletView.GetBulletDamage();

                AudioController.Play(AudioClipManager.ShipHitting);
                EffectController.Create(EffectManager.ShipHitting, gameObject.transform);
            }
            else if (interactiveObject is IAsteroid)
            {
                var asteroidView = (AsteroidView)interactiveObject;
                damage = asteroidView.GetAsteroidDamage();
            }
            else if (interactiveObject is IShip)
            {
                Die();
            }

            if (damage != null)
            {
                GetDamage((int)damage);
            }
        }
    }

    public override void Die()
    {
        if (_loseMenu)
        {
            _loseMenu.SetGameEndParams(false);
            _loseMenu.SetScreenActive(true);
        }

        AudioController.Play(AudioClipManager.ShipExplosion);
        EffectController.Create(EffectManager.ShipExplosion, gameObject.transform);
        Destroy(gameObject);
    }

    // TODO remove awake
    private void Awake()
    {
        _hudView = FindObjectOfType<PlayerHUDView>();
        _loseMenu = FindObjectOfType<EndGameMenuView>();
    }

    private void Start()
    {
        if (_loseMenu)
        {
            _loseMenu.gameObject.SetActive(false);
        }
    }

    public void SetMaxHealth(int health)
    {
        _hudView.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        _hudView.SetHealth(health);
    }

    public void SetMaxArmor(int armor)
    {
        _hudView.SetMaxArmor(armor);
    }

    public void SetArmor(int armor)
    {
        _hudView.SetArmor(armor);
    }

    public void SetScore(int? value)
    {
        _hudView.SetScore(value);
    }
}
