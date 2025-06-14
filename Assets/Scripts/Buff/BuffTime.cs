using UnityEngine;
[CreateAssetMenu(fileName = "BuffTime", menuName = "Scriptable Objects/Buff/BuffTime")]
public class BuffTime : Buff
{
    [SerializeField] private float duration;
    [SerializeField] private float timeScale;
    public override void ActiveBuff()
    {
        GameManager.instance.BuffTime(duration, timeScale);
    }
}
