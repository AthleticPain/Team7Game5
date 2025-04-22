using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NarrativeHandler : MonoBehaviour
{
    public static NarrativeHandler Instance {get; private set;}

    [Header("Dialogue Settings")]
    [SerializeField] private Sprite[] characterPortraits;
    [SerializeField] private string startNode = "";
    [SerializeField] private bool startAutomatically;

    [Header("Components")]
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private Image activeCharacterPortrait;
    [SerializeField] private GameObject narrativeCanvas;

    [Header("Rest Dialogue Settings")]
    [SerializeField] private string[] restDialogue;
    [SerializeField] private List<string> shuffledRestDialogue;
    [SerializeField] private int restDialogueIndex = 0;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        AddYarnCommands();
    }

    private void Start()
    {
        if(startAutomatically)
        {
            StartDialogue(startNode);
        }
        ShuffleRestDialogue();
    }

    public void StartDialogue(string node)
    {
        dialogueRunner.StartDialogue(node);
    }

    // For Skipping the Dialouge
    public void StopDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
            narrativeCanvas.SetActive(false);
        }
    }

    // Shuffle the dialogue order
    private void ShuffleRestDialogue()
    {
        shuffledRestDialogue = new List<string>(restDialogue);
        for (int i = 0; i < shuffledRestDialogue.Count; i++)
        {
            string temp = shuffledRestDialogue[i];
            int randomIndex = Random.Range(i, shuffledRestDialogue.Count);
            shuffledRestDialogue[i] = shuffledRestDialogue[randomIndex];
            shuffledRestDialogue[randomIndex] = temp;
        }
    }

    // Load randomized rest dialogue in order
    public void LoadRestDialogue()
    {
        // If we’ve reached the end, reshuffle and reset index
        if (restDialogueIndex >= shuffledRestDialogue.Count)
        {
            ShuffleRestDialogue();
            restDialogueIndex = 0;
        }

        string nodeToPlay = shuffledRestDialogue[restDialogueIndex];
        StartDialogue(nodeToPlay);
        restDialogueIndex++;
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
