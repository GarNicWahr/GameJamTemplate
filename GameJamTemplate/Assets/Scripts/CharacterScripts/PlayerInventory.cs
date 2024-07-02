using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    public TextMeshProUGUI pushesCount;

    public TextMeshProUGUI immortalityCount;

    public GameObject staminaBar;

    private int _availablePushes = 0;

    private int _availableImmortality = 0;

    private int _availableSpeedboost = 0;

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
        if(index == 2)
        {
            _availableSpeedboost++;
        }
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        pushesCount.text = _availablePushes.ToString();


    }

}
