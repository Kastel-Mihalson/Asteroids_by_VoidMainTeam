using UnityEngine;
using Object = UnityEngine.Object;

public sealed class EnemyShipController : ShipController
{
    private EnemyShipModel _model;
    private EnemyShipView _view;
    private EnemyShipMovement _movementController;

    public EnemyShipController(ShipData data) : base(data)
    {
        _model = new EnemyShipModel(data);
    }

    public override void Init()
    {
        GameObject shipGameObject = Object.Instantiate(Data.ShipPrefab, Data.StartPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<EnemyShipView>();

        BulletStartPoint = _view.BulletSpawnPoint;

        _view.SetMaxHealth(_model.MaxHP);
        _view.SetMaxArmor(_model.MaxArmor);

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = new EnemyShipMovement();
            _movementController.Init(_view.Rigidbody, _model, collider);
        }

        OnEnable();
    }

    public override void OnEnable()
    {
        _view.OnDamagedEvent += _model.RecieveDamage;
        _model.OnHpChangedEvent += _view.SetHealth;
        _model.OnArmorChangedEvent += _view.SetArmor;
        _model.OnDiedEvent += _view.Die;
        _model.OnDiedEvent += OnDisable;
    }

    public override void OnDisable()
    {
        _view.OnDamagedEvent -= _model.RecieveDamage;
        _model.OnHpChangedEvent -= _view.SetHealth;
        _model.OnArmorChangedEvent -= _view.SetArmor;
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