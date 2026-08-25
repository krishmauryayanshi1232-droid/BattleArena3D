using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRange = 20f;
    
    private float currentHealth;
    private PhotonView photonView;
    private Transform playerTarget;
    
    void Start()
    {
        currentHealth = maxHealth;
        photonView = GetComponent<PhotonView>();
    }
    
    void Update()
    {
        if (!photonView.IsMine) return;
        
        DetectPlayer();
        if (playerTarget != null)
        {
            ChasePlayer();
        }
    }
    
    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                playerTarget = hit.transform;
                break;
            }
        }
    }
    
    void ChasePlayer()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        photonView.RPC("DestroyEnemy", RpcTarget.All);
    }
    
    [PunRPC]
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
