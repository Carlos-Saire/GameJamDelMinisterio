using UnityEngine;
public class ButtonLoadScene : MyButton
{
    [SerializeField] private string scene;

    protected override void OnClick()
    {
        if (interactue)
        {
            LoadSceneManager.LoadScene(scene);
        }
    }
}