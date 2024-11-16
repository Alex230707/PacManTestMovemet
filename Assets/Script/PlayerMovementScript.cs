using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementScript : MonoBehaviour
{
    private Vector2 MoveInput;
    private Vector2 MoveDirection;
    private Vector2 LastDirection;
    private Vector2 NextDirection;
    public bool IsOccupied;
    public InputActionReference move;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private LayerMask ObstacleLayer;
    private void OnMovement(InputValue inputValue) {
        //MoveInput = inputValue.Get<Vector2>().normalized;

        MoveDirection = move.action.ReadValue<Vector2>();
        NextDirection = MoveDirection;
        
        LockMovementin4Dir(ref MoveDirection); /// Blocca il Movimento del Player nelle direzioni Sù, Giù, Destra, Sinistra evitando il movimento diagonale        
        
        if (MoveDirection != Vector2.zero)
        {
            LastDirection = MoveDirection;
        }
    }




    void Update()
    {       
            IsOccupied = Occupied(MoveDirection);
            Vector2 direction = MoveDirection != Vector2.zero ? MoveDirection : LastDirection;

            Vector3 Move = new Vector3(direction.x, direction.y) * MoveSpeed * Time.deltaTime; ///Fa traslare in Player
            transform.Translate(Move);

        Debug.Log(IsOccupied);
    }

        private void LockMovementin4Dir(ref Vector2 Input)
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
        }

    bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, MoveDirection, 1.5f, this.ObstacleLayer);
        return hit.collider != null;
    }
    
    void Stop()
        {
            MoveDirection = Vector2.zero;
            LastDirection=Vector2.zero;

        }
    } 
