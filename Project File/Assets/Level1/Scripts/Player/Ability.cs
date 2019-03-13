using UnityEngine;

public abstract class Ability : ScriptableObject {

    public string aName = "New Ability";
    public Sprite aSprite;
    public AudioClip audioClip;
    public float aBaseCooldown = 2f;

    public abstract void Initialize(GameObject go);
    public abstract void TriggerAbility();
}
