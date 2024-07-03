using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Slider StaminaSlider;

    public TextMeshProUGUI PushesCount;

    public TextMeshProUGUI ImmortalityCount;

    public int AvailablePushes;

    private int _availableImmortality = 0;

    private float _availableSpeedboost;

    private PlayerStats _playerStats;

    // Start is called before the first frame update
    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        AvailablePushes = 3;
        _availableImmortality = 0;
        UpdateHUD();
        _playerStats.SetValues(1, 100f);
        _availableSpeedboost = _playerStats.StatValues(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && AvailablePushes > 0)
        {
        
            AvailablePushes = AvailablePushes - 1;
            PushesCount.text = AvailablePushes.ToString();
        }
    }

    public void ItemCollected(int index)
    {

        if(index == 0 && AvailablePushes < 3)
        {
             AvailablePushes++;
        }
        if(index == 1 && _availableImmortality == 0)
        {
            _availableImmortality++;
        }
        if(index == 2 && _availableSpeedboost <= 100)
        {
         
            _availableSpeedboost = 100f;
            _playerStats.SetValues(1, 100f);

        }
        
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        PushesCount.text = AvailablePushes.ToString();

        ImmortalityCount.text = _availableImmortality.ToString();

    }

}
