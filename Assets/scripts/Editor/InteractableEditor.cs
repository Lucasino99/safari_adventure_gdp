using UnityEditor;
[CustomEditor(typeof(Interactable),true)] //this editor script affects child classes of our interactable
public class InteractableEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        Interactable interactable= (Interactable)target; //currently selected gameobject we're inspecting
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage= EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents= true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }

        }
        else
        {
            base.OnInspectorGUI();
            if(interactable.useEvents)
            {   
                //we are using events --> add the component
                if(interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                //we are not using events --> remove the component.
                if(interactable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
        
    }
}
