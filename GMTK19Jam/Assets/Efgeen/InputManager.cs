using System;
using UnityEngine;
using XInputDotNetPure; // Required in C#

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

    public void Upd8(int player)
    {
        Move(player);
        Kick();
    }

    private void Move(int player)
    {
        Vector3 direction = Vector3.zero;

        GamePadState gamePadState = GamePad.GetState((PlayerIndex)player);
        if (gamePadState.IsConnected) {
            if (gamePadState.DPad.Up == ButtonState.Pressed) {
                Debug.Log("Player: " + player + " moving up");
                direction.z++;
            }

            if (gamePadState.DPad.Left == ButtonState.Pressed) {
                Debug.Log("Player: " + player + " moving left");
                direction.x--;
            }

            if (gamePadState.DPad.Down == ButtonState.Pressed) {
                Debug.Log("Player: " + player + " moving down");
                direction.z--;
            }

            if (gamePadState.DPad.Right == ButtonState.Pressed) {
                Debug.Log("Player: " + player + " moving right");
                direction.x++;
            }

            if(direction == Vector3.zero) {
                direction.x += gamePadState.ThumbSticks.Left.X;
                direction.z += gamePadState.ThumbSticks.Left.Y;
            }

            GameManager.INSTANCE.players[player].OnMove(direction);
        }
        else {
            throw new Exception("CONTROLLER NOT CONNECTED!!!!!!!!!!!!!!!!!");

            if (Input.GetKey(moveUp)) {
                direction.z++;
            }

            if (Input.GetKey(moveLeft)) {
                direction.x--;
            }

            if (Input.GetKey(moveDown)) {
                direction.z--;
            }

            if (Input.GetKey(moveRight)) {
                direction.x++;
            }
        }

        //moveDelegate?.Invoke(direction);
    }
    private void Kick()
    {

        if (Input.GetKeyDown(kickKey))
        {
            kickDelegate?.Invoke();
        }

    }

    
}