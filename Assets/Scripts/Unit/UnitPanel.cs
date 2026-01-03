using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour
{
    [SerializeField] GameObject root;

    [SerializeField] Image unitImage;
    [SerializeField] TMP_Text unitNameText;
    [SerializeField] TMP_Text unitDescriptionText;


    private void Start()
    {
        SelectionManager.OnSelectionChanged += SetUnit;
    }

    private void OnDestroy()
    {
        SelectionManager.OnSelectionChanged -= SetUnit;
    }

    private void SetUnit(Unit unit)
    {
        if (unit == null)
        {
            Hide();
            return;
        }

        Fill(unit.Data);
        Show();
    }

    public void Fill(UnitData data)
    {
        unitImage.sprite = data.unitSprite;
        unitNameText.text = data.unitName;
        unitDescriptionText.text = data.unitDescription;
    }

    public void Show() => root.SetActive(true);
    public void Hide() => root.SetActive(false);
    
}
