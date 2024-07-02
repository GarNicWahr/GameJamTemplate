using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    public int ItemValueToCollect = 10;

    public TextMeshProUGUI collectedItemsLabel;

    public int CollectedItemsValue { get; private set; }

    private int _availablePushes;

    private int _availableImmortality;

    private int _availableSpeedboost;

    // Start is called before the first frame update
    void Start()
    {
        CollectedItemsValue = 0;
        UpdateHUD();
    }

    public void ItemCollected(int itemValue,int index)
    {

        if(index == 0)
        {
            print("Push away");
            _availablePushes++;
        }
        if(index == 1)
        {
            print("Immortal");

        }
        if(index == 2)
        {
            print("Speed");
        }
        CollectedItemsValue += itemValue;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        collectedItemsLabel.text = CollectedItemsValue + "/" + ItemValueToCollect;
    }

}
