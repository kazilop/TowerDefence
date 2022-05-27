using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Castle : MonoBehaviour
{
    [SerializeField] int playerLife = 10;
    [SerializeField] TMP_Text lifeText;
    [SerializeField] AudioClip castleDamageSoundFX;

    AudioSource castleDamageSound;

    private void Start()
    {
        castleDamageSound = GetComponent<AudioSource>();
        lifeText.text = playerLife.ToString();
    }
    public void DamageCastle(int damage)
    {
        castleDamageSound.PlayOneShot(castleDamageSoundFX);
        playerLife -= damage;
        lifeText.text = playerLife.ToString();
    }
}
