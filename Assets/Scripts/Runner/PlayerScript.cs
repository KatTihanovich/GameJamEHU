using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float JumpForce;
    [SerializeField]
    bool isGrounded = false;
    bool isAlive = true;
    Rigidbody2D rb;
    float time = 30;
    public TextMeshProUGUI timeTxt;
    public GameObject winScreen;
    public GameObject loseScreen;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded == true)
            {
                rb.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
            }
        }

        if (isAlive)
        {
            time += Time.deltaTime * -1;
            timeTxt.text = "Time: " + time;
        }

        if (time <= 0)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            HeatBar.DecreaseHeat(3f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            isAlive = false;
            Time.timeScale = 0f;
            loseScreen.SetActive(true);
            HeatBar.IncreaseHeat(3f);
        }
    }
}
