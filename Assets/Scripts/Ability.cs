using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

    [Header("This ability attribute, what it does : ")]
    [SerializeField]
    protected int Action;

    public void OnClick()
    {
        GameObject.Find("GamePlayer").GetComponent<PlayerAction>().DoAction(Action);
    }
}
