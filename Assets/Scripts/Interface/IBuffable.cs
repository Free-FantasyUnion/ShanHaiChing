using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    void SetDefenceByRatio(float value);

    void SetAtkByRatio(float value);

    void SetYuanqiDropByRatio(float value);

    void SetSpeedByRatio(float value);

    void GetBuff(Buff buff);
}
