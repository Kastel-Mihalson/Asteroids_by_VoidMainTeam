using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDView : MonoBehaviour
{
    public Slider HealthSlider;
    public Slider ArmorSlider;

    private void Awake()
    {
        ArmorSlider.onValueChanged.AddListener(RemoveIfNullValue);
    }

    public void SetMaxHealth(int health)
    {
        HealthSlider.maxValue = health;
        HealthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        HealthSlider.value = health;
    }

    public void SetMaxArmor(int armor)
    {
        ArmorSlider.maxValue = armor;
        ArmorSlider.value = armor;
    }

    public void SetArmor(int armor)
    {
        ArmorSlider.value = armor;
    }

    private void RemoveIfNullValue(float value)
    {
        if (value <= 0.01f)
        {
            ArmorSlider.gameObject.SetActive(false);
            ArmorSlider.onValueChanged.RemoveAllListeners();
        }
    }
}
