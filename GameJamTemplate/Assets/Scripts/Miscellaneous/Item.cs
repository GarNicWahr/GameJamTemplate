using UnityEngine;

public class Item : MonoBehaviour
{

    public int itemValue = 1;
    public int itemType = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            other.GetComponent<PlayerInventory>().ItemCollected(itemValue,itemType);
            Destroy(gameObject);
        }
    }
}
