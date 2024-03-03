using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    public float expOnDeath;
    private Player _player;
    public float damage = 5f;
    public Collider _collider;
    
    
    private int currentHealth;
    public Slider healthBarPrefab;
    private Slider healthBar;
    
    
    [SerializeField] private AnimationController animationController;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);

        healthBar.maxValue = health;
        healthBar.value = health;
        
        
    }

    private void Update()
    {
        if (healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.forward * 1.2f);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (healthBar)
        {
            healthBar.value -= damage;
            if (healthBar.value <= 0)
            {
            //    _collider.enabled = false;         zombi ölme animasyonundayken playerı öldürmesin diye
                Destroy(healthBar.gameObject);
            }
            base.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        _player.AddExperience((expOnDeath));
        animationController.SetBoolean("Dead",true); 
        
        base.Die();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            _player.TakeDamage(damage);
            
            Destroy(this);
            
        }
    }
}
