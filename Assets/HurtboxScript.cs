using UnityEngine;

public class HurtboxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick") || other.CompareTag("Question"))
        {
            Destroy(other.gameObject);
            transform.parent.GetComponent<CharacterControllerScript>().score += 100;

        }
    }
}
