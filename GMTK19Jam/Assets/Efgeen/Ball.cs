using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region resetvalues
    private Vector3 startPos;
    #endregion

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
    }

    private void Update()
    {

        Rotate();
        //GroundCheck();
        //Gravity();
        data.rigidbody.velocity = new Vector3(data.direction.x * data.speed, data.verticalVelocity, data.direction.z * data.speed);
        data.speed -= data.decrementAmount;
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
    }
}