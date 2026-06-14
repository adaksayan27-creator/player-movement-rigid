using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftedge;
    [SerializeField] private Transform rightedge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;

    private Vector3 initScale;
    private bool movingLeft;
    private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
        anim = enemy.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        if (anim != null)
            anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x > leftedge.position.x)
                MoveInDirection(-1);
            else
                movingLeft = false;
        }
        else
        {
            if (enemy.position.x < rightedge.position.x)
                MoveInDirection(1);
            else
                movingLeft = true;
        }
    }

    private void MoveInDirection(int _direction)
    {
        if (anim != null)
            anim.SetBool("moving", true);

        enemy.localScale = new Vector3(
            Mathf.Abs(initScale.x) * _direction,
            initScale.y,
            initScale.z);

        enemy.position += Vector3.right * _direction * speed * Time.deltaTime;
    }
}