using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    public float currentHealth; // Current health
    public Slider healthSlider; // Reference to the health slider UI
    public bool isCatapult;
    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        UpdateHealthSlider(); // Update the health slider UI
    }


    public void TakeDamage(float amount, Animator animator)
    {
        currentHealth -= amount; // Reduce current health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp to prevent negative health
        UpdateHealthSlider(); // Update the health slider
        animator.Play("Hit");

        if (currentHealth <= 0)
        {
                Manager.instance.CallummaryMenu(!isCatapult);
                Destroy(this.gameObject);
        }

    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth; // Update health slider value
        }
    }

  
}
