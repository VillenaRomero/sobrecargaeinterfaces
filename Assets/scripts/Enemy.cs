using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float speed = 2f;
    public int maxHealth = 50;
    protected int currentHealth;
    protected Transform target;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("player")?.transform;
    }

    protected virtual void Update()
    {
        if (target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    // Interfaz
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    // Sobrecarga: acepta porcentaje
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
        if (collision.gameObject.tag == "player")
        {
            TakeDamage(10);
            collision.gameObject.GetComponent<Player>().TakeDamage(10);
        }
    }
}
