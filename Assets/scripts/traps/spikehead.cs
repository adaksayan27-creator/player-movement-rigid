using UnityEngine;

public class spikehead : enemydamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkdelay;
    [SerializeField] private LayerMask playerLayer;

    private Vector3[] directions = new Vector3[4];
    private float checktTimer;
    private Vector3 destination;
    private bool attacking;
    [Header("SFX")]
    [SerializeField]private AudioClip Spikehead;

    private void OnEnable()
    {
        stop();
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * speed * Time.deltaTime);
        }
        else
        {
            checktTimer += Time.deltaTime;

            if (checktTimer >= checkdelay)
                checkForPlayer();
        }
    }

    private void checkForPlayer()
    {
        calculolatedirection();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i] * range, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                directions[i],
                range,
                playerLayer
            );

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checktTimer = 0;
                break;
            }
        }
    }

    private void calculolatedirection()
    {
        directions[0] = transform.right;
        directions[1] = -transform.right;
        directions[2] = transform.up;
        directions[3] = -transform.up;
    }

    private void stop()
    {
        attacking = false;
        destination = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(Spikehead);
        base.OnTriggerEnter2D(collision);
        stop();
    }
}