using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMaterial : MonoBehaviour
{
    //Variables
    public float scrollX = 0.1f;
    public float scrollY = 0.1f;

    void Update()
    {
        float offsetX = Time.time * scrollX /100;
        float offsetY = Time.time * scrollY /100; 
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
