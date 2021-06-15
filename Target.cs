using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;

    private int minSpeed = 12;
    private int maxSpeed = 16;
    private int maxTorque = 10;
    private int xRange = 4;
    private int ySpawnPos = -6;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(GenUpForce(), ForceMode.Impulse);
        targetRb.AddTorque(GenNum(-maxTorque, maxTorque), GenNum(-maxTorque, maxTorque), GenNum(-maxTorque, maxTorque), ForceMode.Impulse);
        transform.position = new Vector3(GenNum(-xRange, xRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenUpForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private int GenNum(int min, int max)
    {
        return Random.Range(min, max);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.lives--;
            gameManager.UpdateLives();
            if (gameManager.lives <= 0)
            {
                gameManager.GameOver();
            }
        }
    }
}
