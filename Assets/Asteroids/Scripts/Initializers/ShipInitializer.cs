using UnityEngine;

public class ShipInitializer
{
    private ShipData _playerShip;
    private GameObject _shipPrefab;
    private ShipModel _shipModel;
    private ShipView _shipView;
    private ShipController _shipController;

    public ShipModel ShipModel => _shipModel;
    public ShipView ShipView => _shipView;
    public ShipController ShipController => _shipController;

    public ShipInitializer(ShipData shipData)
    {
        _playerShip = shipData;
        _shipPrefab = shipData.ShipPrefab;
    }

    public void InitShip()
    {
        GameObject shipGameObject = Object.Instantiate(_shipPrefab, _playerShip.StartPosition, Quaternion.identity);

        _shipModel = new ShipModel(_playerShip);
        _shipView = shipGameObject.GetComponent<ShipView>();
        _shipController = new ShipController(_shipModel, _shipView);
    }
}
