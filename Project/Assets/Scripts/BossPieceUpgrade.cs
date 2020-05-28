using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPieceUpgrade : MonoBehaviour
{
    bool over = false;
    public bool mainPiece = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.gameObject.name == "Floor") { // as far as I know, only the groundfloor satisfies both of these
            Destroy(GetComponent<Rigidbody2D>());
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            Vector2 temp = transform.position;
            temp.y += 0.1f;
            transform.position = temp;
        }
        if (collision.gameObject.tag == "Player") {
            over = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            over = false;
        }
    }
    public void eat()
    {
        // upgrade stuff
        // give 2 points
        SoundManager.PlaySound("eatingQuestionMark");
        if (mainPiece) {
            UpgradeValues.upgradePoints += 2;
        } else { // gives 1 point
            UpgradeValues.upgradePoints += 1;
        }
        Destroy(gameObject);
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    void lookAround()
    {
        if (Input.GetButtonDown("interact") && over) {
            eat();
        }
    }

}
