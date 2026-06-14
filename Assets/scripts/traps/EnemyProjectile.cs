using UnityEngine;

public class EnemyProjectile : enemydamage
{
    [SerializeField] private float speed;
    [SerializeField] private float restTime;

    private float lifetime;

    public void ActiveProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movement = speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);

        lifetime += Time.deltaTime;

        if (lifetime >= restTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}