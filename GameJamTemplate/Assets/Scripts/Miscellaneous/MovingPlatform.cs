using UnityEngine;
using Cinemachine;

public class MovingPlatform : MonoBehaviour
{
    // Get nice curve in inspector to control animation
    public AnimationCurve AnimationCurve;

    // How long does it take to sample the curve
    public float Duration = 3f;

    // Play form the beginning
    public bool PlayOnStart = true;

    // Elapse time
    private float _progress;

    // Moves the platform via cinemachin
    private CinemachineDollyCart _chart;

    // Update the position of the platform?
    private bool _isMoving;

    // Start is called before the first frame update
    void Start()
    {
        _chart = GetComponent<CinemachineDollyCart>();
        _isMoving = PlayOnStart;
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            // Take the duration for sampling into account
            _progress += Time.deltaTime / Duration;

            // Normalize value for position (0 - 1)
            float position = AnimationCurve.Evaluate(_progress) % Duration;
            _chart.m_Position = position;
        }
    }

    // Enter trigger > make player child of this transform
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            _isMoving = true;
            other.transform.SetParent(transform);
        }        
    }

    // Exit trigger > unparent the player
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isMoving = false;
            other.transform.SetParent(null);
        }
        
    }
}

