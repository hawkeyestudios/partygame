using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(GetComponent<PhotonView>().IsMine == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement = transform.TransformDirection(movement);
            movement *= speed * Time.deltaTime;

            rb.MovePosition(transform.position + movement);
        }
      
    }
}
