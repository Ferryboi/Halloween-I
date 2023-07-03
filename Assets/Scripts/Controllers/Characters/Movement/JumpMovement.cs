using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : Movement
{
    protected bool _isGrounded;
    [SerializeField] protected Rigidbody _rb;

    private LayerMask layerMask;


    protected virtual void Awake()
    {
        SetDirection(Vector3.up);
        _lastValidPosition = transform.position;
        _rb = GetComponent<Rigidbody>();

        layerMask = LayerMask.GetMask(new string[] { "Ground" });
    }

    protected virtual void Update()
    {
        CheckIsGrounded();
    }

    private void CheckIsGrounded()
    {
        Vector3 center = transform.position + (Vector3.up * 0.05f);
        //Vector3 halfExtents = new Vector3(transform.localScale.x / 2, 0.1f, transform.localScale.z / 2);
        //Debug.Log(Physics2D.CircleCast(center, transform.localScale.x / 2, Vector3.down, 0.1f, gameObject.layer).collider);
        _isGrounded = Physics2D.CircleCast(center, transform.localScale.x / 2, Vector3.down, 0.1f, layerMask).collider;
        //_isGrounded = Physics.CheckBox(transform.position, halfExtents, transform.rotation, gameObject.layer);
        if (_isGrounded) _lastValidPosition = transform.position;
    }

    public override void Move(float scale = 1)
    {
        Debug.Log("Is grounded: " + _isGrounded + ", layer: " + gameObject.layer);
        //if (!_isGrounded) return;

        _movement = _direction * _speed * scale;
        if(_rb) _rb.AddForce(_movement, ForceMode.Impulse);
        Debug.Log("Jump activated");
    }

    public override MovementType GetMovementType()
    {
        return MovementType.Jump;
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + (Vector3.up * 0.05f);
        Vector3 halfExtents = new Vector3(transform.localScale.x / 2, 0.1f, transform.localScale.z / 2);

        Gizmos.DrawWireSphere(center, transform.localScale.x / 2);
    }
}
