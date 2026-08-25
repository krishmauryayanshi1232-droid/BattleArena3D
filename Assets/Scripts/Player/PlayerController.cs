using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 5f;
    
    private Rigidbody rb;
    private Transform cameraTransform;
    private bool isGrounded;
    private PhotonView photonView;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }
    
    void Update()
    {
        if (!photonView.IsMine) return;
        
        HandleMovement();
        HandleJump();
        HandleRotation();
    }
    
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        
        rb.drag = isGrounded ? groundDrag : 0;
    }
    
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;
        
        transform.Rotate(0, mouseX, 0);
        cameraTransform.Rotate(-mouseY, 0, 0);
    }
    
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    
    void OnCollisionExit()
    {
        isGrounded = false;
    }
}
