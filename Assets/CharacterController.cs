using System;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Math = Unity.Mathematics.Geometry.Math;

public class CharacterController : MonoBehaviour
{
    private Rigidbody _rb;
    public InputAction move;
    public InputAction jump;
    public float playerSpdMax = 10;
    public float playerAccel = 10;
    public float spdCurrent;
    public float vertSpd;
    private float _drag;
    private bool _isJumping;
    private float _jumpTimer;
    private float _gravTimer;
    private float _floatTimer;
    public float gravity = 6;
    public float fallSpdMax = 5;
    public float jumpPwr = 5;
    private float _gravMult = 0;
    private Vector3 startPos;
    float jumpWindow = 0.5f;
    float gravWindow = 0.5f;
    public int score = 0;
    public int coins = 0;
    public GameObject scoreText;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        move.Enable();
        jump.Enable();
        spdCurrent = 0;
        vertSpd = 0;
        _gravMult = 1;
        _gravTimer = 0;
        _jumpTimer = 0;
        startPos = transform.position;
        Debug.Log(Physics.gravity);
        Physics.gravity = new Vector3(0, -7, 0);
        scoreText.GetComponent<TMPro.TextMeshPro>().text = "00000";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TMPro.TextMeshPro>().text = score.ToString("00000");
        Physics.gravity = new Vector3(0, -7, 0);
        if (_jumpTimer > 0)
        {
            _isJumping = true;
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer < 0)
            {
                _jumpTimer = 0;
            }
        }
        Vector3 startPoint = gameObject.transform.position;
        float castDistance = 1.05f;
        //Test if character on ground~
        bool isGrounded = Physics.Raycast(transform.position,
            Vector3.down,
            out RaycastHit hit,
            castDistance);
        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance*Vector3.down, color, 0.1f, false);
        if (transform.position.y < -5)
        {
            Die();
        }
        //Movement left/right when on ground
        float movePwr = move.ReadValue<float>();
        if (jump.ReadValue<float>()>0)
        {
            if (isGrounded)
            {
                _isJumping = true;
            }
            if (!isGrounded)
            {
                _gravTimer += Time.deltaTime;
                _jumpTimer = jumpWindow;
                if (_gravTimer < gravWindow)
                {
                    Physics.gravity = new Vector3(0, -0.7f, 0);
                }
            }
        }
        Debug.Log(Physics.gravity);
        if (isGrounded)
        {
            _gravTimer = 0;
            _rb.AddForce(movePwr*playerAccel, 0, 0);
            _rb.linearVelocity = new Vector3(Mathf.Clamp(_rb.linearVelocity.x, -playerSpdMax, playerSpdMax),0,0);
            _rb.useGravity = false;
            if (movePwr == 0)
            {
                _rb.linearDamping = 7;
            }
            else { _rb.linearDamping = 3; }

            if (_isJumping && _jumpTimer == 0)
            {
                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpPwr, 0);
                _isJumping = false;
            }
            /*vertSpd = 0;
            _gravTimer = 0;
            if (movePwr == 0)
            {
                _drag = 5;
                if (Mathf.Abs(spdCurrent) < 0.2)
                    spdCurrent = 0;
            }
            else if ((spdCurrent > 0 && movePwr < 0) || (spdCurrent < 0 && movePwr > 0))
            {
                _drag = 3;
                spdCurrent += movePwr * playerAccel * Time.deltaTime;
            }
            else
            {
                _drag = 0;
                spdCurrent += movePwr * playerAccel * Time.deltaTime;
            }
            spdCurrent = Mathf.Clamp(spdCurrent, -playerSpdMax, playerSpdMax);
            if (_isJumping)
            {
                _isJumping = false;
                _gravMult -= Mathf.Min(_gravTimer, 0.6f);
                vertSpd = jumpPwr;
            }*/
        }
        else
        {
            _rb.linearDamping = 0.1f;
            _rb.AddForce(movePwr * playerAccel * 0.4f, 0, 0);
            _rb.linearVelocity = new Vector3(Mathf.Clamp(_rb.linearVelocity.x, -playerSpdMax*1.1f, playerSpdMax*1.1f), _rb.linearVelocity.y, 0);

            if (_rb.linearVelocity.y > 0)
            {

            }
            /*_gravMult = 1 - Mathf.Min(_gravTimer, 0.6f);
            spdCurrent += movePwr * playerAccel * Time.deltaTime * 0.3f;
            if (vertSpd > 0)
            {
                vertSpd -= (gravity * Time.deltaTime * _gravMult);
                if (vertSpd < 0.1)
                {
                    vertSpd = 0;
                }
            }

            if (vertSpd == 0)
            {
                _floatTimer += Time.deltaTime;
                if (_floatTimer > 0.3f)
                {
                    vertSpd -= 0.1f;
                }
            }

            if (vertSpd < 0)
            {
                if (_isJumping)
                {
                    _isJumping = false;
                }
                vertSpd -= (gravity * Time.deltaTime);
                if (vertSpd < -fallSpdMax)
                {
                    vertSpd = -fallSpdMax;
                }
            }*/
            _rb.useGravity = true;
        }
        spdCurrent -= _drag * (spdCurrent) * Time.deltaTime;

        //transform.position = new Vector3(transform.position.x + spdCurrent*Time.deltaTime,transform.position.y + vertSpd*Time.deltaTime,0);
        Debug.Log(spdCurrent);
    }

    void FixedUpdate()
    {
    }

    void Die()
    {
        transform.position = startPos;
        score = 0;
        coins = 0;
        Debug.Log("You died!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);

        }
    }
}
