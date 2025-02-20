using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
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
        
        
    }

    void FixedUpdate()
    {
    }
}
