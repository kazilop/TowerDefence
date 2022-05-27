using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SelectionBase]
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 4;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] AudioClip hitEnemySoundFX;
    private int currentScore;

    AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        currentScore = int.Parse(scoreText.text);
    }

    private void OnParticleCollision(GameObject other)
    {
        ProccessHit();
    }

    private void ProccessHit()
    {
        audioSource.PlayOneShot(hitEnemySoundFX);

        hitPoints = hitPoints - 1;
        hitParticle.Play();

        if(hitPoints <= 0)
        {
            DestroyEnemy(deathParticle);
        }
    }

    public void DestroyEnemy(ParticleSystem fx)
    {
        currentScore++;
        scoreText.text = currentScore.ToString();

        var deathFX = Instantiate(fx, transform.position, Quaternion.identity);
        deathFX.Play();
        Destroy(gameObject);
    }
}
