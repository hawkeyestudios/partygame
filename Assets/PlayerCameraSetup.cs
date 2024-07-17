using Photon.Pun;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviourPunCallbacks
{
    public GameObject cameraPrefab; 

    private void Start()
    {
        if (photonView.IsMine)
        {
            
            GameObject cameraObject = PhotonNetwork.Instantiate(cameraPrefab.name, transform.position, Quaternion.identity);

            
            cameraObject.transform.parent = transform;
        }
    }
}
