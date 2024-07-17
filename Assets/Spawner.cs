using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    
    void Start()
    {
        
        Vector3 spawnPosition = new Vector3(14, -3, Random.Range(-8,0));
        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    }

    
    void Update()
    {

    }
}
