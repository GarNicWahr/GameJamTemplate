using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float _health;

    private float _stamina;
    


    public float StatValues(bool stat)
    {
        if(stat)
        {
            return _health;
        }
        else
        {
            return _stamina;
        }
    }

    public void SetValues(int stat, float value)
    {
        if(stat == 0)
        {
            _health = value;
        }

        if(stat == 1)
        {
            _stamina = value;
        }
    }
}
