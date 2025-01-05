using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageDisplayScript : MonoBehaviour
{
    private int colorCount;
    [SerializeField] private GameObject specialOpsCanvas;

    public void DisplaySelection()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name.Substring(0, EventSystem.current.currentSelectedGameObject.name.IndexOf(" Button"));
        string selectionName = buttonName + " Selection";
        foreach(Transform child in GameObject.Find("Selection Lists").transform)
        {
            if(child.name.Equals(selectionName) == false)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
        specialOpsCanvas.SetActive(false);
    }
}
