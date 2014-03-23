Google Play Downloader
----------------------
This package is an adaption of the Google Play market_downloader library, for use with Unity Android (as a plugin).

This plugin does NOT solve splitting up a >50MB .apk into .obb (through asset bundles or similar techiniques).
It merely handles the downloading of .obb files attached to a published .apk, on devices that don't support automatic downloading.

To try it out
-------------
1) Open Unity, create a new project.
2) Import the Google Play Downloader package (Assets -> Import Package -> Custom Package)
3) Attach the DownloadObbExample.cs to the Main Camera
4) Open GooglePlayDownloader.cs and replace the BASE64_PUBLIC_KEY.
5) Change the Bundle Identifier / Version Code so it matches an application already available on Google Play (that has .obb files attached).
6) Build and Run on your android device.


To rebuild the code
-------------------
1) Make sure you have the JDK and Ant installed
2) Open a Terminal.app / cmd.exe window
3) Change directory to the plugin root
	$> cd {your new project}/Assets/Plugins/Android
4) Update the ant project with the local SDK path
	$> android update project -p .
5) Build the plugin (use 'help' to see a complete list of commands)
	$> ant build

To read data from the .obb
--------------------------

(This is just one way of handling loading of asset bundles from a .obb)

1) Create one or many AssetBundles with the application data (enough to get the .apk below the 50MB limit).
2) Concatenate the generated AssetBundles into a single .obb by ZIP'ing them, using STORE (not DEFLATE).
	$> zip -0 main.obb assetbundles
3) Continue to use WWW.LoadFromCacheOrDownload() as with regular asset bundle loading, but use the path
   to the .obb instead, like this:
	WWW.LoadFromCacheOrDownload(jar:file://" + obbPath + "!/" + originalAssetBundleName);

See also
--------
http://developer.android.com/guide/market/expansion-files.html

