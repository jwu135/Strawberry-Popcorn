using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static int deathCounter = 0;
    public static List<Vector2> positions = new List<Vector2>();
    public static List<Sprite> bodies = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(deathCounter); 
    }
}
