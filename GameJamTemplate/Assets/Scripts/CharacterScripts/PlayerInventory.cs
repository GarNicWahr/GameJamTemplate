using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    public int ItemValueToCollect = 10;

    public TextMeshProUGUI collectedItemsLabel;

    public int CollectedItemsValue { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CollectedItemsValue = 0;
        UpdateHUD();
    }

    public void ItemCollected(int itemValue)
    {
        CollectedItemsValue += itemValue;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        collectedItemsLabel.text = CollectedItemsValue + "/" + ItemValueToCollect;
    }
}
