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


namespace AssemblyCSharp
{
	public enum TypeOfAttributes
	{
		Stat,
		Skill
	}

	public enum AttributeTypes
	{
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma,

		ClothArmor,
		LightArmor,
		MediumArmor,
		HeavyArmour,
		Swords,
		Daggers,
		Staff,
		Mace,
		Hammer,
		Wand,
		Orb,
		Sling,
		Bow,
		CrossBow,
		HideInShadows,
		Tumble,
		LockPick,
		PickPocket,
		CastSpell,
	}

	[XmlRoot("Attribute")]
	public class Attribute
	{
		[XmlElement("AttributeName")]
		public string AttributeName
		{
			get;
			set;
		}

		[XmlElement("AttributeDescription")]
		public string AttributeDescription
		{
			get;
			set;
		}

		[XmlElement("TypeOfAttribute")]
		public TypeOfAttributes TypeOfAttribute
		{
			get;
			set;
		}

		[XmlElement("AttributeType")]
		public AttributeTypes AttributeType
		{
			get;
			set;
		}

		[XmlElement("AttributeLevel")]
		public int AttributeLevel
		{
			get;
			set;
		}

		public Attribute ()
		{
		}

		public Attribute (AttributeTypes type, int level)
		{
			AttributeType = type;
			AttributeLevel = level;
			SetupTypeOfAttribute();
			SetStatName();
		}

		/// <summary>
		/// Change the specified level.
		/// </summary>
		/// <param name="level">A positive or negative number.</param>
		public void Change(int level)
		{
			AttributeLevel += level;
		}

		/// <summary>
		/// Increase the specified level.
		/// </summary>
		/// <param name="level">A positive number.</param>
		public void Increase(int level)
		{
			AttributeLevel += Math.Abs(level);
		}

		/// <summary>
		/// Decrease the specified level.
		/// </summary>
		/// <param name="level">A positive number.</param>
		public void Decrease(int level)
		{
			AttributeLevel -= Math.Abs(level);
		}

		private void SetupTypeOfAttribute()
		{
			switch(AttributeType)
			{
				default:
					TypeOfAttribute = TypeOfAttributes.Skill;
					break;
				case AttributeTypes.Strength:
				case AttributeTypes.Dexterity:
				case AttributeTypes.Constitution:
				case AttributeTypes.Intelligence:
				case AttributeTypes.Wisdom:
				case AttributeTypes.Charisma:
					TypeOfAttribute = TypeOfAttributes.Stat;
					break;
			}
		}

		private void SetStatName()
		{
			switch(AttributeType)
			{
			case AttributeTypes.Strength:
				AttributeName = "Strength";
				AttributeDescription = "Overall physical strength, muscle and physical power";
				break;
			case AttributeTypes.Dexterity:
				AttributeName = "Dexterity";
				AttributeDescription = "Hand-eye coordination, agility, reflexes, and balance";
				break;
			case AttributeTypes.Constitution:
				AttributeName = "Constitution";
				AttributeDescription = "Health and stamina";
				break;
			case AttributeTypes.Intelligence:
				AttributeName = "Intelligence";
				AttributeDescription = "Learning and reasoning";
				break;
			case AttributeTypes.Wisdom:
				AttributeName = "Wisdom";
				AttributeDescription = "Willpower, common sense, perception, and intuition";
				break;
			case AttributeTypes.Charisma:
				AttributeName = "Charisma";
				AttributeDescription = "Force of personality, persuasiveness, personal magnetism, ability to lead, and physical attractiveness";
				break;
			case AttributeTypes.ClothArmor:
				AttributeName = "Wear Cloth Armour";
				AttributeDescription = "Allows the user to wear cloth armors like robes";
				break;
			case AttributeTypes.LightArmor:
				AttributeName = "Wear Light Armor";
				AttributeDescription = "Allows the user to wear light armors like leather armors";
				break;
			case AttributeTypes.MediumArmor:
				AttributeName = "Wear Medium Armor";
				AttributeDescription = "Allows the user to wear medium armors like chain mail";
				break;
			case AttributeTypes.HeavyArmour:
				AttributeName = "Wear Heavy Armor";
				AttributeDescription = "Allows the user to wear heavy armors like platemail";
				break;
			case AttributeTypes.Swords:
				AttributeName = "Swords";
				AttributeDescription = "Can weild swords single or two handed";
				break;
			case AttributeTypes.Daggers:
				AttributeName = "Daggers";
				AttributeDescription = "Can weild daggers";
				break;
			case AttributeTypes.Staff:
				AttributeName = "Staffs";
				AttributeDescription = "Can weild staffs full and quater";
				break;
			case AttributeTypes.Mace:
				AttributeName = "Maces";
				AttributeDescription = "Can weild maces single or two handed";
				break;
			case AttributeTypes.Hammer:
				AttributeName = "Hammers";
				AttributeDescription = "Can weild hammers single or two handed";
				break;
			case AttributeTypes.Wand:
				AttributeName = "Wands";
				AttributeDescription = "Can use wands to cast spells";
				break;
			case AttributeTypes.Orb:
				AttributeName = "Orbs";
				AttributeDescription = "Can use orbs to cast spells";
				break;
			case AttributeTypes.Sling:
				AttributeName = "Slings";
				AttributeDescription = "Can use slings to hurl rocks";
				break;
			case AttributeTypes.Bow:
				AttributeName = "Bows";
				AttributeDescription = "Can use short and long bows";
				break;
			case AttributeTypes.CrossBow:
				AttributeName = "CrossBows";
				AttributeDescription = "Can use crossbows";
				break;
			case AttributeTypes.HideInShadows:
				AttributeName = "Hide in shadows";
				AttributeDescription = "Allows the user to conceal themselves in the shadows and hide";
				break;
			case AttributeTypes.Tumble:
				AttributeName = "Tumble";
				AttributeDescription = "Allows the user to dodge incoming attacks";
				break;
			case AttributeTypes.LockPick:
				AttributeName = "Pick locks";
				AttributeDescription = "Allows the user to pick locks";
				break;
			case AttributeTypes.PickPocket:
				AttributeName = "Pick pockets";
				AttributeDescription = "Allows the user to skillfully pick items from other peoples pockets";
				break;
			case AttributeTypes.CastSpell:
				AttributeName = "Cast spells";
				AttributeDescription = "Allow the casting of spells";
				break;
			}
		}

	}
}
