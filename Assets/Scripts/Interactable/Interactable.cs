using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public Transform playerTransform;
    public InteractableText interactableText;
    public float requiredDistance;
    public int interactableCounter;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Outline>().enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        interactableText = GetComponent<InteractableText>();
        interactableCounter = 0;
    }

    private void OnMouseOver()
    {
        if ((transform.position - playerTransform.position).magnitude < requiredDistance)
        {
            GetComponent<Outline>().enabled = true;
        }
        else
        {
            GetComponent<Outline>().enabled = false;
            GetComponent<Outline>().OutlineColor = Color.red;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
        GetComponent<Outline>().OutlineColor = Color.red;
    }

    private void OnMouseDown()
    {
        if ((transform.position - playerTransform.position).magnitude < requiredDistance)
        {


            Debug.Log("Clicked Interactable");
            playerInteraction.AddInteractionCount(1);
            GetComponent<Outline>().OutlineColor = Color.blue;
            if (playerInteraction.interactionCount <= playerInteraction.maxInteractions)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
