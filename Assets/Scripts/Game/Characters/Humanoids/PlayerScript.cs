using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : AHumanoid
{
    private float m_JumpForce = 700f; //Puissance (hauteur) des sauts
    private float m_MovementSpeed = 40f; //Vitesse de déplacement
    private float m_MovementSmoothing = 0.05f; //Le temps qu'il faut pour atteindre notre pleine vitesse
    
    public LayerMask m_GroundLayer; //Quel est le layer pour le sol
    public Transform m_GroundChecker; //L'objet qui nous sert a verifier qu'on est bien au sol
    private float m_GroundCheckerCircleRadius = 0.2f; //Pour verifier qu'on est au sol, on fait un cercle de ce rayon centre sur le groundchecker; en cas de collisions on est au sol
    private bool m_IsGrounded, m_IsJumping; //Stocker le fait qu'on est au sol, qu'on est en train de sauter

    private Vector2 m_Velocity = Vector2.zero; //La vitesse de notre personnage
    private float m_HorizontalInput = 0f; //Notre input droite/gauche (représenter comme allant de -1.0 à 1.0 pour les manettes)

    private bool m_SwithPol; //Savoir si on doit changer la polarité du personnage ou pas

    /* Lorsque le script se réveille
     */
    private new void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_IsGrounded = false;
        m_IsJumping = false;
        m_SwithPol = false;
    }

    /* À chaque frame (utile pour récupérer les inputs)
     */
    private void Update()
    {
        //Recuperer les instructions de mouvement horizontal
        m_HorizontalInput = Input.GetAxisRaw("Horizontal") * m_MovementSpeed;
        //Recuperer l'instruction de saut
        if (Input.GetButtonDown("Jump")) m_IsJumping = true;
        //Récupérer l'instruction de changement de polarité
        if (Input.GetButtonDown("Polarity")) m_SwithPol = true;
    }

    /* Sur son propre timer différent des Updates (utile pour appliquer la physique)
     */
    private void FixedUpdate()
    {
        //Est-ce qu'on est par terre ?
        GroundChecking();
        //On bouge selon les imputs (horizontal et saut)
        Move(m_HorizontalInput * Time.fixedDeltaTime);
        //On regarde ce qui se passe niveau polarité
        PolarityHandler();
    }
    
    /* Gere le mouvement (vertical et horizontal) du personnage
     */
    private void Move(float move)
    {
        Vector2 targetVelocity = new Vector2(move * 10f, m_RigidBody.velocity.y);
        m_RigidBody.velocity = Vector2.SmoothDamp(m_RigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing); //Super pratique, check la doc
        if (m_IsJumping) //On a l'instruction de saut...
        {
            m_IsJumping = false;
            if (m_IsGrounded) //...si on est "au sol", on peut sauter
            {
                m_IsGrounded = false;
                ResetGravity();
                m_RigidBody.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
        
    }

    /* Regarde si on est en contact avec le sol ou non
     * On cast un cercle a l'endroit prevu et on releve tous les colliders qui font collisions
     * Si l'un d'entre eux n'est pas nous, c'est bon on est au sol
     */
    private void GroundChecking()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundChecker.position, m_GroundCheckerCircleRadius, m_GroundLayer);
        foreach (Collider2D collider in colliders) if (collider.gameObject != gameObject) m_IsGrounded = true;
    }

    /* Reçoit l'incstruction de changement de polarité, et applique ce qui doit s'appliquer
     */
    private void PolarityHandler()
    {
        if (m_SwithPol)
        {
            m_SwithPol = false;
            PolaritySwitch();
        }
    }
}
