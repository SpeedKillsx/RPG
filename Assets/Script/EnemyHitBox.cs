using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    // Damage
    public int damage;
    public int pushForce;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
        base.OnCollide(coll);
    }
}
