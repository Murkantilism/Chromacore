using UnityEngine;
using System.Collections;

public class DownloadObbExample : MonoBehaviour 
{
	private string expPath;
	private string logtxt;
	private bool alreadyLogged = false;
	private string nextScene = "MainMenu";
	private bool downloadStarted;
	
	public Texture2D background;
	public GUISkin mySkin;
	
	void log( string t )
	{
		logtxt += t + "\n";
		print("MYLOG " + t);
	}
	void OnGUI()
	{
		GUI.skin = mySkin;
		GUI.DrawTexture(new Rect(0,0,background.width,background.height),background);
		
		if (!GooglePlayDownloader.RunningOnAndroid())
		{
			GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Use GooglePlayDownloader only on Android device!");
			return;
		}
		
		expPath = GooglePlayDownloader.GetExpansionFilePath();
		if (expPath == null)
		{
			GUI.Label(new Rect(10, 10, Screen.width-10, 20), "External storage is not available!");
		}
		else
		{
			string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
			string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);
			if( alreadyLogged == false )
			{
				alreadyLogged = true;
				log( "expPath = "  + expPath );
				log( "Main = "  + mainPath );
				log( "Main = " + mainPath.Substring(expPath.Length));
				
				if (mainPath != null)
					StartCoroutine(loadLevel());
				
			}
			//GUI.Label(new Rect(10, 10, Screen.width-10, Screen.height-10), logtxt );
			
			if (mainPath == null)
			{
				GUI.Label(new Rect(Screen.width-600, Screen.height-230, 430, 60), "The game needs to download 200MB of game content. It's recommanded to use WIFI connexion.");
				if (GUI.Button(new Rect(Screen.width-500, Screen.height-170, 250, 60), "Start Download !"))
				{
					GooglePlayDownloader.FetchOBB();
					StartCoroutine(loadLevel());
				}
			}
			
		}
		
	}
	protected IEnumerator loadLevel() 
	{ 
		string mainPath;
		do
		{
			yield return new WaitForSeconds(0.5f);
			mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);	
			log("waiting mainPath "+mainPath);
		}
		while( mainPath == null);
		
		if( downloadStarted == false )
		{
			downloadStarted = true;
			
			string uri = "file://" + mainPath;
			log("downloading " + uri);
			WWW www = WWW.LoadFromCacheOrDownload(uri , 0);		
			
			// Wait for download to complete
			yield return www;
			
			if (www.error != null)
			{
				log ("wwww error " + www.error);
			}
			else
			{
				Application.LoadLevel(nextScene);
			}
		}
	}
	
}