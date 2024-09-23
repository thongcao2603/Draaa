
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rd;

    private void Awake(){
        rd = GetComponent<Rigidbody2D>();

    }

    private void Update(){
        rd.velocity = new Vector2(Input.GetAxis("horizontal"),rd.velocity.y);

        if (Input.GetKey(KeyCode.Space)){
            rd.velocity = new Vector2(rd.velocity.x, moveSpeed);
        }
    }
}
