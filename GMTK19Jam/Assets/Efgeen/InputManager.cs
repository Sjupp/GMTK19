using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField]
    private KeyCode moveUp = KeyCode.W;
    [SerializeField]
    private KeyCode moveLeft = KeyCode.A;
    [SerializeField]
    private KeyCode moveDown = KeyCode.S;
    [SerializeField]
    private KeyCode moveRight = KeyCode.D;

    [SerializeField]
    private KeyCode kickKey = KeyCode.Space;

    public delegate void MoveDelegate(Vector3 direction);
    public MoveDelegate moveDelegate;

    public delegate void KickDelegate();
    public KickDelegate kickDelegate;

    public void Upd8()
    {
        Move();
        Kick();
    }

    private void Move()
    {

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(moveUp))
        {
            direction.z++;
        }

        if (Input.GetKey(moveLeft))
        {
            direction.x--;
        }

        if (Input.GetKey(moveDown))
        {
            direction.z--;
        }

        if (Input.GetKey(moveRight))
        {
            direction.x++;
        }

        moveDelegate?.Invoke(direction);

    }
    private void Kick()
    {

        if (Input.GetKeyDown(kickKey))
        {
            kickDelegate?.Invoke();
        }

    }

}