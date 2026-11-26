using UnityEngine;

public class WarlockEnemy : MonoBehaviour
{
    private enum State { Idle, Telegraph, Recover }

    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float telegraphTime = 2f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Color projectileColor = Color.red;
    [SerializeField] private LineRenderer telegraphLine; //warning line for player to see projectile coming
    [SerializeField] private State state; // visible in the Inspector for testing


    private Transform player;
    private Vector2 lockedTarget; // locked position of the player
    private float stateStart;

    private float TimeInState => Time.time - stateStart;
    private void SetState(State s)
    {
        state = s;
        stateStart = Time.time;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;

        if (telegraphLine != null) telegraphLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        switch (state)
        {
            case State.Idle:
                float dist = Vector2.Distance(transform.position, player.position); //distance of warlock to player to find position
                if (dist <= attackRadius)
                {
                    lockedTarget = player.position;   // aim is locked here, so it can be dodged
                    ShowTelegraph(true);
                    SetState(State.Telegraph);
                }
                break;

            case State.Telegraph:
                if (TimeInState >= telegraphTime)
                {
                    Shoot();
                    ShowTelegraph(false);//deactivate the telegraphed line where being shot
                    SetState(State.Recover);
                }
                break;

            case State.Recover: //cooldown between shots
                if (TimeInState >= cooldown) SetState(State.Idle);
                break;
        }
    }

    private void Shoot()
    {
        Vector2 origin = transform.position;
        Vector2 direction = lockedTarget - origin;

        Projectile p = Instantiate(projectilePrefab, origin, Quaternion.identity);

        // tint the bullet red so the player can tell it is dangerous
        SpriteRenderer sr = p.GetComponentInChildren<SpriteRenderer>();
        if (sr != null) sr.color = projectileColor;

        p.fireProj(direction);
    }

    private void ShowTelegraph(bool show)
    {
        if (telegraphLine == null) return;
        telegraphLine.enabled = show;
        if (!show) return;

        // the line runs from the warlock along the shot direction, out to the attack radius
        Vector3 origin = transform.position;
        Vector3 dir = ((Vector3)lockedTarget - origin).normalized;
        telegraphLine.positionCount = 2;
        telegraphLine.SetPosition(0, origin);
        telegraphLine.SetPosition(1, origin + dir * attackRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


}
