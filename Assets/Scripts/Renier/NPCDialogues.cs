using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;
public class NPCDialogues : MonoBehaviour
{
    InputManager _inputs;
    int index = 0;
    int decisionIndex = 0;
    [SerializeField] NPCScriptableObject currentDialogue;
    [SerializeField] NPCScriptableObject npcMainDialogue;
    [SerializeField] NPCScriptableObject npcOnProccesDialogue;
    [SerializeField] NPCScriptableObject npcOnCompleteMission;
    [SerializeField] DialogueInteractions dialogueInteractions;
    [SerializeField] private GameObject spaceBarImage;
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    [SerializeField] private Canvas dialogueBoxCanvas;
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject exclamationSign;

    private bool canInteract = true;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }

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
    public GameObject SpaceBarImage {get{return spaceBarImage;}private set { spaceBarImage = value; } }
    public GameObject ExclamationSign {get{return exclamationSign;} set { exclamationSign = value;}}
    Transform imageTarget;
    Transform dialogueBoxTarget;
    bool animate = false;
    bool animationFinished = false;
    [SerializeField] UnityEvent OnDialogueStart;
    [SerializeField] UnityEvent OnDialgoueEnd;
    [SerializeField] UnityEvent OnMissionRejection;
    [SerializeField] UnityEvent OnAcceptMission;
    public bool playerTakesDesicion {get;set;}
    public bool missionWasRejected {get;set;}
    [SerializeField] Button takeMissionButton;
    [SerializeField] TextMeshProUGUI takeMissionText;
    [SerializeField] Button rejectMissionButton;
    [SerializeField] TextMeshProUGUI rejectMissionText;
    


    private void Awake()
    {
        currentDialogue = npcMainDialogue;
        if(currentDialogue is null)
        {
            Debug.LogWarning("No hay dialogos");
        }
        _inputs = GetComponent<InputManager>();
    }
    private void Update() {
        if(animate)
        {
            PlayDialogueAnimation();
        }
    }
    void PlayDialogue()
    {     
            spaceBarImage?.SetActive(false);
            dialogueBoxCanvas.gameObject?.SetActive(true);
            npcImage.gameObject.SetActive(true);
            StartCoroutine(TypeLine());      
    }
    public void PlayDialogueQuest()
    {
        
        index = 0;
        dialogueBoxText.text = string.Empty;
        _inputs.MovementDirection = Vector2.zero;
        dialogueInteractions.Movement.rb.velocity = new Vector3(0, 0, 0);
        dialogueInteractions.Movement.enabled = false;
        missionWasRejected = false;
        playerTakesDesicion = false;
        PlayDialogue();
    }
    public void PlayOnProcessDialogue()
    {
        currentDialogue = npcOnProccesDialogue;
    }
    public 
    IEnumerator TypeLine()
    {
        foreach(string part in currentDialogue.dialogues)
        {
            string line = part;
            animate = true;
            animationFinished = false;
            if(missionWasRejected)
            {
                index = currentDialogue.dialogueOrder.Length - 1;
                line = currentDialogue.npcsRejectionLine;
            }
            if(currentDialogue.dialogueOrder[index] == WhoIsTalking.Prota)
            {
                dialogueBoxText.text = string.Empty;
                if(currentDialogue.isPlayerDecision[decisionIndex])
                {
                    TakeDecision();
                    yield return new WaitUntil(()=> playerTakesDesicion);
                    HideDecisionButton();
                }else{
                    missionWasRejected = false;
                }
                if (missionWasRejected)
                {
                    line = currentDialogue.protaRejectionLine;
                }
            }
            yield return new WaitUntil(()=>animationFinished);
            
            dialogueBoxText.text = string.Empty;
            foreach(char letter in line)
            {
                dialogueBoxText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            index++;
            yield return new WaitUntil(()=>_inputs.Interact);
            
        }
        animationFinished = false;
        EndDialogueAnimation();
        yield return new WaitUntil(()=>animationFinished);
        ExitDialogue();
    }
    public void ExitDialogue()
    {
        dialogueInteractions.Movement.enabled = true;
        dialogueBoxCanvas.gameObject?.SetActive(false);
        npcImage.gameObject.SetActive(true);
        spaceBarImage.gameObject.SetActive(true);
        //Debug.Log("Acabo el dialogo");
        OnDialgoueEnd?.Invoke();
        if (missionWasRejected)
        {
            OnMissionRejection?.Invoke();
            missionWasRejected = false;
        }else{
            OnAcceptMission?.Invoke();
        }
    }
    void PlayDialogueAnimation()
    {
        if (currentDialogue.dialogueOrder[index] == WhoIsTalking.Prota)
        {
            npcImage.DOMoveX(npcInitialImagePosition.position.x,1f);
            imageTarget = protaImageTargetPosition.transform;
            dialogueBoxTarget = protaDialogueBoxTargetPosition.transform;
            dialogueBoxPosition.DOMove(dialogueBoxTarget.position,1f).onComplete = AnimationFinished;
            protaImage.DOMoveX(imageTarget.position.x, 1f);
        }else{
            if(index > 0)
            {
                if(currentDialogue.dialogueOrder[index-1] == WhoIsTalking.Prota)
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
        //Debug.Log("Animation finished");
    }
    void EndDialogueAnimation()
    {
        dialogueBoxPosition.DOMove(initialDialogueBoxPosition.position, 1f).onComplete = AnimationFinished;
        npcImage.DOMoveX(npcInitialImagePosition.position.x, 1f);
    }
    void TakeDecision()
    {
        takeMissionText.text = currentDialogue.playerDecisions[decisionIndex].desicion1;
        rejectMissionText.text = currentDialogue.playerDecisions[decisionIndex].desicion2;
        takeMissionButton.gameObject.SetActive(true);
        rejectMissionButton.gameObject.SetActive(true);
    }
    void HideDecisionButton()
    {
        takeMissionButton.gameObject.SetActive(false);
        rejectMissionButton.gameObject.SetActive(false);
    }

    public void OnProcess()
    {
        currentDialogue= npcOnProccesDialogue;
    }
    public void OnComplete()
    {
        exclamationSign.SetActive(false);
        currentDialogue=npcOnCompleteMission;
    }
}
