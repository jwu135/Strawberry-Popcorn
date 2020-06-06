using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDialogueBossRoom : MonoBehaviour
{
    public RuntimeAnimatorController phase1;
    public RuntimeAnimatorController phase2;
    public RuntimeAnimatorController phase3;
    private float textspeed = 0.025f;
    private float baseTextSpeed;
    private int tempSwapper;
    private string finalSentence;
    private string currSentence;
    private GameObject dialogueBox;
    private string[] dialogue = new string[3];
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("EventSystem").GetComponent<CutsceneSystem>().interactSign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        currSentence = "";
        dialogueBox = transform.Find("Canvas").Find("DialogueBox").gameObject;
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 1) {
            dialogue[0] = "Aeons Ago";
            dialogue[1] = "I opened my eyes";
            dialogue[2] = "And saw I was alone";
            GetComponent<Animator>().runtimeAnimatorController = phase1;
        }
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 2) {
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector3(-145,53);
            dialogue[0] = "Life was created by my hands";
            dialogue[1] = "And by consuming its flesh";
            dialogue[2] = "I too could experience living";
            GetComponent<Animator>().runtimeAnimatorController = phase2;
        }
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 3) {
            dialogue[0] = "One last experience eludes me";
            dialogue[1] = "One last creation I will make";
            dialogue[2] = "One, who shall soon replace me";
            GetComponent<Animator>().runtimeAnimatorController = phase3;
        }
        GameObject.FindGameObjectWithTag("music").GetComponent<MusicManagerBossRoom>().lowerVolume();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformMovementPhys>().unableToMove = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformMovementPhys>().ableToJump = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FlightMovementPhys>().unableToMove = true;
        transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
        talk();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) {
            if (Input.GetButtonDown("interact")) {
                talk();
            }
        }
    }
    void talk()
    {
        if (index < dialogue.Length) {
            finalSentence += dialogue[index];
            //currSentence += "\n";
            StartCoroutine("textScroll");
            index++;
        } else {
            GameObject.Find("EventSystem").GetComponent<CutsceneSystem>().eaten = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformMovementPhys>().unableToMove = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformMovementPhys>().ableToJump = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FlightMovementPhys>().unableToMove = false;
            GameObject.FindGameObjectWithTag("music").GetComponent<MusicManagerBossRoom>().resetVolume();
            Destroy(gameObject);
        }
    }
    IEnumerator textScroll()
    {
        if (currSentence.Length < finalSentence.Length) {
            if (tempSwapper % 2 == 0) {
                if (finalSentence[currSentence.Length] != ' ') {
                    //SoundManager.PlaySound("playerTalk2");
                    int swapper = Random.Range(0, 4);
                    if (swapper == 0) {
                        SoundManager.PlaySound("playerTalk1");
                    } else if (swapper == 1) {
                        SoundManager.PlaySound("playerTalk2");
                    } else if (swapper == 2) {
                        SoundManager.PlaySound("playerTalk3");
                    } else if (swapper == 3) {
                        SoundManager.PlaySound("playerTalk4");
                    }
                    textspeed = baseTextSpeed;
                }
            } else {
                textspeed = baseTextSpeed;
            }
            if (finalSentence[currSentence.Length] == '!' || finalSentence[currSentence.Length] == '.' || finalSentence[currSentence.Length] == '?')
                textspeed = baseTextSpeed * 10f;
            if (finalSentence[currSentence.Length] == ',')
                textspeed = baseTextSpeed * 3f;
            tempSwapper++;

            currSentence += finalSentence[currSentence.Length];
            dialogueBox.transform.Find("Sentence").GetComponent<Text>().text = currSentence;
            yield return new WaitForSeconds(textspeed);
            StartCoroutine("textScroll");
        } else {
            currSentence += "\n";
            finalSentence += "\n";
        }

    }
}
