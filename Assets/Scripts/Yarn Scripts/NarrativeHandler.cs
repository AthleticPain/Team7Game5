using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NarrativeHandler : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private Sprite[] characterPortraits;
    [SerializeField] private string startNode = "";
    [SerializeField] private bool startAutomatically;

    [Header("Components")]
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private Image activeCharacterPortrait;
    [SerializeField] private GameObject narrativeCanvas;
     private void Awake()
    {
        AddYarnCommands();
    }

    private void Start()
    {
        if(startAutomatically)
        {
            StartDialogue(startNode);
        }
    }

    public void StartDialogue(string node)
    {
        dialogueRunner.StartDialogue(node);
    }

    // Converts Functions to Yarn Commands
    private void AddYarnCommands()
    {
        dialogueRunner.AddCommandHandler("YarnTest", () => YarnTest());
        dialogueRunner.AddCommandHandler<int>("ChangeCharacterSprite", ChangeCharacterSprite);
        dialogueRunner.AddCommandHandler<string>("LoadSceneByName", LoadSceneByName);
        dialogueRunner.AddCommandHandler<bool>("LoadNarrative", LoadNarrative);
    }

    private void YarnTest()
    {
        Debug.Log("Test");
    }

    public void ChangeCharacterSprite(int index)
    {
        if (index >= 0 && index < characterPortraits.Length)
        {
            activeCharacterPortrait.gameObject.SetActive(true);
            activeCharacterPortrait.sprite = characterPortraits[index];
        }else{
            activeCharacterPortrait.gameObject.SetActive(false);
        }
    }
    private void LoadSceneByName(string sceneName) => SceneManager.LoadScene(sceneName);
    private void LoadNarrative(bool isActive) => narrativeCanvas.SetActive(isActive);
}
