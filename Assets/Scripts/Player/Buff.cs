using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    DefenceUp,
    AtkUp,
    FireBlade
}
public abstract class Buff 
{
    private BuffType buffType;
    Sprite BuffImage;
    float yuanqiRatio;
    float atkRatio;

    public Buff(BuffType type)
    {
        this.buffType = type;
    }

    public void BuffEffect(IBuffable targetCharacter)
    {
        
        switch (buffType)
        {
            case BuffType.DefenceUp:
            {
                    targetCharacter.SetYuanqiByRatio(yuanqiRatio);
                    break;
            }
                
            case BuffType.AtkUp:
                {
                    targetCharacter.SetAtkByRatio(atkRatio);
                    break;
                }
            default:
                break;

        }
    }
    
}
