using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TimerManager timerManager;

    private void Start()
    {
        if (timerManager == null)
        {
            Debug.LogError("TimerManager referansý eksik!");
        }
    }

    private void Update()
    {
        
    }

    private void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            timerManager.photonView.RPC("RPC_EndGame", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_EndGame()
    {
        Player[] players = FindObjectsOfType<Player>();
        Player winner = null;
        float maxHoldTime = 0;

        foreach (var player in players)
        {
            if (player.flagHoldTime > maxHoldTime)
            {
                maxHoldTime = player.flagHoldTime;
                winner = player;
            }
        }

        if (winner != null)
        {
            Debug.Log("Kazanan: " + winner.photonView.Owner.NickName + " - Flag Tutma Süresi: " + maxHoldTime);
        }
    }
}
