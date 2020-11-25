using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter 
{
    void SetYuanqiByRatio(float value);

    void SetAtkByRatio(float value);
    void Attack();

    void Burn();
    void Hurt(float value);
}
