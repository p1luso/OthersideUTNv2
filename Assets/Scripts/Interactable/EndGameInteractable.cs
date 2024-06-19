using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameInteractable : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public Transform playerTransform;
    public float requiredDistance;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Outline>().enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();


    }

    private void OnMouseOver()
    {
        if (playerInteraction.interactionCount >= playerInteraction.maxInteractions)
        {


            if ((transform.position - playerTransform.position).magnitude < requiredDistance)
            {
                GetComponent<Outline>().enabled = true;
            }
            else
            {
                GetComponent<Outline>().enabled = false;
                
            }


        }

        


    }

    private void OnMouseExit()
    {
        if (playerInteraction.interactionCount >= playerInteraction.maxInteractions)
        {
            GetComponent<Outline>().enabled = false;
        }

    }

    private void OnMouseDown()
    {
        GetComponent<Outline>().enabled = false;
        if (playerInteraction.interactionCount >= playerInteraction.maxInteractions)
        {
            if ((transform.position - playerTransform.position).magnitude < requiredDistance)
            {
                SceneManager.LoadScene("EndGamev2");

            }
        }
        
    }
}
