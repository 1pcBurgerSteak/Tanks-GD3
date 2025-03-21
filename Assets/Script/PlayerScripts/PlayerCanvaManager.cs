using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvaManager : MonoBehaviour
{
    public GameObject playerManager; // Parent containing PlayerController objects
    public GameObject playerCanva; // Parent containing PlayerCanva UI objects

    private List<GameObject> playerCanvas = new List<GameObject>();
    private List<PlayerCanva> canvaScripts = new List<PlayerCanva>(); 
    private List<PlayerController> playerControllers = new List<PlayerController>();

    public GameObject readyButton;
    public bool isReady;

    void Start()
    {
        isReady = false;
        GetAllChildCanvas();
        GetPlayerControllers();
        AssignPlayersToCanvas();
    }

    void GetAllChildCanvas()
    {
        playerCanvas.Clear();
        canvaScripts.Clear();

        foreach (Transform child in playerCanva.transform)
        {
            playerCanvas.Add(child.gameObject);

            PlayerCanva script = child.GetComponent<PlayerCanva>();
            if (script != null)
            {
                canvaScripts.Add(script);
            }
            else
            {
                Debug.LogWarning($"PlayerCanva script not found on {child.name}");
            }

            child.gameObject.SetActive(false); // Initially hide canvases
        }
    }

    void GetPlayerControllers()
    {
        playerControllers.Clear();

        foreach (Transform child in playerManager.transform)
        {
            PlayerController playerController = child.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerControllers.Add(playerController);
            }
        }
    }

    void AssignPlayersToCanvas()
    {
        int count = Mathf.Min(playerControllers.Count, canvaScripts.Count);

        for (int i = 0; i < count; i++)
        {
            canvaScripts[i].AssignPlayer(playerControllers[i]);
            playerCanvas[i].SetActive(true); 
        }
    }
}
