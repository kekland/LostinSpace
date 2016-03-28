using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class PlayerAbilities : MonoBehaviour {

    public string PlayerClass = "NEWBIE";
    string[] PlayerClasses = new string[4] { "NEWBIE", "SNIPER", "ASSAULT", "SCOUT"};

    [Header("Experience")]
    public int PlayerLevel = 1;
    public int Experience = 0;
    public int ExperienceMax = 200;

    int EXP_FOR_KILL = 100;
    int EXP_FOR_LOOT = 10;
    int EXP_FOR_CRAFT = 20;
    int EXP_FOR_SMELT = 20;

    [Header("Mesh")]
    public Mesh[] gunMesh;
    public GameObject gun;

    [Header("Skills")]
    public GameObject[] abilities;

    [Header("Standard abilities : ")]
    public GameObject[] StandardAbilities;

    [Header("Assault Level Abilities")]
    public GameObject[] assaultAbilities;

    [Header("Sniper Level Abilities")]
    public GameObject[] sniperAbilities;

    [Header("Scout Level Abilities")]
    public GameObject[] scoutAbilities;

    [Header("Current hero abilities")]
    public List<GameObject> currentAbilities;

    [Header("Other(Temp variables)")]
    public GameObject abilitiesParent;

    public bool haveChosenClass;

    void Awake()
    {
        PlayerLevel = SPlayerPrefs.GetInt("PlayerLevel", 1);
        Experience = SPlayerPrefs.GetInt("PlayerExperience", 0);
        PlayerClass = SPlayerPrefs.GetString("PlayerClass", "NEWBIE");
        haveChosenClass = (SPlayerPrefs.GetInt("CHOSEN_CLASS", 0) > 0);
        switch(PlayerClass)
        {
            case "NEWBIE": gun.GetComponent<MeshFilter>().mesh = gunMesh[0]; break;
            case "ASSAULT": gun.GetComponent<MeshFilter>().mesh = gunMesh[1]; break;
            case "SNIPER": gun.GetComponent<MeshFilter>().mesh = gunMesh[2]; break;
            case "SCOUT": gun.GetComponent<MeshFilter>().mesh = gunMesh[3]; break;
        }
        ExperienceMax = 200 + PlayerLevel * 100;
        DrawAbilities();
    }

    public void GetExperience(string how)
    {
        switch (how)
        {
            case "KILL": Experience += EXP_FOR_KILL; break;
            case "LOOT": Experience += EXP_FOR_LOOT; break;
            case "CRAFT": Experience += EXP_FOR_CRAFT; break;
            case "SMELT": Experience += EXP_FOR_SMELT; break;
        }
        if (Experience >= ExperienceMax)
        {
            Experience = 0;
            PlayerLevel += 1;
            ExperienceMax = 200 + PlayerLevel * 100;
        }
        if (PlayerLevel > 2 && !haveChosenClass)
        {
            PlayerClass = PlayerClasses[Random.Range(0, PlayerClasses.Length)];
            switch (PlayerClass)
            {
                case "NEWBIE": gun.GetComponent<MeshFilter>().mesh = gunMesh[0]; break;
                case "ASSAULT": gun.GetComponent<MeshFilter>().mesh = gunMesh[1]; break;
                case "SNIPER": gun.GetComponent<MeshFilter>().mesh = gunMesh[2]; break;
                case "SCOUT": gun.GetComponent<MeshFilter>().mesh = gunMesh[3]; break;
            }
        }
        DrawAbilities();
    }

    public int AbilityCount;
    void DrawAbilities()
    {
        AbilityCount = 5 + Mathf.Clamp(PlayerLevel, 0, 4);

        for(int i = 1; i <= AbilityCount; i++)
        {
            if (i <= 5)
            {
                GameObject abilityPanel = (GameObject)Instantiate(StandardAbilities[i - 1], Vector2.zero, Quaternion.identity);
                abilityPanel.GetComponent<RectTransform>().SetParent(abilitiesParent.transform, true);
                abilityPanel.GetComponent<RectTransform>().localScale = Vector3.one;
                abilityPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2((AbilityCount / 2 - i) * -100, 0f);
            }
            else if(i > 5 && PlayerClass == "ASSAULT")
            {
                GameObject abilityPanel = (GameObject)Instantiate(assaultAbilities[i - 5], Vector2.zero, Quaternion.identity);
                abilityPanel.GetComponent<RectTransform>().SetParent(abilitiesParent.transform, true);
                abilityPanel.GetComponent<RectTransform>().localScale = Vector3.one;
                abilityPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2((AbilityCount / 2 - i) * -100, 0f);
            }
            else if (i > 5 && PlayerClass == "SCOUT")
            {
                GameObject abilityPanel = (GameObject)Instantiate(scoutAbilities[i - 5], Vector2.zero, Quaternion.identity);
                abilityPanel.GetComponent<RectTransform>().SetParent(abilitiesParent.transform, true);
                abilityPanel.GetComponent<RectTransform>().localScale = Vector3.one;
                abilityPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2((AbilityCount / 2 - i) * -100, 0f);
            }
            else if (i > 5 && PlayerClass == "SNIPER")
            {
                GameObject abilityPanel = (GameObject)Instantiate(sniperAbilities[i - 5], Vector2.zero, Quaternion.identity);
                abilityPanel.GetComponent<RectTransform>().SetParent(abilitiesParent.transform, true);
                abilityPanel.GetComponent<RectTransform>().localScale = Vector3.one;
                abilityPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2((AbilityCount / 2 - i) * -100, 0f);
            }
        }
    }
    void Update()
    {

    }

    public void PickNewClass(int num)
    {
        if(haveChosenClass)
        {
            Debug.LogError("UNHANDLED EXCEPTION! CLASS IS ALREADY PICKED!");
            Debug.Break();
        }
        else
        {

        }
    }
}
