using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RaycastAbility")]
public class RaycastAbility : Ability
{
    public int damagePerShot = 20;              // The damage per each bullet
    public float timeBetweenBullets = 2f;     // The time between each bullets
    public float range = 100f;                  // The distance of the gun

    public Color laserColor = Color.white;

    private Player rcShoot;

    public override void Initialize(GameObject go)
    {
        rcShoot = go.GetComponent<Player>();
        //rcShoot.Awake();

        rcShoot.damagePerShot = damagePerShot;
        rcShoot.range = range;
        rcShoot.timeBetweenBullets = timeBetweenBullets;

        rcShoot.gunLine.material = new Material(Shader.Find("Unlit/Color"));
        rcShoot.gunLine.material.color = laserColor;
    }

    public override void TriggerAbility()
    {
        rcShoot.TimeToShootwMouse();
        rcShoot.TimeToShootwController();
    }
}
