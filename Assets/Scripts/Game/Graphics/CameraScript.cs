using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform m_Player; //Le transform (position) de l'objet sur lequel on est attaché

    private Vector3 m_MinCoord, m_MaxCoord; //Les coordonnées maximales et minimales pour notre caméra
    private Vector3 m_CameraCoord; //Utiliser pour calculer la position de la caméra
    private const float m_BaseZ = -10f; //Le z de la caméra doit rester à -10

    /* On ajuste les positions maximales et minimales de la caméra
     * (différentes pour chaque tableau, faudra réfléchir à comment faire)
     */
    private void Awake()
    {
        m_MinCoord = new Vector2(-13.5f, -2.5f);
        m_MaxCoord = new Vector2(13.5f, 2.5f);
    }

    /* Appelée sur chaque frame
     */
    void Update()
    {
        Follow();
    }

    /* Assure que la caméra suit le joueur sans sortir du niveau
     */
    private void Follow()
    {
        //Récupère les coordonnées du joueur
        m_CameraCoord = m_Player.position;
        //Met x et y dans les bonnes bornes (on touche pas à Z en 2D)
        m_CameraCoord.x = Mathf.Clamp(m_CameraCoord.x, m_MinCoord.x, m_MaxCoord.x);
        m_CameraCoord.y = Mathf.Clamp(m_CameraCoord.y, m_MinCoord.y, m_MaxCoord.y);
        //Mettre z à la bonne valeur
        m_CameraCoord.z = m_BaseZ;
        //On applique nos nouvelles (correctes) coordonnées
        transform.position = m_CameraCoord;
    }
}
