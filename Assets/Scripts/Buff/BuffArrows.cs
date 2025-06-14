using System;
using UnityEngine;
[CreateAssetMenu(fileName = "BuffArrows", menuName = "Scriptable Objects/Buff/BuffArrows")]
public class BuffArrows : Buff
{
    public static event Action OnArrowBuff;
    public override void ActiveBuff()
    {
        OnArrowBuff?.Invoke();
    }
}
