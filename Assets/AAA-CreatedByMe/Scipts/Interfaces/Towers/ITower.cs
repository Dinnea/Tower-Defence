using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    CapsuleCollider range { get; }
    void Effect();
}
