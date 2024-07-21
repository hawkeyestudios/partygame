using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public float flagHoldTime = 0f;
    public float jumpForce = 10f;
    private bool isHoldingFlag = false;
    private bool isGrounded = false;
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator bileþeni bulunamadý!");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * 5f;

        if (movement != Vector3.zero)
        {
            rb.MovePosition(rb.position + movement * Time.deltaTime);
            transform.forward = movement;
        }

        bool isWalking = movement.magnitude > 0;
        //animator.SetBool("isWalking", isWalking);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (isHoldingFlag)
        {
            flagHoldTime += Time.deltaTime;

            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add("FlagHoldTime", flagHoldTime);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //animator.SetTrigger("Jump"); 
        isGrounded = false;  
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void StartHoldingFlag()
    {
        isHoldingFlag = true;
    }

    public void StopHoldingFlag()
    {
        isHoldingFlag = false;
    }
}
