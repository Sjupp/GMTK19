using System;
using UnityEngine;
using XInputDotNetPure; // Required in C#

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField]
    public float kickStrength = 90f;
    [SerializeField]
    public float kickKickStrength = 100f;
    [SerializeField]
    public KeyCode moveUp = KeyCode.W;
    [SerializeField]
    public KeyCode moveLeft = KeyCode.A;
    [SerializeField]
    public KeyCode moveDown = KeyCode.S;
    [SerializeField]
    public KeyCode moveRight = KeyCode.D;

    [SerializeField]
    public KeyCode kickKey = KeyCode.Space;

    [SerializeField]
    private PlayerConfig config = null;

    [SerializeField]
    private PlayerData data = null;
    public PlayerData Data
    {
        get { return data; }
    }

    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private Projectile projectile;

    public Team team;

    public GamePadState gamePad;

    #region resetvalues
    private Vector3 startPos;
    #endregion

    private void Start() {
        switch (team) {
            case Team.One:
                gamePad = GamePad.GetState(PlayerIndex.One);
                break;
            case Team.A:
                gamePad = GamePad.GetState(PlayerIndex.Two);
                break;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        #region setresetvalues
        startPos = transform.position;
        #endregion
    }

    private void Update()
    {

        Collider[] overlappingColliders = Physics.OverlapBox(transform.position, new Vector3(1.01f, 1.01f, 1.01f));

        for (int i = 0; i < overlappingColliders.Length; i++)
        {

            if (overlappingColliders[i].tag == "Ball")
            {

                Ball ball = overlappingColliders[i].GetComponent<Ball>();

                ball.Project(this, new Vector3((ball.transform.position.x - transform.position.x), 5f, (ball.transform.position.z - transform.position.z)).normalized, kickStrength);
                ball.Data.decrementAmount = 2.5f;
            }

        }

    }

    public void Upd8()
    {

        if ((data.state & PlayerState.Knockback) == PlayerState.Knockback)
        {

            data.knockbackTimer -= Time.deltaTime;

            if (data.knockbackTimer <= 0)
            {
                data.state &= ~PlayerState.Knockback;
            }

        }

        if (data.state == PlayerState.None)
        {
            Move();
        }

    }
    private void Move()
    {
        rigidbody.velocity = data.normalizedMovementDirection * data.movementSpeed;
    }

    public void OnMove(Vector3 direction)
    {
        data.normalizedMovementDirection = direction.normalized;
        Rotate(direction);
    }
    private void Rotate(Vector3 direction)
    {

        if (direction == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), data.rotationSpeed * Time.deltaTime);

    }

    public void OnKick()
    {

        if (Kick())
        {
            Debug.Log("ONKICK = KICK");
            return;
        }

        Debug.Log("ONKICK = SHOOT");
        Shoot();

    }
    private bool Kick()
    {

        if (data.ammo <= 0)
        {
            return false;
        }

        bool hit = false;

        Collider[] overlappingColliders = Physics.OverlapBox(transform.position, new Vector3(2, 2, 2));

        for (int i = 0; i < overlappingColliders.Length; i++)
        {

            if (overlappingColliders[i].tag == "Ball")
            {

                Ball ball = overlappingColliders[i].GetComponent<Ball>();

                ball.Project(this, transform.rotation * Vector3.forward, kickKickStrength);
                ball.Data.decrementAmount = 0.15f;

                hit = true;
                continue;
            }

            if (overlappingColliders[i].tag == "Player")
            {

                Player kicked = overlappingColliders[i].GetComponent<Player>();

                if (kicked == this)
                {
                    continue;
                }

                kicked.ApplyKnockback((kicked.transform.position - transform.position).normalized, data.knockbackPower);
                hit = true;

            }

        }

        if (hit)
        {
            data.ammo--;
        }

        return hit;

    }
    private void Shoot()
    {

        if (data.ammo <= 0)
        {
            return;
        }

        Projectile temp = Instantiate(projectile, transform.position, transform.rotation, null);
        temp.Project(this, transform.rotation * Vector3.forward, 50);

        data.ammo--;

        //HACK
        //temp.Project(this, (FindObjectOfType<Ball>().transform.position - transform.position), 50);
        //Destroy(temp.gameObject, 0.5f);
    }

    public void OnDash()
    {
        Debug.Log("[Player.Dash] : Dashed");
    }


    public void ApplyKnockback(Vector3 direction, float power)
    {
        data.state |= PlayerState.Knockback;
        data.knockbackTimer = data.knockbackDuration;
        rigidbody.velocity = direction * power;
    }

    public void Reset() {
        transform.position = startPos;
    }
}
