using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float floatSpeed = 2f;
    public float lifeTime = 1.5f;

    private Color textColor;

    private void Awake()
    {
        textColor = text.color;
    }

    public void Setup(float damage)
    {
        text.text = damage.ToString();
        text.color = textColor;
        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Fade out
        textColor.a -= Time.deltaTime;
        text.color = textColor;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
