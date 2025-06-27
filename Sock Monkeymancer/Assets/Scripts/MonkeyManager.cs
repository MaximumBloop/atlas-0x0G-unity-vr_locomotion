using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

using System.Collections.Generic;

public class MonkeyManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction primaryButtonAction;
    private InputAction secondaryButtonAction;
    [SerializeField] private float analogActivationThreshold = 0.1f;

    [Header("Sphere Raycaster")]
    [SerializeField] private GameObject sphereRaycasterGameObject;
    private IShoot sphereRaycaster;

    [Header("Raycaster")]
    [SerializeField] private GameObject raycasterGameObject;
    private IShoot raycaster;

    private List<GameObject> monkeys;

    private bool isSecondaryButtonHeld = false;

    void Start()
    {
        sphereRaycaster = sphereRaycasterGameObject.GetComponent<IShoot>();
        if (sphereRaycaster == null) Debug.LogError("No Sphere Raycaster initialized.");

        raycaster = raycasterGameObject.GetComponent<IShoot>();
        if (raycaster == null) Debug.LogError("No Raycaster initialized.");

        monkeys = new List<GameObject>();
        if (monkeys == null) Debug.LogError($"List ({monkeys}) initialization failed.");
    }

    void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("XRI Left Interaction");
        primaryButtonAction = actionMap.FindAction("SphereCast");
        secondaryButtonAction = actionMap.FindAction("RayCast");

        primaryButtonAction.Enable();
        secondaryButtonAction.Enable();
    }

    void OnDisable()
    {
        primaryButtonAction.Disable();
        secondaryButtonAction.Disable();
    }

    void Update()
    {
        float primaryAnalogPressValue = primaryButtonAction.ReadValue<float>();
        float secondaryAnalogPressValue = secondaryButtonAction.ReadValue<float>();

        if (primaryAnalogPressValue > analogActivationThreshold)
        {
            GetMonkeys();
        }
        else
        {
            // if (selectionVisualizerInstance) Destroy(selectionVisualizerInstance);
            sphereRaycaster.RaycastCleanup();
        }

        if (secondaryAnalogPressValue > analogActivationThreshold && !isSecondaryButtonHeld)
        {
            isSecondaryButtonHeld = true;
            GetMonkeyDestination();
        }
        else
        {
            isSecondaryButtonHeld = false;
        }
    }

    void GetMonkeys()
    {

        if (monkeys.Count > 0)
        {
            foreach (GameObject monkey in monkeys)
            {
                ISelectable selected = monkey.GetComponent<ISelectable>();
                if (selected != null) selected.Deselect();
            }
        }

        sphereRaycaster.ForwardRaycast();
    }

    public void HandleSphereCast(List<GameObject> list)
    {
        monkeys.Clear();
        monkeys = list;

        foreach (GameObject monkey in monkeys)
        {
            ISelectable selected = monkey.GetComponent<ISelectable>();
            if (selected != null) selected.Select();
        }
    }

    void GetMonkeyDestination()
    {
        raycaster.ForwardRaycast();
    }

    public void MoveMonkeys(Vector3 destination)
    {
        foreach (GameObject monkey in monkeys)
        {
            monkey.GetComponent<Monkey>().MoveTo(destination);
        }
    }
}
