//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Xml.Serialization;
using System.IO;

namespace AssemblyCSharp
{
	public class XMLHandler
	{
		public static Player LoadCharacter(string fileName)
		{
			// Load from File
			XmlSerializer serializer = new XmlSerializer(typeof(Player));
			using(var stream = new FileStream(@"GameData\" + fileName + ".xml", FileMode.Open))
			{
				return serializer.Deserialize(stream) as Player;
			}
		}

		public static void SaveCharacter(Player charToSave)
		{
			if(!Directory.Exists(@"GameData\"))
			{
				Directory.CreateDirectory(@"GameData\");
			}			 

			XmlSerializer serializer = new XmlSerializer(typeof(Player));		
			using(var stream = new FileStream(@"GameData\" + charToSave.Name + ".xml", FileMode.Create))
			{
				serializer.Serialize(stream, charToSave);
			}
		}
	}
}
