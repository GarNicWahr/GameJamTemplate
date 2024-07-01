using UnityEngine;

/// <summary>
/// Implementation of virtual eyes
/// </summary>
public class Eyes : Sense
{
    // Field of view
    public float Fov;

    // Which layer are relevant
    public LayerMask DetectionLayer;

    // Update is called once per frame
    protected override void Update()
    {
        if(IsInRange() && IsInFieldOfView() && IsNotOccluded()) 
        {
            IsDetecting = true;
        }
        else 
        {
            IsDetecting = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        base.Update();
        SenseGizmos.DrawRangeCircle(HeadReferenceTransform.position, transform.up, Range);

        if (IsInRange()) 
        {
            SenseGizmos.DrawFOV(HeadReferenceTransform.position, HeadReferenceTransform.forward, Vector3.up, Range, Fov);

            if (IsInFieldOfView()) 
            {
                SenseGizmos.DrawRay(HeadReferenceTransform.position, _player.position, IsNotOccluded());
            }
        }
    }
#endif

    // Player inside fov?
    public bool IsInFieldOfView()
    {
        Vector3 direction = _directionToPlayer;
        direction.y = 0;

        Vector3 forward = HeadReferenceTransform.forward;
        forward.y = 0;
        float angleBetween = Vector3.Angle(forward, direction);
        return angleBetween < Fov * 0.5f;
    }

    // Player not occluded by anything?
    public bool IsNotOccluded() 
    {
        RaycastHit hit;
        Ray ray = new Ray(HeadReferenceTransform.position, _directionToPlayer);

        if(Physics.Raycast(ray, out hit, Range, DetectionLayer)) 
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        else
        {
            return false;
        }
    }

}
