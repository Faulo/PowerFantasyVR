﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.AI
{
    // Class created by Robin Daraban and permittedly used
    public class MarsagliaGenerator
    {
        public static float Next()
        {
            
            Vector2 validDouble = Random.insideUnitCircle;
            float s = validDouble.sqrMagnitude;
            if(s == 0)
            {
                return Next();
            }
            return validDouble.x * Mathf.Sqrt((-2 * Mathf.Log(s)) / s);
        }
    }
}