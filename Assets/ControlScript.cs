using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    public int coins = 0;
    public TMP_Text coinsText;
    public TMP_Text clockText;

    public float maxTime = 600;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins = 0;
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Brick"))
                {
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.CompareTag("Question"))
                {
                    coins++;
                    Destroy(hit.collider.gameObject);
                    coinsText.text = "= " + coins.ToString();
                }
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.gameObject.transform.position += Vector3.right * (25 * Time.deltaTime);
            Debug.Log("To the right!");
        }

        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.gameObject.transform.position -= Vector3.right * (25 * Time.deltaTime);
        }

        timer -= Time.deltaTime;
        clockText.text = Mathf.Floor(timer).ToString();
    }
}
