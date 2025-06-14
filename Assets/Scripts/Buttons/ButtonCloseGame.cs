using UnityEngine;
public class ButtonCloseGame : MyButton
{
    protected override void OnClick()
    {
        if (interactue)
        {
            Debug.Log("Se Cerro el Juego");
            Application.Quit();
        }
    }
}