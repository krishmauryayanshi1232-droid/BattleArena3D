using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int maxPlayers = 100;
    
    private static GameManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToPhoton()
    {
        Debug.Log("Connected to Photon");
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        SpawnPlayer();
    }
    
    void SpawnPlayer()
    {
        MapManager mapManager = FindObjectOfType<MapManager>();
        Vector3 spawnPos = mapManager != null ? mapManager.GetRandomSpawnPoint() : Vector3.zero;
        
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
