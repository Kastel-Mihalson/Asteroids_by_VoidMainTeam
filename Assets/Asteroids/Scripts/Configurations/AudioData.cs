using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip _playerShot;
    [SerializeField] private AudioClip _enemyShot;
    [SerializeField] private AudioClip _shipExplosion;
    [SerializeField] private AudioClip _asteroidExplosion;
    [SerializeField] private AudioClip _shipHitting;
    [SerializeField] private AudioClip _asteroidHitting;
    [SerializeField] private AudioClip _gameOverMusic;
    [SerializeField] private AudioClip _newGameMusic;
    [SerializeField] private AudioClip _backgroundMusic;

    public AudioClip PlayerShot => _playerShot;
    public AudioClip EnemyShot => _enemyShot;
    public AudioClip ShipExplosion => _shipExplosion;
    public AudioClip AsteroidExplosion => _asteroidExplosion;
    public AudioClip ShipHitting => _shipHitting;
    public AudioClip AsteroidHitting => _asteroidHitting;
    public AudioClip GameOverMusic => _gameOverMusic;
    public AudioClip NewGameMusic => _newGameMusic;
    public AudioClip BackgroundMusic => _backgroundMusic;
}
