using UnityEngine;

public class ModalController : MonoBehaviour
{
    public GameObject tramStationModal; // Reference to the TramStationModal GameObject

    public void ShowModal()
    {
        tramStationModal.SetActive(true); // Activate the modal
    }

    public void HideModal()
    {
        tramStationModal.SetActive(false); // Deactivate the modal
    }
}