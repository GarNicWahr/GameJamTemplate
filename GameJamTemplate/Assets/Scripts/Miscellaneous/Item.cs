using UnityEngine;

public class Item : MonoBehaviour
{

    public int itemType = 0;

    public GameObject ParticleSplash;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            other.GetComponent<PlayerInventory>().ItemCollected(itemType);
            Instantiate(ParticleSplash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
