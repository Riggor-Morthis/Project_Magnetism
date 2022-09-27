using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBlock
{
    /* Effet du champ magnétique émis par le bloc
     */
    void MagField();

    /* Effet de l'interaction entre un mob et le bloc
     */
    void Interaction();
}
