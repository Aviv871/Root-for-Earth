using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
    private Sprite[] animationFrames;
    [SerializeField] private float animationSpeed = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        animationFrames = Resources.LoadAll<Sprite>("factory-sprite");
        if (animationFrames.Length == 0) {
            throw new System.Exception("Can't find sprites!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator turnIntoTree() {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < animationFrames.Length; i++)
        {
            renderer.sprite = animationFrames[i];
            if (i == 2) {
                GameObject smoke = gameObject.GetComponentInChildren<ParticleSystem>().gameObject;
                GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>().factoryAmount--;
                Destroy(smoke);
            }
            yield return new WaitForSeconds(animationSpeed);
        }
        
    }
}
