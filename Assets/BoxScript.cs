using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float _counter;

    private Material _myMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_counter >= 0.5)
        {
            _myMaterial.mainTextureOffset += new Vector2(0, 0.2f);
            _counter = 0;
        }

        _counter += Time.deltaTime;
    }
}
