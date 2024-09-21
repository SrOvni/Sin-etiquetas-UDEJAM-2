using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;
public class NPCDialogues : MonoBehaviour, INPC
{
    InputManager _inputs;
    int index = 0;
    [SerializeField] NPCScriptableObject npcDialogues;
    [SerializeField] private Image spaceBarImage;
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    [SerializeField] private Canvas dialogueBoxCanvas;
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private Button exitButton;
    [SerializeField] private Image exclamationSign;
    [Header("Animaciones")]
    [SerializeField] private Transform dialogueBoxPosition;
    [SerializeField] private Transform npcDialogueBoxTargetPosition;
    [SerializeField] private Transform initialDialogueBoxPosition;
    [SerializeField] private Transform npcImage;
    [SerializeField] private Transform npcImageTargetPosition;
    [SerializeField] private Transform npcInitialImagePosition;
    [SerializeField] private Transform protaImage;
    [SerializeField] private Transform protaImageTargetPosition;
    [SerializeField] private Transform protaIntialImagePostion;
    [SerializeField] private Transform protaDialogueBoxPosition;
    [SerializeField] private Transform protaDialogueBoxTargetPosition;
    [SerializeField] AnimationCurve animationCurveForCanvasDialogues;
    public Image SpaceBarImage {get{return spaceBarImage;}private set { spaceBarImage = value; } }
    public Image ExclamationSign {get{return exclamationSign;} set { exclamationSign = value;}}
    Transform imageTarget;
    Transform dialogueBoxTarget;
    bool animate = false;
    bool animationFinished = false;
    [SerializeField] UnityEvent OnDialogueStart;
    [SerializeField] UnityEvent OnDialgoueEnd;
    


    private void Awake()
    {

        _inputs = GetComponent<InputManager>();
        if(npcDialogues is null)
        {
            npcDialogues = new NPCScriptableObject();
        }
    }
    private void Update() {
        if(animate)
        {
            PlayDialogueAnimation();
        }
    }
    
    public void PlayDialogue()
    {
        spaceBarImage.gameObject?.SetActive(false);
        dialogueBoxCanvas.gameObject?.SetActive(true);
        npcImage.gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(string part in npcDialogues.dialogues)
        {
            animate = true;
            animationFinished = false;
            yield return new WaitUntil(()=>animationFinished);
            
            dialogueBoxText.text = string.Empty;
            foreach(char letter in part)
            {
                dialogueBoxText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            index++;
            yield return new WaitUntil(()=>_inputs.IsSpaceBarPressed);
            
        }
        animationFinished = false;
        EndDialogueAnimation();
        yield return new WaitUntil(()=>animationFinished);
        ExitDialogue();
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
        if (npcDialogues.dialogueOrder[index] == WhoIsTalking.Prota)
        {
            npcImage.DOMoveX(npcInitialImagePosition.position.x,1f);
            imageTarget = protaImageTargetPosition.transform;
            dialogueBoxTarget = protaDialogueBoxTargetPosition.transform;
            dialogueBoxPosition.DOMoveX(dialogueBoxTarget.position.x,1f).onComplete = AnimationFinished;
            protaImage.DOMoveX(imageTarget.position.x, 1f);
        }else{
            if(index > 0)
            {
                if(npcDialogues.dialogueOrder[index-1] == WhoIsTalking.Prota)
                {
                    protaImage.DOMoveX(protaIntialImagePostion.position.x, 1f);
                    npcImage.DOMoveX(npcImageTargetPosition.position.x, 1f).onComplete = AnimationFinished;
                    dialogueBoxPosition.DOMoveX(npcDialogueBoxTargetPosition.position.x, 1f);

                }else{
                    AnimationFinished();
                }
            }else{
                imageTarget = npcImageTargetPosition.transform;
                dialogueBoxTarget = npcDialogueBoxTargetPosition.transform;
                dialogueBoxPosition.transform.DOMoveY(dialogueBoxTarget.position.y,1f).onComplete = AnimationFinished;
                npcImage.DOMoveX(imageTarget.position.x, 1f);
            }
        }
    }
    void AnimationFinished()
        {
            animationFinished = true;
            animate = false;
            Debug.Log("Animation finished");
        }
    void EndDialogueAnimation()
        {
            OnDialgoueEnd?.Invoke();
            dialogueBoxPosition.DOMove(initialDialogueBoxPosition.position, 1f).onComplete = AnimationFinished;
            npcImage.DOMoveX(npcInitialImagePosition.position.x, 1f);
        }
}
