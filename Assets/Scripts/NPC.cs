using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Faction
{
    Friendly, // on the player's side 
    Enemy, // opposed to the player
    Neutral // starts at neutral, but if the place where the neutral guys are is captured, they join the capturing faction
}
public class NPC // model off PARTS of the NPC logic in Dune II
{

	public Faction faction;

	public NPC(Faction faction)
	{
		this.faction = faction;
		// and more stuff
	}

	public void changeFaction(Faction faction)
	{
		this.faction = faction;
	}

}

public class Enemy
{
	// the controller class of the enemies to the player
	Faction faction = Faction.Enemy;
}

public class Friendly
{
	// the controller class to the player's soldiers
	Faction faction = Faction.Friendly;
}

public class Neutral
{
	// the neutral soldiers, if their command post is taken, they join the capturing side
	public Faction faction = Faction.Neutral;

	public CommandPost home;
}

public class CommandPost
{
	public Vector3 position;

	//public Rect boundries = new Rect(x, y, width, height);
	Rect boundries = new Rect(0, 0, 10, 10);

	public Faction alliedPositon; 

	public float points = 0; // -50 is Enemy, 50 is Friendly, 0 is neutral
	public Text HP; // have a text over the 
	public List<GameObject> allGOs; // a list of all the gameobjects in the scene
	public float rate = 1f; // the rate at a command post will be captured

	public CommandPost(Vector3 pos, Text text, List<GameObject> gos)
	{
		this.position = pos;
		this.HP = text;
		this.allGOs = gos;
		points = Mathf.Clamp(points, -50, 50);
	}

	public void updatePoints()
	{
		var inside = ViewInterior();
		points += inside * rate; 

		// START_ERRORS
		//if(points >= 25) { alliedPosition = Faction.Friendly; HP.text = $"Friendly, health: {MathF.Abs(points)}"; }
		//else if(points <= -25) { alliedPositon = Faction.Enemy; HP.text = $"Enemy, health: {MathF.Abs(points)}"; }
		//else { alliedPositon = Faction.Neutral; HP.text = $"Neutral, health: {MathF.Abs(points)}";}
		// END_ERRORS
	}

	/*
	Dictionary<GameObject, Faction> ViewInterior()
	{
		Dictionary<GameObject, Faction> units = new Dictionary<GameObject, Faction>();
		foreach(var obj in allGOs)
		{
			if(boundries.Contains(obj) && obj.NPC_Controller.faction != Faction.Neutral)
			{
				units.TryAdd(obj, obj.NPC_Controller.faction); // possibly won't work, there's no "obj.faction"
			}
		}
		return contains;
	}
	*/
	int ViewInterior()
	{
		int inide = 0; // the units inside: 0->Neutral(no Enemy or Friendly),  1->Friendly(more Friendly than Enemy), -1->Enemy(more Enemy than friendly)
		// even if there's more Neutral NPCs than other classes, the class other than Neutral with the most numbers gets the points
		// if the number of Enemy and Friendly classes are equal, return 0

		int good = 0; // Friendly count
		int bad = 0; // Enemy count

		foreach(var obj in allGOs)
		{
			if(boundries.Contains(new Vector2(obj.transform.position.x, obj.transform.position.y))) //  && obj.NPC_Controller.faction != Faction.Neutral)
			{
				//if(obj.NPC_Controller.faction == Faction.Enemy) { bad++; }
				//if(obj.NPC_Controller.faction == Faction.Friendly) { good++; }
			}
		}
		//inside = (good != bad) ? (good > bad) ? 1 : -1 : 0; 
		//return inside;
		return 0;
	}
	
}