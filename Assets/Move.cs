using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 direction;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // ������� �������� ����
    public float leftBound = -5f;
    public float rightBound = 5f;
    public float bottomBound = -5f;
    public float topBound = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Random.insideUnitCircle.normalized;
        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    void Update()
    {
        // ������������ ������� ������� � �������� �������� ����
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        rb.linearVelocity = direction * speed;

        // ������� ���������� ������ �� �����
        transform.position += (Vector3)collision.contacts[0].normal * 0.1f;

        ApplyRandomChange();
    }

    void ApplyRandomChange()
    {
        int changeType = Random.Range(0, 4);
        switch (changeType)
        {
            case 0:
            
                spriteRenderer.color = Random.ColorHSV();
                break;

            case 1:
                
                float randomSize = Random.Range(0.2f, 1.5f);
                transform.localScale = new Vector3(randomSize, randomSize, 1f);
                break;

            case 2:
                float randomRotation = Random.Range(0f, 360f);
                transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
                break;

            case 3:
                float randomX = Random.Range(0.5f, 2f);
                float randomY = Random.Range(0.5f, 2f);
                transform.localScale = new Vector3(randomX, randomY, 1f);
                break;
        }

        UpdateCollider();
    }

    void UpdateCollider()
    {
        if (GetComponent<Collider2D>() is CircleCollider2D circleCollider)
        {
            circleCollider.radius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2f;
        }
        else if (GetComponent<Collider2D>() is BoxCollider2D boxCollider)
        {
            boxCollider.size = transform.localScale;
        }
    }
}