using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDView : MonoBehaviour
{
    public Slider healthSlider;
    public Slider armorSlider;
    public Text scoreValue;

    public void SetScreenActive(bool flag)
    {
        gameObject.SetActive(flag);
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxArmor(int armor)
    {
        armorSlider.maxValue = armor;
        armorSlider.value = armor;
    }
    public void SetArmor(int armor)
    {
        armorSlider.value = armor;
    }

    public int GetScore()
    {
        string stringScoreValue = scoreValue.text;
        int intScoreValue;
        bool tryGetValue = int.TryParse(stringScoreValue, out intScoreValue);

        return tryGetValue ? intScoreValue : -1;
    }

    public void SetScore(int? value)
    {
        if (value != null && value > 0)
        {
            var oldScoreValue = GetScore();
            scoreValue.text = (oldScoreValue + value).ToString();
        }
    }
}
