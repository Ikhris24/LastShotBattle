using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    private Material material;
    private Vector2 offset;

    void Start()
    {
        material = GetComponent<Image>().material;
    }

    void Update()
    {
        offset.x -= scrollSpeed * Time.deltaTime; 
        offset.y += scrollSpeed * Time.deltaTime; 

        material.mainTextureOffset = offset;
    }
}