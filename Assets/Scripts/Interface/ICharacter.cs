using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter 
{
    void Attack();
    void Burn(float burnValue, int burnTime);
    void Hurt(float value);
}
