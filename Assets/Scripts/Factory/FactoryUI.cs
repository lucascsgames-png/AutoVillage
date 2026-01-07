using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactoryUI : MonoBehaviour
{
    [SerializeField] [ColorUsage(true, true)] private Color onColor, offColor;
    [SerializeField] private GameObject root;
    [SerializeField] private Image statusImage;
    [SerializeField] private Image resourceImage;
    [SerializeField] private TMP_Text productionRateText;
    [SerializeField] private TMP_Text storageText;

    private IFactory current;


    public void Start() =>
        SelectionManager.OnSelectionChanged += UnitSelected;

    private void OnDestroy() => 
        SelectionManager.OnSelectionChanged -= UnitSelected;


    private void Show() => root.SetActive(true);
    private void Hide() => root.SetActive(false);


    private void UnitSelected(Unit unit)
    {
       if(current != null) current.Updated -= Fill;

        if (unit != null && unit.Factory != null)
        {
            
            current = unit.Factory;
            current.Updated += Fill;

            Fill(current);
            Show();
        }else Hide();
        
    }

    public void Fill(IFactory factory)
    {
        resourceImage.sprite = factory.ResourceData.resourceSprite;
        productionRateText.text = $"+{(factory.Working ? factory.ProductionRate : 0)} min";
        storageText.text = $"{(int) factory.CurrentStorage} / {factory.CapacityStorage}";
        statusImage.color = factory.Working ? onColor : offColor;
    }

}
