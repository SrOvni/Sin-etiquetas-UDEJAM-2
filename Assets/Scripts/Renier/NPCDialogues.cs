using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class NPCDialogues : MonoBehaviour, INPC
{
    InputManager _inputs;
    int index = 0;
    [SerializeField] NPCScriptableObject npcDialogues;
    [SerializeField] private Image spaceBarImage;
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    [SerializeField] private Canvas dialogueBoxCanvas;
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private Image npcImage;
    [SerializeField] private Button exitButton;
    [SerializeField] private Image exclamationSign;
    [SerializeField] private Transform npcDialogueBoxPosition;
    private Transform npcInitialDialogueBoxPosition;
    [SerializeField] private Transform npcImagePosition;
    private Transform npcInitialImagePosition;
    [SerializeField] private Transform protaImagePosition;
    private Transform protaIntialImagePostion;
    [SerializeField] private Transform protaDialogueBoxPosition;
    private Transform protaInitialDialogueBoxPosition;
    [SerializeField] AnimationCurve animationCurveForCanvasDialogues;
    public Image SpaceBarImage {get{return spaceBarImage;}private set { spaceBarImage = value; } }
    public Image ExclamationSign {get{return exclamationSign;} set { exclamationSign = value;}}
    


    private void Awake()
    {
        npcInitialDialogueBoxPosition = npcDialogueBoxPosition;
        npcInitialImagePosition = npcImagePosition;

        protaInitialDialogueBoxPosition = protaDialogueBoxPosition;
        protaIntialImagePostion = protaImagePosition;

        _inputs = GetComponent<InputManager>();
        if(npcDialogues is null)
        {
            npcDialogues = new NPCScriptableObject();
        }
    }
    public void PlayDialogue()
    {
        spaceBarImage.gameObject.SetActive(false);
        dialogueBoxCanvas.gameObject?.SetActive(true);
        npcImage.gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(string part in npcDialogues.dialogues)
        {
            
            dialogueBoxText.text = string.Empty;
            foreach(char letter in part)
            {
                dialogueBoxText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            index++;
            
            yield return new WaitUntil(()=>_inputs.IsSpaceBarPressed);
            
        }
    }
    public void ExitDialogue()
    {
        dialogueBoxCanvas.gameObject?.SetActive(false);
        npcImage.gameObject.SetActive(true);
        spaceBarImage.gameObject.SetActive(true);
        StopCoroutine(TypeLine());
    }
    void PlayDialogueAnimation()
    {
        Vector2 imageTarget;
        Vector2 dialogueBoxTarget;
        bool animationHasFinished;
        if (npcDialogues.dialogueOrder[index] == WhoIsTalking.Prota)
        {
            imageTarget = protaImagePosition.transform.position;
            dialogueBoxTarget = protaDialogueBoxPosition.transform.position;
            protaDialogueBoxPosition.transform.position = Vector2.Lerp(protaDialogueBoxPosition.transform.position, dialogueBoxTarget, animationCurveForCanvasDialogues.Evaluate(Time.deltaTime));
        }else{
            imageTarget = npcImage.transform.position;
            dialogueBoxTarget = npcDialogueBoxPosition.transform.position;
        }
        /*
        dialogueBoxCanvas.transform.position = Vector2.Lerp(dialogueBoxCanvas.transform.position, npcDialoguePanelPosition.position, animationCurveForCanvasDialogues.Evaluate(Time.deltaTime));
        yield return new WaitUntil(dialogueBoxCanvas.transform.position == target);
        */

    }
}
