using UnityEngine;

public class WarlockEnemy : EnemyBase
{
    private enum State { Idle, Telegraph, Recover }

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Color projectileColor = Color.red;
    [SerializeField] private LineRenderer telegraphLine;
    [SerializeField] private State state;   

    private Vector2 lockedTarget;
    private float stateStart;

    private float TimeInState => Time.time - stateStart;
    private void SetState(State s) { state = s; stateStart = Time.time; }

    protected override void Start()
    {
        base.Start();
        if (telegraphLine != null) telegraphLine.enabled = false;
    }

    private void Update()
    {
        if (player == null) return;

        switch (state)
        {
            case State.Idle:
                if (status.IsBlinded) break;  
                float dist = Vector2.Distance(transform.position, player.position);
                //finding player position and locking it in to shoot at the player
                if (dist <= data.attackRange)
                {
                    lockedTarget = player.position;
                    ShowTelegraph(true);
                    SetState(State.Telegraph);
                }
                break;

            case State.Telegraph:
                if (TimeInState >= data.telegraphTime)
                {
                    Shoot();
                    ShowTelegraph(false);
                    SetState(State.Recover);
                }
                break;
            //time between shots at the player
            case State.Recover:
                if (TimeInState >= data.attackCD) SetState(State.Idle);
                break;
        }
    }

    private void Shoot()
    {
        Vector2 origin = transform.position;
        Vector2 direction = status.IsBlinded ? Random.insideUnitCircle.normalized : lockedTarget - origin;//randomly fire a projectile if enemy is blinded

        Projectile p = Instantiate(projectilePrefab, origin, Quaternion.identity);

        SpriteRenderer sr = p.GetComponentInChildren<SpriteRenderer>();
        if (sr != null) sr.color = projectileColor;

        p.Configure(data.attackDmg, data.statusOnHit, data.statusDuration, data.statusStrength);
        p.fireProj(direction);
    }

    private void ShowTelegraph(bool show)
    {
        if (telegraphLine == null) return;
        telegraphLine.enabled = show;
        if (!show) return;

        Vector3 origin = transform.position;
        Vector3 dir = ((Vector3)lockedTarget - origin).normalized;
        telegraphLine.positionCount = 2;
        telegraphLine.SetPosition(0, origin);
        telegraphLine.SetPosition(1, origin + dir * data.attackRange);
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }
}