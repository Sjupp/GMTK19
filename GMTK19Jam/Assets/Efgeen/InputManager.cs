using System;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class InputManager : MonoBehaviour
{
    public delegate void MoveDelegate(Vector3 direction);
    public MoveDelegate moveDelegate;

    public delegate void KickDelegate();
    public KickDelegate kickDelegate;

    GamePadState[] gamePadState = new GamePadState[2];
    ButtonState[] prevState = new ButtonState[2];
    ButtonState[] kickPrevState = new ButtonState[2];

    public void Upd8(int player) 
    {
        gamePadState[player] = GamePad.GetState((PlayerIndex)player);

        Move(player);
        Kick(player);

        if (gamePadState[player].IsConnected) {
            if (prevState[player] == ButtonState.Pressed && gamePadState[player].Buttons.Start == ButtonState.Released) {
                if (InterfaceManager.Instance.InterfaceState == InterfaceState.InGame) {
                    InterfaceManager.Instance.UpdateInterfaceState(InterfaceState.InGameMenu);
                }
                else if (InterfaceManager.Instance.InterfaceState == InterfaceState.InGameMenu) {
                    InterfaceManager.Instance.UpdateInterfaceState(InterfaceState.InGame);
                }
            }
        }

        prevState[player] = gamePadState[player].Buttons.Start;
        kickPrevState[player] = gamePadState[player].Buttons.A;

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

        if (!gamePadState.IsConnected || direction == Vector3.zero) {
            if (Input.GetKey(GameManager.INSTANCE.players[player].moveUp)) {
                direction.z++;
            }

            if (Input.GetKey(GameManager.INSTANCE.players[player].moveLeft)) {
                direction.x--;
            }

            if (Input.GetKey(GameManager.INSTANCE.players[player].moveDown)) {
                direction.z--;
            }

            if (Input.GetKey(GameManager.INSTANCE.players[player].moveRight)) {
                direction.x++;
            }

            GameManager.INSTANCE.players[player].OnMove(direction);
        }

        //moveDelegate?.Invoke(direction);
    }
    private void Kick(int player)
    {

        if (Input.GetKeyDown(GameManager.INSTANCE.players[player].kickKey) || (kickPrevState[player] == ButtonState.Released && gamePadState[player].Buttons.A == ButtonState.Pressed))
        {
            GameManager.INSTANCE.players[player].OnKick();
        }

    }

    
}