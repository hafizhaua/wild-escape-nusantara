using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField]
    private Image _Cooldown1ForegroundImage;
    [SerializeField]
    private Image _Cooldown2ForegroundImage;
    public void UpdateCooldown()
    {
        RogerSkill rogerSkill = FindObjectOfType<RogerSkill>(); // Or obtain the reference in some other way
        _Cooldown1ForegroundImage.fillAmount = rogerSkill.remainingCooldownPercentage.pounce;
        _Cooldown2ForegroundImage.fillAmount = rogerSkill.remainingCooldownPercentage.roar;
    }
}
