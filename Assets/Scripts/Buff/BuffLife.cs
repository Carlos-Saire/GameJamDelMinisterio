using System;
using UnityEngine;
[CreateAssetMenu(fileName = "BuffLife", menuName = "Scriptable Objects/Buff/BuffLife")]
public class BuffLife : Buff
{
    [SerializeField] private int life;
    public static event Action<int> OnBuffLife;
    public override void ActiveBuff()
    {
        OnBuffLife?.Invoke(life);
    }
}
