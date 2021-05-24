using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    public enum PurchaseType { vipmember, diamond55, diamond165, diamond360, diamond655};
    public PurchaseType purchaseType;

    public void ClickPurchasedButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.vipmember:
                IAPManager.instance.BuyVIPMember();
                break;

            case PurchaseType.diamond55:
                IAPManager.instance.BuyDiamond55();
                break;

            case PurchaseType.diamond165:
                IAPManager.instance.BuyDiamond165();
                break;

            case PurchaseType.diamond360:
                IAPManager.instance.BuyDiamond360();
                break;
            case PurchaseType.diamond655:
                IAPManager.instance.BuyDiamond655();
                break;
        }
    }
}
