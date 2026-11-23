using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;

    [Header("UI (opsional)")]
    [SerializeField] private TMP_Text teksKoin;
    [SerializeField] private GameObject panelMenang;

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Total koin di Scene: " + totalKoin);

        if (panelMenang != null) panelMenang.SetActive(false);
        UpdateUI();
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;
        UpdateUI();

        if (totalKoin > 0 && koinTerkumpul >= totalKoin)
        {
            Menang();
        }
    }

    void Menang()
    {
        Debug.Log("KAMU MENANG!");
        if (panelMenang != null) panelMenang.SetActive(true);
    }

    void UpdateUI()
    {
        if (teksKoin != null)
            teksKoin.text = "Koin: " + koinTerkumpul + "/" + totalKoin;
    }
}