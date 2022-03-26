using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damagePoint = {1,2,3,4,5,6,7};
    public float[] pushForce = { 2.0f,5.0f, 5.0f, 6.0f, 6.0f, 6.0f, 7.0f};


    //Upgrade section
    public int weaponLevel = 0;
    public  SpriteRenderer spriteRenderer;

  
    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;


    protected override void Start()
    {
        base.Start();
     
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {   // Attack the ennemy with the weapon
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");

    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name != "Player")
            {
                // Create a new damage object

                Damage dmg = new Damage
                {
                    damageAmount = damagePoint[weaponLevel],
                    origin = transform.position,
                    pushForce = pushForce[weaponLevel]
                };

                coll.SendMessage("ReceiveDamage", dmg);

                
                Debug.Log(coll.name);
            }
            
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change the stats %%
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
