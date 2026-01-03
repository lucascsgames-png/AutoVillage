using UnityEngine;

[CreateAssetMenu(menuName = "meus objetos/unit")]
public class UnitData : ScriptableObject
{
    [SerializeField] public string unitName;
    [SerializeField] [TextArea] public string unitDescription;
    [SerializeField] public Sprite unitSprite;
}
