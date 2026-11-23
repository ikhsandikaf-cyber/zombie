using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Harus sama dengan Threshold di Blend Tree Move dan Idle
    private const float BlendDown = 0f;
    private const float BlendLeft = 1f / 3f;
    private const float BlendRight = 2f / 3f;
    private const float BlendUp = 1f;

    public float kecepatan = 5f;
    public int skor = 0;

    private Vector2 arahGerak;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;

        UpdateAnimasi();
    }

    void UpdateAnimasi()
    {
        if (anim == null) return;

        bool berjalan = arahGerak.sqrMagnitude > 0.01f;
        anim.SetBool("IsWalking", berjalan);

        // Saat berhenti, Blend dibiarkan supaya idle menghadap arah terakhir
        if (!berjalan) return;

        float blend;
        if (Mathf.Abs(arahGerak.x) > Mathf.Abs(arahGerak.y))
            blend = arahGerak.x > 0 ? BlendRight : BlendLeft;
        else
            blend = arahGerak.y > 0 ? BlendUp : BlendDown;

        anim.SetFloat("Blend", blend);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            skor++;
            Debug.Log("Skor Kamu: " + skor);

            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null) gm.AmbilKoin();
        }
    }
}