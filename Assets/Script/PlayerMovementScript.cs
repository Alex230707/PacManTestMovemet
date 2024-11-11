using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementScript : MonoBehaviour
{
    private Vector2 MoveInput;
    private Vector2 MoveDirection;
    private Vector2 LastDirection;
    [SerializeField] private float MoveSpeed;

    private void OnMovement(InputValue inputValue) {
        MoveInput = inputValue.Get<Vector2>().normalized;

        LockInputIn4Dir(ref MoveInput); /// Blocca il Movimento del Player nelle direzioni Sù, Giù, Destra, Sinistra evitando il movimento diagonale

        if(MoveInput != Vector2.zero)
        {
             LastDirection = MoveInput;
        }

    }



    void Update()
    {
        Vector2 direction = MoveInput != Vector2.zero ? MoveInput : LastDirection;

        Vector3 Move = new Vector3(direction.x, direction.y) * MoveSpeed * Time.deltaTime; ///Fa traslare in Player
        transform.Translate(Move);
    }



    private Vector2 LockInputIn4Dir(ref Vector2 Input)
    {
        if (Input.y > 0.1f)
        {
            Input = new Vector2(0, 1);
        }

        else if (Input.y < -0.1f)
        {
            Input = new Vector2(0, -1);
        }

        else if (Input.x > 0.1f)
        {
            Input = new Vector2(1, 0);
        }
        else if (Input.x < -0.1f)
        {
            Input = new Vector2(-1, 0);
        }
        return Input;
    }
}
