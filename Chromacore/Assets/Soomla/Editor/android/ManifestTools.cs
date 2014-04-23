using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;

namespace UnityEditor.SoomlaEditor
{
	public class ManifestTools
    {

        public static void GenerateManifest()
        {
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

            // only copy over a fresh copy of the AndroidManifest if one does not exist
            if (!File.Exists(outputFile))
            {
                var inputFile = Path.Combine(EditorApplication.applicationContentsPath, "PlaybackEngines/androidplayer/AndroidManifest.xml");
                File.Copy(inputFile, outputFile);
            }
            UpdateManifest(outputFile);
        }

		public static void UpdateManifest(string fullPath) {
			XmlDocument doc = new XmlDocument();
			doc.Load(fullPath);
			
			if (doc == null)
			{
				Debug.LogError("Couldn't load " + fullPath);
				return;
			}

			XmlNode manNode = FindChildNode(doc, "manifest");
			XmlNode applicationNode = FindChildNode(manNode, "application");
			
			if (applicationNode == null) {
				Debug.LogError("Error parsing " + fullPath);
				return;
			}
			
			string ns = manNode.GetNamespaceOfPrefix("android");

			findOrPrependElement("uses-permission", "name", ns, "com.android.vending.BILLING", manNode, doc);
			findOrPrependElement("uses-permission", "name", ns, "android.permission.INTERNET", manNode, doc);

			ns = applicationNode.GetNamespaceOfPrefix("android");

			XmlElement applicationElement = FindChildElement(manNode, "application");
			applicationElement.SetAttribute("name", ns, "com.soomla.store.SoomlaApp");

			findOrAppendIabActivity(ns, applicationNode, doc);

			doc.Save(fullPath);
		}

		private static void findOrAppendIabActivity(string ns, XmlNode applicationNode, XmlDocument doc) {
			XmlElement e = FindElementForNameWithNamespace("activity", "name", ns, "com.soomla.store.StoreController$IabActivity", applicationNode);
			if (e == null)
			{
				e = doc.CreateElement("activity");
				e.SetAttribute("name", ns, "com.soomla.store.StoreController$IabActivity");
				e.SetAttribute("theme", ns, "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
				e.InnerText = "\n    ";
				applicationNode.AppendChild(e);
			}
		}

		private static void findOrPrependElement(string name, string androidName, string ns, string value, XmlNode parent, XmlDocument doc) {
			XmlElement e = FindElementForNameWithNamespace(name, androidName, ns, value, parent);
			if (e == null)
			{
				e = doc.CreateElement(name);
				e.SetAttribute(androidName, ns, value);
				parent.PrependChild(e);
			}
		}

		private static XmlNode FindChildNode(XmlNode parent, string name)
		{
			XmlNode curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name))
				{
					return curr;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		
		private static XmlElement FindChildElement(XmlNode parent, string name)
		{
			XmlNode curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name))
				{
					return curr as XmlElement;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		
//		private static XmlElement FindMainActivityNode(XmlNode parent)
//		{
//			XmlNode curr = parent.FirstChild;
//			while (curr != null)
//			{
//				if (curr.Name.Equals("activity") && 
//				    (curr.FirstChild != null && curr.FirstChild.Name.Equals("intent-filter")) &&
//				    (curr.FirstChild.FirstChild != null && curr.FirstChild.FirstChild.Name.Equals("action")))
//				{
//					return curr as XmlElement;
//				}
//				curr = curr.NextSibling;
//			}
//			return null;
//		}
		
		private static XmlElement FindElementForNameWithNamespace(string name, string androidName, string ns, string value, XmlNode parent)
		{
			var curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
				{
					return curr as XmlElement;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
	}
}