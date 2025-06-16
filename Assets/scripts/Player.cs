using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}
public class Player : MonoBehaviour, IDamageable
{
    public float speed = 5f;
    public int maxHealth = 100;
    protected int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    // Interfaz
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    // Sobrecarga: también acepta daño como porcentaje
    public void TakeDamage(float percent)
    {
        int damage = Mathf.RoundToInt(maxHealth * percent);
        TakeDamage(damage);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            TakeDamage(10);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(10);
        }
    }
}

