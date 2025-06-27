using UnityEngine;

public class Raycaster : MonoBehaviour, IShoot
{
    [Header("Raycast Config")]
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;

    [Header("Events")]
    public Vector3Event onRaycastHit;
    public void ForwardRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask))
        {
            onRaycastHit?.Invoke(hit.point);
        }
    }

    public void RaycastCleanup()
    {
        
    }
}
