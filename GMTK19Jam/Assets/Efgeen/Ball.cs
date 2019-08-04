using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region resetvalues
    private Vector3 startPos;
    #endregion

    private Vector3 p1goal = new Vector3(-20, 1, 0);
    private Vector3 p2goal = new Vector3(20, 1, 0);

    [SerializeField]
    private BallData data = null;
    public BallData Data
    {
        get { return data; }
    }

    private void Awake()
    {
        data.rigidbody = GetComponent<Rigidbody>();

        #region setresetvalues
        startPos = transform.position;
        #endregion
    }

    public void Project(Player source, Vector3 direction, float speed)
    {
        data.state = BallState.Aired;
        data.source = source;
        data.direction = direction;
        data.speed = speed;
        //MaybeApplyCurve();
    }

    private void MaybeApplyCurve()
    {
        GetCurvePoints();
        var number = CalculateCurveStrength();
        Debug.Log("number: " + number);
    }

    private void GetCurvePoints()
    {
        Vector3 p0 = data.rigidbody.position;
        Vector3 p1 = data.direction * (data.speed / 10.0f);
        Vector3 p2 = GetOpponentGoal();
    }

    private Vector3 GetOpponentGoal()
    {
        Vector3 p2;
        if (data.source.team == Team.One)
        {
            p2 = p2goal;
        }
        else
        {
            p2 = p1goal;
        }

        return p2;
    }

    private float CalculateCurveStrength()
    {
        float f;
        var distanceFromGoal = (GetOpponentGoal() - data.rigidbody.position).sqrMagnitude;
        var angleOffGoal = Vector3.Angle(data.rigidbody.position, GetOpponentGoal());
        Debug.Log(distanceFromGoal);
        Debug.Log(angleOffGoal);
        f = (distanceFromGoal + angleOffGoal) / 100;
        return f;
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;

        Vector3 v = Mathf.Pow(u, 2) * p0;
        v += 2 * u * t * p1;
        v += Mathf.Pow(t, 2) * p2;

        return v;
    }

    private void Update()
    {

        Rotate();
        //GroundCheck();
        //Gravity();
        data.rigidbody.velocity = new Vector3(data.direction.x * data.speed, data.verticalVelocity, data.direction.z * data.speed);
        if (data.speed > 20.0f || data.speed < 10.0f)
        {
            data.speed -= data.decrementAmount;
        }
        else
        {
            data.speed *= 0.99f; 
        }
        if (data.speed < 0)
        {
            data.speed = 0;
        }

    }

    private void Rotate()
    {
        data.rotationDirection = new Vector3(UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f));
        transform.rotation = Quaternion.Euler(data.rotationDirection * data.speed * 5);
    }

    private void GroundCheck()
    {

        if (Physics.SphereCast(data.rigidbody.position, 0.5f, Vector3.down, out RaycastHit hitInfo, 0.08f))
        {
            data.state = BallState.Grounded;
        }

    }
    private void Gravity()
    {

        if (data.state == BallState.Aired)
        {
            data.verticalVelocity -= data.gravity;
        }

        else
        {
            data.verticalVelocity = 0f;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.CompareTag("Horizontal Wall"))
        {
            data.direction.z *= -1;
            data.speed *= 0.5f;
        }

        else if (collision.collider.CompareTag("Vertical Wall"))
        {
            data.direction.x *= -1;
            data.speed *= 0.5f;
        }

    }

    public void Reset() {
        transform.position = startPos;
        if(data.rigidbody != null)
            data.rigidbody.velocity = Vector3.zero;
    }
}