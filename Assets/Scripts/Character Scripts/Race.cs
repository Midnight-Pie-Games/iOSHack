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
using System.Collections.Generic;
using System.Xml.Serialization;


namespace AssemblyCSharp
{
	public enum RaceTypes
	{
		Human,
		Elf,
		Halfling,
		Gnome,
		Dwarf,
		Orc
	}

	[XmlRoot("Race")]
	public class Race
	{
		[XmlArray("attributeChange")]
		public List<Attribute> attributeChange = new List<Attribute>();

		[XmlElement("RaceType")]
		public RaceTypes RaceType
		{
			get;
			set;
		}

		public Race ()
		{
		}

		public Race (RaceTypes raceType)
		{
			switch (raceType)
			{
				default:
				case RaceTypes.Human:
					// Captain Average
					break;
				case RaceTypes.Elf:
					attributeChange.Add(new Attribute(AttributeTypes.Dexterity, 2));
					attributeChange.Add(new Attribute(AttributeTypes.Intelligence, 2));
					attributeChange.Add(new Attribute(AttributeTypes.Constitution, -2));
					attributeChange.Add(new Attribute(AttributeTypes.Charisma, 1));
					break;
				case RaceTypes.Halfling:
					attributeChange.Add(new Attribute(AttributeTypes.Strength, -2));
					attributeChange.Add(new Attribute(AttributeTypes.Dexterity, 2));
					break;
				case RaceTypes.Gnome:
					attributeChange.Add(new Attribute(AttributeTypes.Strength, -2));
					attributeChange.Add(new Attribute(AttributeTypes.Dexterity, 2));
					attributeChange.Add(new Attribute(AttributeTypes.Intelligence, 1));
					break;
				case RaceTypes.Dwarf:
					attributeChange.Add(new Attribute(AttributeTypes.Strength, 2));
					attributeChange.Add(new Attribute(AttributeTypes.Constitution, 2));
					break;
				case RaceTypes.Orc:
					attributeChange.Add(new Attribute(AttributeTypes.Strength, 2));
					attributeChange.Add(new Attribute(AttributeTypes.Charisma, -3));
					break;
			}
		}
	}
}

