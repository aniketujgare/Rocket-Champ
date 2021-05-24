using System;
using UnityEngine;
using UnityEngine.Purchasing;


public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string vipMember = "vip_member";
    private string diamond55 = "diamond_55";
    private string diamond165 = "diamond_165";
    private string diamond360 = "diamond_360";
    private string diamond655 = "diamond_655";

    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(vipMember, ProductType.NonConsumable);
        builder.AddProduct(diamond55, ProductType.Consumable);
        builder.AddProduct(diamond165, ProductType.Consumable);
        builder.AddProduct(diamond360, ProductType.Consumable);
        builder.AddProduct(diamond655, ProductType.Consumable);


        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void BuyVIPMember()
    {
        BuyProductID(vipMember);
    }
    public void BuyDiamond55()
    {
        BuyProductID(diamond55);
    }
    public void BuyDiamond165()
    {
        BuyProductID(diamond165);
    }
    public void BuyDiamond360()
    {
        BuyProductID(diamond360);
    }
    public void BuyDiamond655()
    {
        BuyProductID(diamond655);
    }


    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, vipMember, StringComparison.Ordinal))
        {
            Debug.Log("VIP Member Concidered");
            SaveManager.Instance.state.diamond += 1010;
            SaveManager.Instance.state.isvipMember = true;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, diamond55, StringComparison.Ordinal))
        {
            Debug.Log("Give the player 55 diamonds");
            SaveManager.Instance.state.diamond += 55;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, diamond165, StringComparison.Ordinal))
        {
            Debug.Log("Give the player 165 diamonds");
            SaveManager.Instance.state.diamond += 165;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, diamond360, StringComparison.Ordinal))
        {
            Debug.Log("Give the player 360 diamonds");
            SaveManager.Instance.state.diamond += 360;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, diamond655, StringComparison.Ordinal))
        {
            Debug.Log("Give the player 665 diamonds");
            SaveManager.Instance.state.diamond += 655;
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}