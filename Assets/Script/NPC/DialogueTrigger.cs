using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            AudioManager.Instance.PlayVFX("Dialogue");
            dialogueManager.OpenDialogue(messages, actors);
        }
        else
        {
            Debug.LogError("DialogueManager not found!");
        }
    }

    public void EndDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.CloseDialogue(); // Đóng đối thoại khi kết thúc
        }
    }

    [System.Serializable]
    public class Message
    {
        public string localizationKey; // Thay vì message trực tiếp+
        public int actorId; // Thêm actorId
    }

    [System.Serializable]
    public class Actor
    {
        public string name;
        public Sprite sprite;
    }
}
