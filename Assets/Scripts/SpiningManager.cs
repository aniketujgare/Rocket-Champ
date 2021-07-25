using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
//using admob;
using UnityEngine.SceneManagement;
//using admob;

public class SpiningManager : MonoBehaviour
{
	int randVal;
	private float timeInterval;
	private bool isCoroutine;
	private int finalAngle;

	public Text winText;
	public Text diamondText;
	public int section;
	float totalAngle;
	public string[] PrizeName;

	[Header("On Play Disable")]
	[SerializeField] GameObject backButton;
	[SerializeField] GameObject startButton;

	[Header("Internet Check")]
	public Transform internetPanel;
	public Transform videoNotLoadPanel;

	public Transform bonusLvlPanel;

	string LevelNo;
	private bool isnetconnect = false;
	private bool f = false;

	// Use this for initialization
	private void Start()
	{

		isCoroutine = true;
		totalAngle = 360 / section;
		backButton.SetActive(true);
		startButton.SetActive(true);

		LevelNo = SceneManager.GetActiveScene().name;
		///Admob.Instance().initSDK(new AdProperties());
		///Admob.Instance().loadRewardedVideo("ca-app-pub-1147416347412616/5655307978");
		/*
		StartCoroutine(checkInternetConnectionCo((isConnected) =>
			{
				//h;andle connection status here
				isnetconnect = true;
			})); 
		*/
		//setting video not loaded panel to disble when spin and win is opened
		videoNotLoadPanel.gameObject.SetActive(false);
		//UpdateDiamondText();
	}

	// Update is called once per frame
	private void Update()
	{
		float xRotate = CrossPlatformInputManager.GetAxis("Horizontal");
		//internetPanel.gameObject.SetActive(false);
		if (Yodo1Ads.instance.isRewardedReady() && xRotate >= 1)
		{
			//videoNotLoadPanel.gameObject.SetActive(false);
			//float xRotate = CrossPlatformInputManager.GetAxis("Horizontal");
			if (isCoroutine)
			{
				StartCoroutine(Spin());
			}
		}
		else if(!Yodo1Ads.instance.isRewardedReady() && xRotate >= 1)
		{
			//video is not ready
			videoNotLoadPanel.gameObject.SetActive(true);
			
		}
	


		//if (!Admob.Instance().isRewardedVideoReady()) { Admob.Instance().loadRewardedVideo("ca-app-pub-3940256099942544/5224354917"); }

		UpdateDiamondText();
	
	}




