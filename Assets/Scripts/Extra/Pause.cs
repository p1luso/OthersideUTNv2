using UnityEngine;
using UnityEngine.AI;

public class Pause : MonoBehaviour
{
    private GameObject[] _enemies;
    public Movement playerMovement;
    void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    public void InteractableTextShowing()
    {

        Debug.Log("Esta entrando");
        
        foreach (GameObject go in _enemies)
        {
            go.SetActive(false);
        }
        
    }
    public void InteractableTextNotShowing()
    {
        Debug.Log("No esta entrando");

        playerMovement.enabled = true;

        foreach (GameObject go in _enemies)
        {
            go.SetActive(true);
        }

        
    }
}
