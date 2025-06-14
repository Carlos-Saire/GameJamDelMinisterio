using System;
using UnityEngine;

public class ButtonUI : MyButton
{
    [SerializeField] private bool open;
    [Header ("DotWeen")]
    [SerializeField] private DoMove doMove;
    protected override void OnClick()
    {
        if (interactue)
        {
            ActiveClick(open);
        }
    }
    private void ResetInput()
    {
        interactue = true;
    }
    public void ActiveClick(bool value)
    {
        if (value)
        {
            doMove.Go(ResetInput);
        }
        else
        {
            doMove.Return(ResetInput);
        }
        interactue = false;
    }
    public void ActiveClick(bool value,Action OnFinish)
    {
        if (value)
        {
            doMove.Go(ResetInput,OnFinish);
        }
        else
        {
            doMove.Return(ResetInput,OnFinish);
        }
        interactue = false;
    }
}
