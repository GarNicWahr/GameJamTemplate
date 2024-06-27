using UnityEngine;

public class Item : MonoBehaviour
{

    public int itemValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            other.GetComponent<PlayerInventory>().ItemCollected(itemValue);
            Destroy(gameObject);
        }
    }
}
