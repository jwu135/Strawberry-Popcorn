using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public GameObject SP;
    public Button win;
    public Sprite mouseSP1;
    public Sprite mouseSP2;
    public GameObject cursor;
    private Scene scene;
    private GameObject SPObj;
    private float tempY;
    private Vector2 pos;
    private Vector2 finalPos;
    private bool landed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (win != null) {
            win.onClick.AddListener(winGame);
        }
        if (scene.name.Equals("VictoryGameover")){
            cursor.GetComponent<SpriteRenderer>().enabled = false;
            cursor.GetComponent<CursorMovement>().paused = true;
            tempY = Random.Range(-4.5f, -2.5f);
            float finalTempX = Random.Range(-7f, 7f);
            pos = new Vector2(finalTempX, 8f);
            finalPos = new Vector2(finalTempX, tempY);
            SPObj = Instantiate(SP,pos,SP.transform.rotation);
        }
    }

    public void winGame()
    {
        SceneManager.LoadScene("Scenes/Outro");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (scene.name.Equals("VictoryGameover")) {
            if (!landed) {
                //SPObj.transform.position = Vector2.Lerp(pos, finalPos, Time.deltaTime);
                Vector2 temp = SPObj.transform.position;
                temp.y -= 0.12f;
                SPObj.transform.position = temp;
                //Debug.Log(finalPos);
                if (SPObj.transform.position.y <= finalPos.y) {
                    landed = true;
                    Debug.Log("landed");
                    SPObj.GetComponent<SpriteRenderer>().enabled = false;
                    StartCoroutine("mouse");
                }
            }
        }

    }
    IEnumerator mouse()
    {
        cursor.transform.position = finalPos;
        cursor.transform.localScale = new Vector3(0.4f, 0.4f);
        cursor.GetComponent<SpriteRenderer>().sprite = mouseSP1;
        cursor.GetComponent<MouseEyeControl>().MouseEyeClosed = mouseSP1;
        cursor.GetComponent<MouseEyeControl>().MouseEyeOpen = mouseSP2;
        cursor.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        cursor.GetComponent<CursorMovement>().paused = false;
    }
}
