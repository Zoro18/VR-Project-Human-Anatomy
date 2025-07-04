using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRObjectPanelActivator : MonoBehaviour
{
    [SerializeField] private GameObject panel; // Assign the panel in the inspector

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("No XRGrabInteractable component found on this GameObject.");
        }

        if (panel != null)
        {
            panel.SetActive(false); // Ensure the panel is inactive initially
        }
        else
        {
            Debug.LogError("No panel assigned to this script.");
        }

        // Add event listeners
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        // Remove event listeners
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    // This method is called when the object is grabbed
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (panel != null)
        {
            panel.SetActive(true); // Activate the panel when the object is grabbed
        }
    }

    // This method is called when the object is released
    private void OnRelease(SelectExitEventArgs args)
    {
        if (panel != null)
        {
            panel.SetActive(false); // Deactivate the panel when the object is released
        }
    }
}