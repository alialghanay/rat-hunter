using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventonlyInteract)){
            interactable.promptMessage = EditorGUILayout. TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventonlyInteract can ONLY use UnityEvents.", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null){
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }

        }
        else {
            base.OnInspectorGUI();
            if(interactable.useEvents){
                if(interactable.GetComponent<InteractionEvent>() == null){
                interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }else {
                if(interactable.GetComponent<InteractionEvent>() != null) {
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
