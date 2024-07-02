using UnityEngine;

public class Item : MonoBehaviour
{

    public int itemType = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            other.GetComponent<PlayerInventory>().ItemCollected(itemType);
            Destroy(gameObject);
        }
    }
}
