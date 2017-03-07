using UnityEngine;
using System.Collections;

public abstract class Buff : MonoBehaviour
{
    public abstract void ApplyBuff();
    public abstract string BuffName { get; }
}
