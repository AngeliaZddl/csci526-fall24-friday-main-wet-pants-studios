using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoController : MonoBehaviour
{

    public GameObject ghost;
    public GameObject ghost3;
    public GameObject moveTutoWall;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public TMPro.TextMeshPro tmp;
    public TMPro.TextMeshPro tmp2;
    public GameObject csc;

    public GameObject fPrompt;

    private bool moveLearned = false;
    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;

    private bool t2Triggered = false;
    private bool stunLearned = false;
    private bool fPressed = false;

    private bool t3Triggered = false;
    private bool playerTurned = false;
    private Quaternion target;
    private int turnTries = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (!moveLearned) learnMove();
        //if (t2Triggered && !stunLearned) learnStun();
        if (t2Triggered && !stunLearned) checkF();
        if (t3Triggered && !playerTurned) turnPlayer(0);
    }

    void checkF()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            stunLearned = true;
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.moveAllowed = true;
            fPrompt.SetActive(false);
            cutsceneController c = csc.GetComponent<cutsceneController>();
            c.hide();
        }

    }

    void turnPlayer(float tries)
    {
        tries += Time.deltaTime;
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, target, 0.01f);
        if (tries > 5 || (Vector3.Dot(player.transform.forward, Vector3.left) > 0.999))
        {
            playerTurned = true;
            //playerCamera.transform.rotation *= Quaternion.Euler(Vector3.left * -10);
            letGhost3Loose();
        }
    }

    void letGhost3Loose()
    {
        GhostController gc3 = ghost3.GetComponent<GhostController>();
        gc3.moveSpeed = 2f;
        gc3.moveAllowed = true;
    }

    public void trigger4()
    {
        ghost3.SetActive(false);
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        //tmp2.SetText("Turn Back and Run!");
        cutsceneController c = csc.GetComponent<cutsceneController>();
        c.hide();
        pm.moveAllowed = true;
        pm.turnAllowed = true;
    }

    void learnMove()
    {
        if (Input.GetKeyDown(KeyCode.W)) wPressed = true;
        //if (Input.GetKeyDown(KeyCode.A)) aPressed = true;
        //if (Input.GetKeyDown(KeyCode.S)) sPressed = true;
        //if (Input.GetKeyDown(KeyCode.D)) dPressed = true;
        if (wPressed
            /*
            && aPressed && sPressed && dPressed
            */
            )
        {
            moveLearned = true;
            moveTutoWall.SetActive(false);
        }
    }

    void learnStun()
    {
        if (Input.GetKeyDown(KeyCode.F)) fPressed = true;
        if (fPressed)
        {
            tmp.SetText("Get Past Them Now!");
            stunLearned = true;
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.moveAllowed = true;
        }
    }

    public void trigger1()
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm.moveAllowed = false;
        t1.SetActive(false);
        cutsceneController c = csc.GetComponent<cutsceneController>();
        c.show();
    }

    public void trigger1Turned()
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm.moveAllowed = true;
        t1.SetActive(false);
    }

    public void trigger2()
    {
        t2Triggered = true;
        fPrompt.SetActive(true);
        t2.SetActive(false);
    }

    public void trigger3()
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm.moveAllowed = false;
        pm.turnAllowed = false;
        t3Triggered = true;
        t3.SetActive(false);
        target = player.transform.rotation * Quaternion.Euler(Vector3.up * 180);
        //target *= Quaternion.Euler(Vector3.left * -5);
    }

}
