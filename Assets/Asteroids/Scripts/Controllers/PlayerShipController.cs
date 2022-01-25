using UnityEngine;
using Object = UnityEngine.Object;

public sealed class PlayerShipController : ShipController
{
    private PlayerShipModel _model;
    private PlayerShipView _view;
    private PlayerShipMovement _movementController;

    public PlayerShipController(ShipData data) : base(data)
    {
        _model = new PlayerShipModel(data);
    }

    public override void Init()
    {
        GameObject shipGameObject = Object.Instantiate(Data.ShipPrefab, Data.StartPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<PlayerShipView>();

        BulletStartPoint = _view.BulletSpawnPoint;

        _view.SetMaxHealth(_model.MaxHP);
        _view.SetMaxArmor(_model.MaxArmor);

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = new PlayerShipMovement();
            _movementController.Init(_view.Rigidbody, _model, collider);
        }

        OnEnable();
    }

    public override void OnEnable()
    {
        _view.OnDamagedEvent += _model.RecieveDamage;
        _model.OnHpChangedEvent += _view.SetHealth;
        _model.OnArmorChangedEvent += _view.SetArmor;
        //_view.OnScoreChangedEvent += _model.ChangeScore; // ?
        _model.OnDiedEvent += _view.Die;
        _model.OnDiedEvent += OnDisable;
    }

    public override void OnDisable()
    {
        _view.OnDamagedEvent -= _model.RecieveDamage;
        _model.OnHpChangedEvent -= _view.SetHealth;
        _model.OnArmorChangedEvent -= _view.SetArmor;
        //_view.OnScoreChangedEvent -= _model.ChangeScore; // ?
        _model.OnDiedEvent -= _view.Die;
        _model.OnDiedEvent -= OnDisable;
    }
    public override void Execute()
    {
        _movementController.Execute();
    }

    public override void FixedExecute()
    {
        _movementController.FixedExecute();
    }
}