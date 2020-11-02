using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    Sprite weaponImage;

    float attackValue;


    virtual public AttackEffection weaponEffection()
    {


        return AttackEffection.A;
    }



}

public enum AttackEffection
{
    ///
    A,
    B,
    C,

}
