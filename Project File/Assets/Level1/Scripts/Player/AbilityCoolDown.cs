using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour
{

    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public Text coolDownTextDisplay;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject weaponHolder;
    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    public Player player;

    bool coolDownComplete;


    // Use this for initialization
    void Start()
    {
        Initialize(ability, weaponHolder);
    }

    public void Initialize(Ability _selectedAbility, GameObject _weaponHolder)
    {
        ability = _selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.aSprite;
        darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.aBaseCooldown;
        ability.Initialize(weaponHolder);
        AbilityReady();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextReadyTime)
        {
            AbilityReady();
            if (Input.GetButtonDown("Fire1"))
            {
                ButtonTriggered();
                player.TimeToShootwMouse();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                ButtonTriggered();
                player.TimeToShootwController();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCD = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

        abilitySource.clip = ability.audioClip;
        abilitySource.Play();
        ability.TriggerAbility();
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = (false);
        darkMask.enabled = false;
    }
}
