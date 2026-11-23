using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public enum ArahIsi { Horizontal, Vertical }

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;                       // Image bar merah yang akan memendek
    [SerializeField] private ArahIsi arah = ArahIsi.Horizontal;     // Horizontal = berkurang dari kanan ke kiri

    void Start()
    {
        if (playerHealth == null)
            playerHealth = FindAnyObjectByType<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth tidak ditemukan di scene.", this);
            return;
        }

        if (fillImage != null)
        {
            fillImage.type = Image.Type.Filled;
           
                fillImage.fillMethod = Image.FillMethod.Vertical;
                fillImage.fillOrigin = (int)Image.OriginVertical.Bottom;
        }

        playerHealth.OnHealthChanged += Refresh;
        Refresh(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= Refresh;
    }

    void Refresh(int current, int max)
    {
        if (fillImage == null || max <= 0) return;

        fillImage.fillAmount = (float)current / max;
    }
}