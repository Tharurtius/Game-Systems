using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : EnemyMovement
{
    public Material[] wolfSkin = new Material[4];
    new public Renderer renderer;
    public override void Start()
    {
        base.Start();
        difficulty = Random.Range(1, 5);
        attributes[0].maxValue = baseStats[2].value * 5 * difficulty;
        attributes[0].curValue = attributes[0].maxValue;
        baseDamage = baseStats[1].value * 2;
        walkSpeed = baseStats[1].value * 2;
        runSpeed = baseStats[1].value * 3;
        renderer = GetComponentInChildren<Renderer>();
        renderer.material = wolfSkin[difficulty-1];
    }
    public void BiteAttack()
    {
        int critChance = Random.Range(1, 21);
        float critDamage = 0;
        if (critChance >= critAmount)
        {
            critDamage = Random.Range(baseDamage / 2, baseDamage * difficulty);
        }
        Debug.Log(baseDamage * difficulty + critDamage);
        /* player.GetComponent<PlayerHandler>().DamagePlayer(baseDamage*difficulty+critDamage);*/
    }
}
