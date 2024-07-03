using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEnemies : MonoBehaviour
{
    public float pushRadius = 5f;  
    public float pushDistance = 3f;

    private PlayerInventory _inventory;
    private void Start()
    {
        _inventory = GetComponent<PlayerInventory>();
    }
    void Update()
    {
        // When Player has Pushes, then push Enemies
        if (Input.GetKeyDown(KeyCode.Alpha1) && !_inventory.AvailablePushes = 0)  
        {
            PushNearbyEnemies();
        }
    }

    void PushNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))  // Prüfen, ob der getroffene Collider ein Gegner ist
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                direction.y = 0;  // Optionale Anpassung: Keine vertikale Verschiebung
                direction.Normalize();
                hitCollider.transform.position += direction * pushDistance;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Zeichnet den Radius im Editor, um den Bereich der Abstoßung zu visualisieren
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
}
