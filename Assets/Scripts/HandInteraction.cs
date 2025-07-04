using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInteraction : MonoBehaviour
{
    public GameObject panelToShow; // The panel to set active when interacting with an object
    public GameObject objectToInteractWith; // The object to interact with

    private XRGrabInteractable interactable;

    private void Start()
    {
        // Get the XRGrabInteractable component of the object to interact with
        interactable = objectToInteractWith.GetComponent<XRGrabInteractable>();

        // Ensure that the panel is initially inactive
        panelToShow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the object to interact with
        if (other.gameObject == objectToInteractWith)
        {
            // Subscribe to the OnSelectEntered event of the interactable
            interactable.onSelectEntered.AddListener(ShowPanel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the object to interact with
        if (other.gameObject == objectToInteractWith)
        {
            // Unsubscribe from the OnSelectEntered event of the interactable
            interactable.onSelectEntered.RemoveListener(ShowPanel);
            // Ensure that the panel is inactive when leaving the interaction area
            panelToShow.SetActive(false);
        }
    }

    private void ShowPanel(XRBaseInteractor interactor)
    {
        // Set the panel to active when interacting with the object
        panelToShow.SetActive(true);
    }
}

