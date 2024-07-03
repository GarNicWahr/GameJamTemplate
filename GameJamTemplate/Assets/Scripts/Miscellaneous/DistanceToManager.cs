using UnityEngine;
using UnityEngine.UI;

public class DistanceToManager : MonoBehaviour
{
    public Transform Player;
    public Transform Manager;
    public Slider distanceSlider;

    public float maxDistance = 100f;

    void Start()
    {
        // Initializing min- and maxValue
        distanceSlider.minValue = 0f;
        distanceSlider.maxValue = Vector3.Distance(Player.position, Manager.position);
    }

    void Update()
    {
        // Vector for Distance between Player and Manager-NPC
        float distance = Vector3.Distance(Player.position, Manager.position);

        // Setting Distance to Slider
        distanceSlider.value = Mathf.Clamp(distance, 0f, maxDistance);
    }
}
