using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPolarity
{
    //Inverser la polarité actuelle
    void PolaritySwitch();

    //Récupérer notre polarité actuelle
    bool IsNegative();
    bool IsPositive();
}
