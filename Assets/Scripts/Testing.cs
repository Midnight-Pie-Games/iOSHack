using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class Testing : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//ItemWithEffectPlusOne();
		//TestAbilities();
		//TestAbilities2();
		AddTemporaryEffect();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private Player CreateNewCharacter()
	{
		List<Race> races = new List<Race>();
		races.Add(new Race(RaceTypes.Human));
		
		List<CharClass> charClasses = new List<CharClass>();
		charClasses.Add(new CharClass(CharacterClass.Warrior));
		
		Player newChar = new Player("Test Dude", Sexes.Male, races, charClasses);
		return newChar;
	}
	
	private Weapon CreateWeaponSword(EffectTypes effectType, AttributeTypes attributeTypes, int value, string name)
	{
		List<Effect> effects = new List<Effect>();
		
		Effect tempEffect = new Effect(effectType, attributeTypes, value);
		effects.Add(tempEffect);
		
		return new Weapon(6, WeaponTypes.Sword, Handedness.Single, DamageTypes.Slashing, name, ItemTypes.Weapon, MaterialTypes.Steel, 400, effects);
	}

	private void ItemWithEffectPlusOne()
	{
		Player newChar = CreateNewCharacter();
		
		Weapon wep = CreateWeaponSword(EffectTypes.Strengthen, AttributeTypes.Strength, 1, "Sword of Strength");
		
		newChar.PickUpItem(wep);
		newChar.EquipItem(0);

		Debug.Log(newChar.FindAttributeLevel(newChar.addedAttributes, AttributeTypes.Strength));
		Debug.Log(newChar.FindAttributeLevel(newChar.totalAttributes, AttributeTypes.Strength));
	}

	private void TestAbilities()
	{
		Player newChar = CreateNewCharacter();
		
		newChar.AddAttribute(new AssemblyCSharp.Attribute(AttributeTypes.Swords, 1));
		
		Weapon wep = CreateWeaponSword(EffectTypes.Strengthen, AttributeTypes.Swords, 1, "Sword of mastery");
		
		newChar.PickUpItem(wep);
		newChar.EquipItem(0);
		
		Debug.Log(newChar.FindAttributeLevel(newChar.baseAttributes, AttributeTypes.Swords));
		Debug.Log(newChar.FindAttributeLevel(newChar.addedAttributes, AttributeTypes.Swords));
		Debug.Log(newChar.FindAttributeLevel(newChar.totalAttributes, AttributeTypes.Swords));
	}

	public void TestAbilities2()
	{
//		Character newChar = CreateNewCharacter();
//		
//		newChar.AddAbility(new Ability(AbilityTypes.Swords, 1));
//		
//		Weapon wep = CreateWeaponSword(AbilityTypes.Swords, 1, "Sword of mastery");
//		
//		newChar.PickUpItem(wep);
//		newChar.EquipItem(0);
//		
//		Debug.Log(newChar.baseAbilities[0].SkillLevel);
//		Debug.Log(newChar.tempEffectAbilities[0].SkillLevel);
//		Debug.Log(newChar.totalAbilities[0].SkillLevel);
//
//		newChar.UnEquipItem(0);
//
//		Debug.Log(newChar.baseAbilities[0].SkillLevel);
//		Debug.Log(newChar.tempEffectAbilities.Count);
//		Debug.Log(newChar.totalAbilities[0].SkillLevel);
	}
	
	public void AddTemporaryEffect()
	{
		Player newChar = CreateNewCharacter();
		//EffectTypes effectType, int rounds
		newChar.AddAttribute(new AssemblyCSharp.Attribute(AttributeTypes.Swords, 1));
		
		Effect temp = new Effect(EffectTypes.Strengthen, AttributeTypes.Swords, 1, 1);
		newChar.AddTemporaryEffect(temp);
		
		Debug.Log(newChar.FindAttributeLevel(newChar.baseAttributes, AttributeTypes.Swords));
		Debug.Log(newChar.FindAttributeLevel(newChar.addedAttributes, AttributeTypes.Swords));
		Debug.Log(newChar.temporaryEffects[0].EffectValue);
		Debug.Log(newChar.FindAttributeLevel(newChar.totalAttributes, AttributeTypes.Swords));
		
		newChar.CharacterMove();
		
		Debug.Log(newChar.FindAttributeLevel(newChar.baseAttributes, AttributeTypes.Swords));
		Debug.Log(newChar.FindAttributeLevel(newChar.addedAttributes, AttributeTypes.Swords));
		Debug.Log(newChar.temporaryEffects.Count);
		Debug.Log(newChar.FindAttributeLevel(newChar.totalAttributes, AttributeTypes.Swords));
	}


	private void TestStats()
	{
		List<Race> races = new List<Race>();
		races.Add(new Race(RaceTypes.Dwarf));
		
		List<CharClass> charClasses = new List<CharClass>();
		charClasses.Add(new CharClass(CharacterClass.Warrior));
		
		Player newChar = new Player("Test Dude", Sexes.Male, races, charClasses);
		
		foreach(Attribute tempStat in newChar.baseAttributes)
		{
			Debug.Log("Stat Name: " + tempStat.AttributeName + " Stat Type: " + tempStat.AttributeType + " Stat Value: " + tempStat.AttributeLevel);
		}
		
		foreach(Attribute tempStat in newChar.addedAttributes)
		{
			Debug.Log("Stat Name: " + tempStat.AttributeName + " Stat Type: " + tempStat.AttributeType + " Stat Value: " + tempStat.AttributeLevel);
		}
		
		foreach(Attribute tempStat in newChar.totalAttributes)
		{
			Debug.Log("Stat Name: " + tempStat.AttributeName + " Stat Type: " + tempStat.AttributeType + " Stat Value: " + tempStat.AttributeLevel);
		}
	}
}
