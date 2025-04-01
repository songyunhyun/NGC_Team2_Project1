using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    Vector2 _moveDir;
    public float _speed = 15;
    float _maxSpeed = 599;
    float _jumpPow = 5;

    public bool _isAttack;
    float _attackSpeed = 1000f;//대쉬 속도
    public float _attackCoolTime = 2f;//대쉬 쿨타임
    public float _attackCoolDown;//대쉬 남은 쿨타임
    public int _attackDamege = 1;
    public float _frictionForce = 0.95f;//마찰력(높을 수록 작은 거)
    [SerializeField] private Rigidbody2D _rigid;
    Vector3 mousePos;
    

    private void FixedUpdate()
    {
        _rigid.AddForce(_moveDir * _speed);
        _rigid.linearVelocity = _rigid.linearVelocity * 0.99f;
        /*
        if (_rigid.linearVelocityX > _maxSpeed && !_isAttack)
        {
            _rigid.linearVelocityX = _maxSpeed;
        }
        else if (_rigid.linearVelocityX < -_maxSpeed && !_isAttack)
        {
            _rigid.linearVelocityX = -_maxSpeed;
        }
        if (_rigid.linearVelocityY > _maxSpeed && !_isAttack)
        {
            _rigid.linearVelocityY = _maxSpeed;
        }
        else if (_rigid.linearVelocityY < -_maxSpeed && !_isAttack)
        {
            _rigid.linearVelocityY = -_maxSpeed;
        }
        
        //_rigid.linearVelocityY -= 0.5f * Time.deltaTime;
        //transform.Translate(0, -0.5f * Time.deltaTime, 0);
        */
    }

    private void Update()
    {
        //moveDir = Input.GetAxis("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = mousePos - transform.position;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (_attackCoolDown <= _attackCoolTime)
        {
            _attackCoolDown += Time.deltaTime;
        }
        if (_isAttack)
        {
            _rigid.linearVelocity -= _rigid.linearVelocity * 0.5f * Time.deltaTime;
            if (_rigid.linearVelocityX <= _maxSpeed && _rigid.linearVelocityY <= _maxSpeed)
            {
                _isAttack = false;
            }
        }
    }

    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>();
    }
    private void OnJump()
    {
        //_rigid.linearVelocityY = _jumpPow;
    }

    private void OnAttack()
    {
        if (_attackCoolDown >= _attackCoolTime)
        {
            _isAttack = true;
            Vector2 a = mousePos - transform.position;
            a.Normalize();
            print(a.magnitude);
            _rigid.linearVelocity = new Vector2(0, 0);
            _rigid.AddForce(a * _attackSpeed);
            _attackCoolDown = 0;
        }
        
        
        //_rigid.AddForce(mousePos - transform.position * _attackSpeed);
    }
}
