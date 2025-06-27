using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.XR.Interaction.Toolkit;

using System.Collections.Generic;

public class SphereGeneralCaster : MonoBehaviour, IShoot
{
    [Header("Sphere Settings")]

    [SerializeField] private float visualizerRadius = 0.1f;
    [SerializeField] private GameObject selectionVisualizer;
    private GameObject selectionVisualizerInstance;
    [SerializeField] private float maxDistance = 10f;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask selectableLayerMask;
    [SerializeField] private LayerMask detectionLayerMask;

    [Header("Events")]
    public GameObjectListEvent onSphereCastHit;

    private List<GameObject> workers;

    void Start()
    {
        workers = new List<GameObject>();
    }
    public void ForwardRaycast()
    {
        FindAllMonkeys();
    }

    public void FindAllMonkeys()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, detectionLayerMask))
        {
            if (selectionVisualizerInstance)
            {
                selectionVisualizerInstance.transform.position = hit.point;
            }
            else
            {
                selectionVisualizerInstance = Instantiate(selectionVisualizer, hit.point, Quaternion.identity);
                selectionVisualizerInstance.transform.localScale *= visualizerRadius;
            }
            workers = FindWorkersInRadius(hit.point);
        }

        onSphereCastHit?.Invoke(workers);
    }

    public void RaycastCleanup()
    {
        if (selectionVisualizerInstance) Destroy(selectionVisualizerInstance);
    }

    private List<GameObject> FindWorkersInRadius(Vector3 position)
    {
        List<GameObject> workers = new List<GameObject>();
        Collider[] hits = Physics.OverlapSphere(position, visualizerRadius, selectableLayerMask);

        foreach (Collider hit in hits)
        {
            workers.Add(hit.gameObject);
        }

        return workers;
    }
}
