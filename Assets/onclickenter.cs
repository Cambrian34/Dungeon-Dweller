using UnityEngine;

public class OnClickEnter : StateMachineBehaviour
{
    private InteractionZone interactionZone;

    // Called when the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        interactionZone = GameObject.FindGameObjectWithTag("interactionzone")
            ?.GetComponent<InteractionZone>();

        if (interactionZone == null)
        {
            Debug.LogError("InteractionZone2D not found or missing 'interactionzone' tag!");
        }
    }

    // Called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Check if Enter key is pressed
        {
            if (interactionZone != null && interactionZone.CanInteract())
            {
                animator.SetTrigger("Door Open"); // Ensure this matches the trigger defined in the Animator

                changescene sceneChanger = animator.gameObject.AddComponent<changescene>();
                //check current scene and load next scene
                if (animator.gameObject.scene.name == "MainGame")
                {
                   sceneChanger.changescene1("Level 1");
                }
                else if (animator.gameObject.scene.name == "Level 1")
                {
                   sceneChanger.changescene1("Level 2");
                }
                else if (animator.gameObject.scene.name == "Level 2")
                {
                   sceneChanger.changescene1("Level 3");
                }
                else if (animator.gameObject.scene.name == "Level 3")
                {
                   sceneChanger.changescene1("Main Game");
                }
                else
                {
                    //print current scene
                    Debug.Log("Current Scene: " + animator.gameObject.scene.name);
                    Debug.LogWarning("No scene to load.");
                }
            }
        }
    }
}
