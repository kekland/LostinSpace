using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour {

    int energy;
    public int maxEnergy;

    public int Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
            OnEnergyChange();
        }
    }
    public Image[] energyImg;
    void Awake()
    {
        energy = maxEnergy;
        for(int i = 1; i <= 5; i++)
        {
            energyImg[i-1] = GameObject.Find("Energy" + i).GetComponent<Image>();
        }
    }

    void OnEnergyChange()
    {
        for(int i = 0; i <= 4; i++)
        {
            if(energy >= i)
            {
                energyImg[i].color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                energyImg[i].color = new Color(1, 1, 1, 0.5f);
            }
        }
    }
}
