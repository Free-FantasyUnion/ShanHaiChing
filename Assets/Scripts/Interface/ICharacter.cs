using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter 
{
    float Attack(float value);
    void Hurt(float value);
}
