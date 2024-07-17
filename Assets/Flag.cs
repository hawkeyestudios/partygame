using Photon.Pun;
using UnityEngine;

public class Flag : MonoBehaviourPunCallbacks
{
    private Transform currentHolder;
    private Vector3 offset = new Vector3(0, 5, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView playerPhotonView = other.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                if (currentHolder != null && currentHolder.GetComponent<PhotonView>().IsMine)
                {
                    currentHolder.GetComponent<Player>().StopHoldingFlag();
                }

                SetHolder(other.transform);
                other.GetComponent<Player>().StartHoldingFlag();
            }
        }
    }

    private void Update()
    {
        if (currentHolder != null)
        {
            transform.position = currentHolder.position + offset;
        }
    }

    private void SetHolder(Transform newHolder)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_SetHolder", RpcTarget.AllBuffered, newHolder.GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    private void RPC_SetHolder(int holderViewID)
    {
        PhotonView holderPhotonView = PhotonView.Find(holderViewID);
        if (holderPhotonView != null)
        {
            currentHolder = holderPhotonView.transform;
        }
    }
}
