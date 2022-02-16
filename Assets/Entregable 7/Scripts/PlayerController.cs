using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool GameOver;

    private int monedasRecolectadas = 0;
    private Rigidbody playerRigidbody;
    [SerializeField] private float jumpForce = 400f;
    public float gravityModifier = 1f;

    private float maxHeigth = 13.5f;
    private float minHeight = 0;

    public ParticleSystem explosionParticleSystem;
    public ParticleSystem fireworksParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        
           
        if (Input.GetKeyDown(KeyCode.Space) && !GameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

        if (transform.position.y > maxHeigth && !GameOver)
        {
            transform.position = new Vector3(transform.position.x, maxHeigth, transform.position.z);
            playerRigidbody.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);

        }
        
       

    }


    private void OnCollisionEnter(Collision otherCollider)
    {

        if (!GameOver)
        {
            //etiquetas para diferenciar colisiones
            if (otherCollider.gameObject.CompareTag("Ground"))
            {
                GameOver = true;
                //Activamos sistema de particulas de tierra
                

            }

            
            if (otherCollider.gameObject.CompareTag("Bomb"))
            {
                Destroy(otherCollider.gameObject);
                explosionParticleSystem.Play();
                Instantiate(explosionParticleSystem, transform.position, explosionParticleSystem.transform.rotation);
                GameOver = true;

            }

            if (otherCollider.gameObject.CompareTag("Money"))
            {
                Destroy(otherCollider.gameObject);
                monedasRecolectadas++;
                Debug.Log(monedasRecolectadas);
                fireworksParticleSystem.Play();
                Instantiate(fireworksParticleSystem, transform.position, explosionParticleSystem.transform.rotation);

            }

        }

    }
}
