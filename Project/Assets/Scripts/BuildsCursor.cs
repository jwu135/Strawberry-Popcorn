using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildsCursor : MonoBehaviour
{
    bool over = false;
    string name;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Builds") {
            name = col.gameObject.name;
            over = true;
        }
    } 
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Builds") {
            over = false;
        }
    }
    private void Update()
    {
        if (!GameObject.Find("EventSystem").GetComponent<Builds>().chooseCorpse&&over&& (Input.GetButtonDown("interact")|| Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))) {
            if(name== "SPCorpse0")
                GameObject.Find("EventSystem").GetComponent<Builds>().chooseOne();
            else if(name == "SPCorpse1")
                GameObject.Find("EventSystem").GetComponent<Builds>().chooseTwo();
            else if(name == "SPCorpse2")
                GameObject.Find("EventSystem").GetComponent<Builds>().chooseThree();
        }
    }
}
