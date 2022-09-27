using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AHumanoid : MonoBehaviour, IPolarity
{
    protected char m_Polarity; //Pour stocker la polarité actuelle de notre personnage
    protected Rigidbody2D m_RigidBody; //Notre rigidbody (on l'utilise pour deplacer le perso tout en interagissant avec le moteur physique)
    protected bool m_ForcedMove; //Un champ magnétique vient de forcer notre mouvement

    //Appelé lorsque le script est activé, on s'en sert pour initialiser la valeur de m_Polarity
    protected void Awake()
    {
        m_Polarity = 'p';
        m_ForcedMove = false;
        
    }

    //Deux fonctions pour vérifier rapidement la polarité de l'humanoïde
    public bool IsNegative()
    {
        return m_Polarity.Equals('n');
    }
    public bool IsPositive()
    {
        return m_Polarity.Equals('p');
    }

    public void PolaritySwitch()
    {
        if (m_Polarity.Equals('p')) m_Polarity = 'n';
        else m_Polarity = 'p';
    }

    /* "Reset" la gravité : remet la vélocité en y à 0;
     */
    public void ResetGravity()
    {
        if(m_RigidBody.velocity.y < 0) m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 0f);
    }

    /* Pour permettre à d'autres gameObjects de bouger notre humanoïde
     */
    public void ForceMove(Vector2 move)
    {
        m_RigidBody.AddForce(move);
        m_ForcedMove = true;
    }
}
