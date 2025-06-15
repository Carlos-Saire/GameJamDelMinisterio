using UnityEngine;
using System.Collections;
public class FinishDialogueTransicion : MonoBehaviour
{
    [SerializeField] string Scene;
    [SerializeField] DialogueManager Manager;
    private void OnEnable()
    {
        Manager.OnFinishDialogue += FinishDialogueTransitionScene;
    }
    private void OnDisable()
    {
        Manager.OnFinishDialogue -= FinishDialogueTransitionScene;
    }
    public void FinishDialogueTransitionScene()
    {
        StartCoroutine( StartAnimation());
    }
    public IEnumerator StartAnimation()
    {
        Transicion.Instance.TransicionCerrarPuertas();
        yield return new WaitForSecondsRealtime(2f);

        LoadSceneManager.LoadScene(Scene);


    }
}
