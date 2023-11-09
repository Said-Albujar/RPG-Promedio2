
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public float timeBetweenShots1 = 1.0f;
    public float timeBetweenShots2 = 2.5f;
    public int playerLevel = 1;
    public int playerExperience = 0;
    public int experienceToNextLevel = 100;
    [SerializeField] private int baseDamage1 = 5;
    [SerializeField] private int baseDamage2 = 8;
    public float invincibilityTime = 0.5f; 
    private bool isInvincible = false;
    private float invincibilityTimer = 0.0f;

    [Header("Reference")]
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public Transform firePoint1;
    public Transform firePoint2;
    public int shotgunPellets; 
    public float spreadAngle; 

    [Header("UI")]
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExperienceText;
    public string DieScene;

    private float timer1;
    private float timer2;

    public delegate void LevelIncreasedEventHandler(int level);
    public event LevelIncreasedEventHandler OnLevelIncreased;

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0)
            {
                isInvincible = false; 
            }
        }

        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timer1 >= timeBetweenShots1)
        {
            Shoot1();
            timer1 = 0f;
        }

        if (Input.GetButtonDown("Fire2") && timer2 >= timeBetweenShots2)
        {
            Shoot2();
            timer2 = 0f;
        }

        UpdateUI();
    }
    void UpdateUI()
    {
        HealthText.text = "Health: 100/ " + currentHealth;
        LevelText.text = "Level: " + playerLevel;
        ExperienceText.text = "Experience: " + playerExperience + " / " + experienceToNextLevel;
    }

    void Shoot1()
    {
        GameObject bullet = Instantiate(bulletPrefab1, firePoint1.position, firePoint1.rotation);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.damage = baseDamage1; 
    }

    void Shoot2()
    {
        for (int i = 0; i < shotgunPellets; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);
            GameObject bullet = Instantiate(bulletPrefab2, firePoint2.position, firePoint2.rotation * spreadRotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.damage = baseDamage2; 
            bulletComponent.bulletLifetime = 3.0f; 
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die();
            }

            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(DieScene);
    }

    public void GainExperience(int experienceAmount)
    {
        playerExperience += experienceAmount;

        if (playerExperience >= experienceToNextLevel)
        {
            IncreaseLevel(1);
        }
    }

    public void IncreaseLevel(int level)
    {
        playerLevel++;
        playerExperience -= experienceToNextLevel;
        experienceToNextLevel = (int)(experienceToNextLevel * 1.5f);
        currentHealth = maxHealth;

        baseDamage1 += 3;
        baseDamage2 += 4;
        invincibilityTime += 0.2f;

        if (timeBetweenShots1 > 0.1f)
        {
            timeBetweenShots1 -= 0.1f;
        }

        if (timeBetweenShots2 > 0.5f)
        {
            timeBetweenShots2 -= 0.5f;
        }

        OnLevelIncreased?.Invoke(level);
    }
}
