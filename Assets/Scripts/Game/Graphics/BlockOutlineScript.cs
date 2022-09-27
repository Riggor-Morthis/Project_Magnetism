using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOutlineScript : MonoBehaviour
{
   
    public IronBlock m_Block;  //Le bloc dont on veut changer l'outline
    Color m_CNeg, m_CPos; //La couleur représentant le positif, la couleur représentant le négatif

    // Start is called before the first frame update
    void Awake()
    {
        //character = gameObject.GetComponent<AHumanoid>();
        m_CNeg = new Color(0f, 0f, 1f, 1f);
        m_CPos = new Color(1f, 0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Block.IsPositive()) gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", m_CPos);
        else gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", m_CNeg);
    }
}
