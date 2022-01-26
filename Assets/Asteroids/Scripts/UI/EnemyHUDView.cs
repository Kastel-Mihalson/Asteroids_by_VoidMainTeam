public sealed class EnemyHUDView : HUDView
{ 
    private void Awake()
    {
        ArmorSlider.onValueChanged.AddListener(RemoveIfNullValue);
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
