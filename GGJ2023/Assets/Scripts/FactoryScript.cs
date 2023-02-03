using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator turnIntoTree() {
        yield return new WaitForSeconds(animationSpeed);
        Sprite[] animation = Resources.LoadAll<Sprite>("factory-sprite");
        if (animation.Length == 0) {
            throw new System.Exception("Can't find sprites!");
        }
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        // renderer.sprite = 
    }
}
