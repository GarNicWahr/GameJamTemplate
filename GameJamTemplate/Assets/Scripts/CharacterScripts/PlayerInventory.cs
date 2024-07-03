using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Slider StaminaSlider;

    public TextMeshProUGUI PushesCount;

    public TextMeshProUGUI ImmortalityCount;

    private int _availablePushes = 0;

    private int _availableImmortality = 0;

    private float _availableSpeedboost = 0;

    // Start is called before the first frame update
    void Start()
    {
        _availablePushes = 3;
        
        UpdateHUD();
    }

    public void ItemCollected(int index)
    {

        if(index == 0 && _availablePushes < 3)
        {
             _availablePushes++;
        }
        if(index == 1 && _availableImmortality == 0)
        {
            _availableImmortality++;
        }
        if(index == 2 && _availableSpeedboost <= 1)
        {
            _availableSpeedboost = 1f;
        }
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        PushesCount.text = _availablePushes.ToString();

        ImmortalityCount.text = _availableImmortality.ToString();

        StaminaSlider.value = StaminaSlider.maxValue;
    }

}