	private IEnumerator Spin()
	{
		//Flurry Instance
		EventLogExample.Instance.PressedSpinAndWin(LevelNo);

		isCoroutine = false;
		backButton.SetActive(false);
		startButton.SetActive(false);

		randVal = Random.Range(80, 150);

		timeInterval = Time.deltaTime * 2;

		for (int i = 0; i < randVal; i++)
		{

			transform.Rotate(0, 0, (totalAngle / 2)); //Start Rotate 


			//To slow Down Wheel
			if (i > Mathf.RoundToInt(randVal * .2f))//0.2f
				timeInterval = 0.5f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * .5f))//0.2f
				timeInterval = 1f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * .7f))//0.2f
				timeInterval = 1.5f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * .8f))//0.2f
				timeInterval = 2f * Time.deltaTime;
			if (i > Mathf.RoundToInt(randVal * .9f))
				timeInterval = 2.5f * Time.deltaTime;

			yield return new WaitForSeconds(timeInterval);

		}

		if (Mathf.RoundToInt(transform.eulerAngles.z) % totalAngle != 0) //when the indicator stop between 2 numbers,it will add aditional step 
			transform.Rotate(0, 0, totalAngle / 2);

		finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);//round off euler angle of wheel value

		print(finalAngle);

		//Prize check
		for (int i = 0; i < section; i++)
		{

			if (finalAngle == i * totalAngle)
			{
				winText.text = PrizeName[i];
				if (winText.text == "BONUS LEVEL")
				{
					PlayerPrefs.SetInt("BONUSLEVEL", 1);
					PlayerPrefs.Save();
					//Debug.Log("Play Bonus Level");
					//if (Yodo1Ads.instance.isRewardedReady())
					//{
					Yodo1Ads.instance.showRewardedAd();			
					bonusLvlPanel.gameObject.SetActive(true);
					//}
				}
				else if (winText.text == "DIAMONDS X8")
				{
					//Debug.Log("Give DIAMONDS X8");
					PlayerPrefs.SetInt("DIAMONDX8", 1);
					PlayerPrefs.Save();
					//if (Yodo1Ads.instance.isRewardedReady())
					Yodo1Ads.instance.showRewardedAd();
				}
				else if (winText.text == "DIAMONDS X4")
				{
					//Debug.Log("Give DIAMONDS X4");
					PlayerPrefs.SetInt("DIAMONDX4", 1);
					PlayerPrefs.Save();
					//if (Yodo1Ads.instance.isRewardedReady())
					Yodo1Ads.instance.showRewardedAd();
				}
				else if (winText.text == "JACKPOT")
				{
					//Debug.Log("Give JACKPOT");
					PlayerPrefs.SetInt("JACKPOT", 1);
					PlayerPrefs.Save();
					//if (Yodo1Ads.instance.isRewardedReady())
					Yodo1Ads.instance.showRewardedAd();
				}
				//Flurry Event
				EventLogExample.Instance.VideoShown(LevelNo, "Spin");
				//StartCoroutine(RewardCo());
				
			}

		}
		isCoroutine = true;
		backButton.SetActive(true);
		startButton.SetActive(true);
		//UpdateDiamondText();
	}
	private void UpdateDiamondText()
	{
		diamondText.text = SaveManager.Instance.state.diamond.ToString();
	}

	//	Admob.Instance().rewardedVideoEventHandler += onRewardedVideoEvent;
	/*
	void onRewardedVideoEvent()
	{
		
			if (PlayerPrefs.HasKey("BONUSLEVEL"))
			{
			//Reward Bonus Level
				bonusLvlPanel.gameObject.SetActive(true);
				PlayerPrefs.DeleteKey("BONUSLEVEL");

				//Flurry Event
				EventLogExample.Instance.RewardForVideo("BONUS LEVEL");
			}
			else if (PlayerPrefs.HasKey("DIAMONDX8"))
			{
				SaveManager.Instance.state.diamond += 8;
				SaveManager.Instance.Save();
				PlayerPrefs.DeleteKey("DIAMONDX8");

				//Flurry Event
				EventLogExample.Instance.RewardForVideo("8 DIAMONDS");
			}
			else if (PlayerPrefs.HasKey("DIAMONDX4"))
			{
				SaveManager.Instance.state.diamond += 4;
				SaveManager.Instance.Save();
				PlayerPrefs.DeleteKey("DIAMONDX4");

				//Flurry Event
				EventLogExample.Instance.RewardForVideo("4 DIAMONDS");
			}
			else if (PlayerPrefs.HasKey("JACKPOT"))
			{
				SaveManager.Instance.state.diamond += 25;
				SaveManager.Instance.Save();
				PlayerPrefs.DeleteKey("JACKPOT");

				//Flurry Event
				EventLogExample.Instance.RewardForVideo("JACKPOT");
			}
		
	}
	*/

	/*	IEnumerator checkInternetConnectionCo(System.Action <bool> action)
		{
			WWW www = new WWW("http:/google.com");
			yield return www;
			if(www.error != null)
			{
				action(false);
			}
			else
			{
				action(true);
			}
		}*/
	public void PlayBonusLevel()
	{
		int i = Random.Range(252, 256);
		string bonusLvlNo = (i - 252 + 1).ToString();
		EventLogExample.Instance.BonusLevelPlayed("BonusLevel_" + bonusLvlNo);
		Initiate.Fade(i, GameAssets.i.color, 1f);
	}
	/*
	IEnumerator RewardCo()
    {
		yield return new WaitForSeconds(0.5f);
		onRewardedVideoEvent();
	}
	*/
}