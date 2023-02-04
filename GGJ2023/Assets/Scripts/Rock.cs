using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        Sprite selectedSprite = sprites[Random.Range(0, sprites.Length)];
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
