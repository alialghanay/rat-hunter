using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    [Header("Health Bar")]

    public float chipSpeed = 2f;

    public Image frontHealthBar;
    public Image backHealthBar;

    [SerializeField]
    private TextMeshProUGUI promptText;

    [Header("Damge Overlay")]
    public Image overlay;
    public float duration;
    public float feedSpeed;
    private float durationTimer;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip damageSound;
    
    private void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        UpdateHealthUI();
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
         if(overlay.color.a > 0){
            durationTimer += Time.deltaTime;
            if(health < 30) return;
            if(durationTimer > duration){
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * feedSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
         if (health <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHealthUI()
    {
        float healthFraction = health / maxHealth;

        frontHealthBar.fillAmount = healthFraction;
        promptText.text = health + " / " + maxHealth; 

        StartCoroutine(LerpHealthBar(backHealthBar, healthFraction));
    }

    private IEnumerator LerpHealthBar(Image healthBar, float targetFillAmount)
    {
        float currentFill = healthBar.fillAmount;
        float timer = 0f;

        if(healthBar.fillAmount > targetFillAmount) healthBar.color = Color.red;
        else healthBar.color = Color.green;

        while (timer < chipSpeed)
        {
            timer += Time.deltaTime;
            float percentComplete = timer / chipSpeed;
            healthBar.fillAmount = Mathf.Lerp(currentFill, targetFillAmount, percentComplete);
            yield return null;
        }

        healthBar.fillAmount = targetFillAmount; 
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        durationTimer = 0;
        PlayDamageSound();
        UpdateHealthUI();
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthUI();
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
