using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogBox;
    public TMP_Text dialogText;

    public string[] dialogLines;
    public int currentLine;

    private bool justStarted;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.dialogActive) {
            if (Input.GetButtonUp("Comunicar")) {

                if (!justStarted) {
                    currentLine++;
                    if (currentLine >= dialogLines.Length) {
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogActive = false;
                    } else {
                        dialogText.text = dialogLines[currentLine];
                    }
                } else {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLines, bool shouldWaitForNextClick) {
        dialogLines = newLines;

        currentLine = 0;

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        justStarted = shouldWaitForNextClick;

        GameManager.instance.dialogActive = true;
    }
}
