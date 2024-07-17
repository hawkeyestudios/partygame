using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{

    public InputField input_create;
    public InputField input_join;


    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(input_create.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_join.text);
    }

    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GamePlay");
    }
   
}
