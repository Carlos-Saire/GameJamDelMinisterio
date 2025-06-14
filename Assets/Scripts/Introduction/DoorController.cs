using UnityEngine;
public class DoorController : Interactue
{
    [Header("Scene")]
    [SerializeField] private string nameScene;
    private bool isDialogue;
    protected override void Interaction()
    {
        if(isDialogue)
        {
            LoadSceneManager.LoadScene(nameScene);
        }
    }
    public void SetBool(bool value)
    {
        isDialogue= value;
    }
}
