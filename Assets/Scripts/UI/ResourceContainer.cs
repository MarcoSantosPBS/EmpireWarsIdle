using TMPro;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ResourceEnum resource;

    private void Update()
    {
        var amount = Inventory.Instance.GetResourceAmountInInventory(resource);
        text.text = amount.ToString();
    }

}
