﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Buff 
{
    public enum BuffType
    {
        DefenceUp,
        AtkUp,
        SpeedUp,
        SpeedDown,
        YuanqiDropSlower
    }
    private BuffType buffType;
    Sprite BuffImage;
    float defenceRatio;
    float atkRatio;
    float speedRatio;
    float yuanqiDropRatio;

    public Buff(BuffType type)
    {
        this.buffType = type;
        this.defenceRatio = 1.0f;
        this.atkRatio = 1.0f;
        this.speedRatio = 1.0f;
        this.yuanqiDropRatio = 1.0f;
        switch (type)
        {
            case BuffType.DefenceUp:
                {
                    this.defenceRatio = 1.5f;
                    break;
                }

            case BuffType.AtkUp:
                {
                    this.atkRatio = 1.5f;
                    break;
                }
            case BuffType.SpeedDown:
                {
                    this.speedRatio = 1.3f;
                    break;
                }
            case BuffType.SpeedUp:
                {
                    this.speedRatio = 0.7f;
                    break;
                }
            case BuffType.YuanqiDropSlower:
                {
                    this.yuanqiDropRatio = 0.7f;
                    break;
                }
            default:
                break;

        }
    }

    public void BuffEffect(IBuffable targetCharacter)
    {
        
        switch (buffType)
        {
            case BuffType.DefenceUp:
            {
                    targetCharacter.SetDefenceByRatio(this.defenceRatio);
                    break;
            }
                
            case BuffType.AtkUp:
                {
                    targetCharacter.SetAtkByRatio(this.atkRatio);
                    break;
                }
            case BuffType.SpeedDown:
                {
                    targetCharacter.SetSpeedByRatio(this.speedRatio);
                    break;
                }
            case BuffType.SpeedUp:
                {
                    targetCharacter.SetSpeedByRatio(this.speedRatio);
                    break;
                }
            case BuffType.YuanqiDropSlower:
                {
                    targetCharacter.SetYuanqiDropByRatio(this.yuanqiDropRatio);
                    break;
                }
            default:
                break;

        }
    }
    
}
