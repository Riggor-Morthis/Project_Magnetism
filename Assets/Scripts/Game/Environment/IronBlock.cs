using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBlock : MonoBehaviour, IBlock, IPolarity
{
    char m_Polarity; //La polarité actuelle de notre bloc de fer

    float m_MagRadius; //Le rayon du champ magnetique emis par notre bloc
    Transform m_MagCenter; //La position de notre bloc, et/ou le centre du champ magnétique
    public LayerMask m_CharacterLayer; //Les layers dans lesquels les personnages se trouvent, pour savoir si la collision qu'on détecte est la bonne
    AHumanoid character; //Un mob (pour les collisions)
    float m_MagForce; //La puissance du champ magnétique
    Vector2 m_MagMove; //Le mouvement résultant de l'attraction/répulsion de notre bloc

    public void Interaction()
    {
        //Rien, ce bloc est permanent
    }

    public bool IsNegative()
    {
        return m_Polarity.Equals('n');
    }

    public bool IsPositive()
    {
        return m_Polarity.Equals('p');
    }

    public void MagField()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_MagCenter.position, m_MagRadius, m_CharacterLayer);
        foreach (Collider2D collider in colliders) if (collider.gameObject != gameObject)
            {
                character = collider.GetComponent<AHumanoid>();
                if(character != null)
                {
                    //Reset la gravité pour quand le mob tombe dessus
                    character.ResetGravity();

                    if (IsPositive()) //Si notre bloc est positif...
                    {
                        if (character.IsNegative()) m_MagMove = (m_MagCenter.position - character.transform.position) * m_MagForce * Time.fixedDeltaTime; //... et le personnage negatif, on l'attire
                        else m_MagMove = (character.transform.position - m_MagCenter.position) * m_MagForce * Time.fixedDeltaTime; //... et le personnage positif, on le repousse
                    }
                    else
                    {
                        if (character.IsPositive()) m_MagMove = (m_MagCenter.position - character.transform.position) * m_MagForce * Time.fixedDeltaTime;
                        else m_MagMove = (character.transform.position - m_MagCenter.position) * m_MagForce * Time.fixedDeltaTime;
                    }

                    //On applique le mouvement résultant sur notre mob
                    character.ForceMove(m_MagMove);
                }
            }
    }

    public void PolaritySwitch()
    {
        if (m_Polarity.Equals('p')) m_Polarity = 'n';
        else m_Polarity = 'p';
    }

    /* On donne une polarité de base à notre bloc
     * On ajuste les données de notre champ magnétique
     */
    void Awake()
    {
        m_Polarity = 'n';
        m_MagRadius = 3f;
        m_MagCenter = gameObject.transform;
        m_MagForce = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MagField();
    }
}
