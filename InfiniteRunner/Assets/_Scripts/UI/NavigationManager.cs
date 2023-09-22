using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    public GameObject PanelA;
    public GameObject PanelB;
    public Button ButtonB;

    public void SwitchToPanelB()
    {
        PanelA.SetActive(false);
        PanelB.SetActive(true);
        
        // Set the new button to be selected
        EventSystem.current.SetSelectedGameObject(ButtonB.gameObject);
    }
}