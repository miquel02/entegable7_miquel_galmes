using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable publica per determinar l'estat del joc
    public bool GameOver;

    //Variables per aplicar la força vertical al player
    private Rigidbody playerRigidbody;
    [SerializeField] private float jumpForce = 400f;
    public float gravityModifier = 1f;

    //Variables que determinen el limit superior
    private float maxHeigth = 13.5f;
    private float minHeight = 0;

    //Variables per activar el sistema de particules
    public ParticleSystem explosionParticleSystem;
    public ParticleSystem fireworksParticleSystem;

    //Contador de monedes
    private int monedasRecolectadas = 0;

    //Variables per activar el so
    private AudioSource playerAudioSource;
    private AudioSource explosionAudioSource;
    private AudioSource moneyAudioSource;
    private AudioSource cameraAudioSource;
    public AudioClip boingClip;
    public AudioClip blipClip;
    public AudioClip boomClip;

    
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRigidbody = GetComponent<Rigidbody>();
    }
   
    void Update()
    {     
        //Feim que el player rebi una força per amunt quan presional l'espai
        if (Input.GetKeyDown(KeyCode.Space) && !GameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //Actival l'audio quan bota
            playerAudioSource.PlayOneShot(boingClip, 1);
            cameraAudioSource.volume = 0.01f;
        }

        //Feim que si el player passa del limit superior rebi una força cap avall
        if (transform.position.y > maxHeigth && !GameOver)
        {
            transform.position = new Vector3(transform.position.x, maxHeigth, transform.position.z);
            playerRigidbody.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);

        }        
       
    }

    //Funcio per determinar que passa quan colisionam amb un obstacle
    private void OnCollisionEnter(Collision otherCollider)
    {
        //Aquestes condicions nomes passaran si el GameOver = true
        if (!GameOver)
        {
            //Si colisiona amb el terra el GameOver = true
            if (otherCollider.gameObject.CompareTag("Ground"))
            {
                GameOver = true;
            }
            
            //Si colisionam amb una bomba el GameOver = true, apareixen particules i sona una explosió
            if (otherCollider.gameObject.CompareTag("Bomb"))
            {
                Destroy(otherCollider.gameObject);
                explosionParticleSystem.Play();
                Instantiate(explosionParticleSystem, transform.position, explosionParticleSystem.transform.rotation);
                GameOver = true;
                explosionAudioSource.PlayOneShot(boomClip, 1);
                cameraAudioSource.volume = 0.01f;
            }

            //Si colisionam amb una moneda aumenta el contador de monedes, apareixen particules i sona un so
            if (otherCollider.gameObject.CompareTag("Money"))
            {
                Destroy(otherCollider.gameObject);
                monedasRecolectadas++;
                Debug.Log(monedasRecolectadas);
                fireworksParticleSystem.Play();
                Instantiate(fireworksParticleSystem, transform.position, explosionParticleSystem.transform.rotation);
                moneyAudioSource.PlayOneShot(blipClip, 1);
                cameraAudioSource.volume = 0.01f;
            }

        }

    }
}
