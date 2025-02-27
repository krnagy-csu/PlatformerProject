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
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPoint = gameObject.transform.position;
        float castDistance = 1f;
        //Test if character on ground~
        bool isGrounded = Physics.Raycast(transform.position,
            Vector3.down,
            out RaycastHit hit,
            castDistance);
        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance*Vector3.down, color, 0.1f, false);
        
        //Movement left/right when on ground
        float movePwr = move.ReadValue<float>();
        if (jump.ReadValue<float>()>0)
        {
            _isJumping = true;
            _gravTimer += Time.deltaTime;
        }
        if (isGrounded)
        {
            vertSpd = 0;
            if (movePwr == 0)
            {
                _drag = 5;
                if (Mathf.Abs(spdCurrent) < 0.2)
                    spdCurrent = 0;
                /*_spdCurrent *= (0.5f * Time.deltaTime);
                if (Mathf.Abs(_spdCurrent) < 0.2f)
                {
                    _spdCurrent = 0;
                }*/
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
                vertSpd = 5;
            }
        }
        else
        {
            spdCurrent += movePwr * playerAccel * Time.deltaTime * 0.3f;
            if (vertSpd > 0)
            {
                vertSpd -= (gravity * Time.deltaTime * vertSpd * _gravMult);
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
                vertSpd -= (gravity * 1.3f * Time.deltaTime * vertSpd * _gravMult);
            }
            
        }
        spdCurrent -= _drag * (spdCurrent) * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + spdCurrent*Time.deltaTime,transform.position.y + vertSpd*Time.deltaTime,0);
        Debug.Log(spdCurrent);
    }

    void FixedUpdate()
    {
    }
}
