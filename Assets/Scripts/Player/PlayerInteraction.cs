using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //public Interactable interactable;
    public int interactionCount;
    public int maxInteractions = 5;
    public InteractableText _interactableText;


    // Start is called before the first frame update
    void Start()
    {
        interactionCount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    

    public void AddInteractionCount(int addCount)
    {
        
        if (interactionCount != maxInteractions)
        {
            interactionCount += addCount;
            _interactableText.ShowText();
            Debug.Log("El numero de interacciones actual es " + interactionCount);
        }
        else
        {
            interactionCount = maxInteractions;
            Debug.Log("El numero de interacciones lleg� al tope.");
        }

    }


}
