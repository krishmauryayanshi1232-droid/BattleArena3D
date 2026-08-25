using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    [SerializeField] private string gunName = "Rifle";
    [SerializeField] private float damage = 25f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private Transform muzzle;
    
    private int ammoInMag;
    private float nextFireTime = 0f;
    private bool isReloading = false;
    private PhotonView photonView;
    
    void Start()
    {
        ammoInMag = magazineSize;
        photonView = GetComponent<PhotonView>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && !isReloading)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    
    void Fire()
    {
        if (ammoInMag <= 0)
        {
            Reload();
            return;
        }
        
        ammoInMag--;
        
        // Raycast for hit detection
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, 1000f))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                photonView.RPC("DamageEnemy", RpcTarget.All, hit.collider.gameObject.name, damage);
            }
        }
        
        // Spawn muzzle flash
        SpawnMuzzleFlash();
    }
    
    void Reload()
    {
        if (isReloading) return;
        StartCoroutine(ReloadCoroutine());
    }
    
    System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammoInMag = magazineSize;
        isReloading = false;
    }
    
    void SpawnMuzzleFlash()
    {
        // Instantiate muzzle flash effect at muzzle position
    }
    
    [PunRPC]
    void DamageEnemy(string enemyName, float damageAmount)
    {
        Debug.Log($"Enemy {enemyName} took {damageAmount} damage");
    }
}
