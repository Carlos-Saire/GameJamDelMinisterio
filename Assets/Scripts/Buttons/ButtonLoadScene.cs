using UnityEngine;
using System.Collections;
public class ButtonLoadScene : MyButton
{
    [SerializeField] private string scene;

    protected override void OnClick()
    {
        if (interactue)
        {
            StartCoroutine(StartAnimation());
        }
    }
    public IEnumerator StartAnimation()
    {
        Transicion.Instance.TransicionCerrarPuertas();
        yield return new WaitForSecondsRealtime(2f);
        LoadSceneManager.LoadScene(scene);
       
    }
}