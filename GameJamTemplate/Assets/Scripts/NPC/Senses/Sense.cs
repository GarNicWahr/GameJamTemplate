using UnityEngine;

/// <summary>
/// Base class for all senses
/// </summary>
[ExecuteInEditMode]
public abstract class Sense : MonoBehaviour
{
    // Range of the sense (meter)
    public float Range;

    // Head in Mixamo-Rig
    public Transform HeadReferenceTransform;

    // Wird der Spieler gerade wahrgenommen
    public bool IsDetecting { get; protected set; }

    // Transform of player
    protected Transform _player;

    // Direction vector to the player
    protected Vector3 _directionToPlayer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        _directionToPlayer = _player.transform.position - HeadReferenceTransform.position;
    }

    // Player in range?
    public bool IsInRange()
    {
        return _directionToPlayer.sqrMagnitude <= (Range * Range);
    }

}
