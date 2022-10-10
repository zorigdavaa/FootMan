using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Abilities : MonoBehaviour
{
    [SerializeField] List<AbilityUI> abilities;
    [SerializeField] Button Skill1;
    [SerializeField] Image skill1Cooldown;
    Button Skill2;
    Image skill2Cooldown;
    Button Skill3;
    Image skill3Cooldown;
    private void OnEnable()
    {
        foreach (Transform item in transform)
        {
            abilities.Add(new AbilityUI
            {
                Button = item.GetComponent<Button>(),
                skill2Cooldown = item.GetChild(2).GetComponent<Image>()
            });
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Skill1.onClick.AddListener(UseSkill);
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].Button.onClick.AddListener(UseSkill);
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (var ability in abilities)
        {
            if (ability.skill2Cooldown.fillAmount > 0)
            {
                // print(skill1Cooldown.fillAmount);
                // skill1Cooldown.fillAmount -= (1 / 5) * Time.deltaTime;
                ability.skill2Cooldown.fillAmount -= 0.2f * Time.deltaTime;
                if (ability.skill2Cooldown.fillAmount <= 0)
                {
                    ability.Button.interactable = true;
                }
            }
        }
        // if (skill1Cooldown.fillAmount > 0)
        // {
        //     print(skill1Cooldown.fillAmount);
        //     // skill1Cooldown.fillAmount -= (1 / 5) * Time.deltaTime;
        //     skill1Cooldown.fillAmount -= 0.2f * Time.deltaTime;
        //     if (skill1Cooldown.fillAmount <= 0)
        //     {
        //         Skill1.interactable = true;
        //     }
        // }
    }

    private void UseSkill()
    {

        // print(EventSystem.current.currentSelectedGameObject.name);

        int number = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        // skill1Cooldown.fillAmount = 1;
        // Skill1.interactable = false;
        abilities[number].Button.interactable = false;
        abilities[number].skill2Cooldown.fillAmount = 1;
        abilities[number].OnSkillUsed?.Invoke(this, EventArgs.Empty);
        // abilities[number].OnSkillUsed.Invoke();
    }
    public List<AbilityUI> GetAbilities()
    {
        return abilities;
    }
    public AbilityUI GetAbilitieByIndex(int index)
    {
        return abilities[index];
    }
}
[System.Serializable]
public class AbilityUI
{

    public Button Button;
    public Image skill2Cooldown;
    // public Action OnSkillUsed;
    public EventHandler OnSkillUsed;
}
