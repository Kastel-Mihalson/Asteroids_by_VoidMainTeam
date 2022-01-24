using UnityEngine;
using UnityEngine.UI;

public abstract class ArmorBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxArmor(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetArmor(int health)
    {
        slider.value = health;
    }
}
