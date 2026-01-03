using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] Transform slotsParent;

    private Slot[] slots;

    private void Awake()
    {
        int count = slotsParent.childCount;

        slots = new Slot[count];

        for(int i = 0; i < count; i++)
        {
            Image iconImage = slotsParent.GetChild(i).GetChild(0).GetComponent<Image>();
            TMP_Text amountText = slotsParent.GetChild(i).GetChild(1).GetComponent<TMP_Text>();
            GameObject root = slotsParent.GetChild(i).gameObject;
          
            slots[i] = new() { 
                root = root,
                iconImage = iconImage,
                amountText = amountText,
            };
        }

        ResourceManager.Updated += Refresh;
    }

    private void OnDestroy()
    {
        ResourceManager.Updated -= Refresh;
    }

    private void Refresh()
    {
        Fill(ResourceManager.Instance.AllResources);
    }

    private void Fill(ResourceManager.ResourcePack[] resources)
    {
        int count = 0;
        foreach(var slot in slots)
        {
            if (count < resources.Length)
            {
                slot.iconImage.sprite = resources[count].resource.resourceSprite;
                slot.amountText.text = resources[count].amount.ToString();
                slot.root.SetActive(true);
            }
            else
            {
                slot.root.SetActive(false);
            }
            
            count += 1;
        }
    }


    public struct Slot
    {
        public GameObject root;
        public Image iconImage;
        public TMP_Text amountText;
    }
}
