using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI kilsText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Canvas mainCanvas;
    
    private PlayerController playerController;
    private Gun currentGun;
    private int kills = 0;
    
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        currentGun = FindObjectOfType<Gun>();
        UpdateUI();
    }
    
    void Update()
    {
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (playerController != null)
        {
            // Update health display
            float healthPercent = 100f; // Get from player health
            healthBar.fillAmount = healthPercent / 100f;
            healthText.text = $"Health: {healthPercent:F0}";
        }
        
        if (currentGun != null)
        {
            // Update ammo display
            ammoText.text = $"Ammo: 30/120"; // Get from gun
        }
        
        kilsText.text = $"Kills: {kills}";
    }
    
    public void AddKill()
    {
        kills++;
    }
}
