using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerPrefab;

    [HideInInspector]
    public GameObject player;

    public List<GameObject> EntityList = new List<GameObject>();

    public GameStateHandler stateHandler;

    void Start()
    {
        stateHandler = GetComponent<GameStateHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CleanEntities()
    {
        if (player != null) Destroy(player);
        if (EntityList.Count <= 0) return;

        foreach (GameObject entity in EntityList)
        {
            Destroy(entity);
        }
        EntityList = new();
    }
    public void InitPlay()
    {
        if (player != null) return;
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
        player.GetComponent<PlayerController>()._gameManager = this;
    }

    public void QuitGame()
    {
        Destroy(player);
        player = null;
    }

    public void LoadMainMenu()
    {
        CleanEntities();
    }
}
