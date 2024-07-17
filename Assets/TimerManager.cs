using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviourPunCallbacks
{
    public Text timerText;
    public Text flagTimeText;

    private float gameTime = 180f;

    private void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            EndGame();
        }
        UpdateTimerUI();
        UpdateFlagTimeUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void UpdateFlagTimeUI()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("FlagHoldTime", out object holdTime))
        {
            float flagHoldTime = (float)holdTime;
            flagTimeText.text = "Flag Hold Time: " + flagHoldTime.ToString("F2") + "s";
        }
    }

    private void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_EndGame", RpcTarget.All);
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
