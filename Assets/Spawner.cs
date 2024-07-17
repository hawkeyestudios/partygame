using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    
    void Start()
    {
        
        Vector3 spawnPosition = new Vector3(15, 4, Random.Range(-15, 5));
        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    }

    
    void Update()
    {

    }
}
