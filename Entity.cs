using ConsoleGameEngine;
using System;
using System.Collections.Generic;

namespace rpgtest2020
{
	internal class Entity : Interactable
	{
		//inventory
		//stats
		//name
		//lvl = based off stats
		//location

		/*
	 Job:
		default stat mod
		default inventory
		special actions ex: shop, attacking, running
		stack of interations to complete
	 Race:
		defulat stats
		attack other races
		only friendly interacts with same race

	General:
		order of day:
			1. wake up
			2. do job
			3. go back to bed(spawn point) and sleep

		Events what happen on day
			- attack player or enemy if in range and "can" attack?
			- run away if cant attack
			- clear stack and go to bed

		 */

		protected String name;
		protected int health;

		protected CharacterStat maxHealth;
		protected CharacterStat moveSpeed;
		protected CharacterStat agility;
		protected CharacterStat defence;
		protected CharacterStat strength;
		protected CharacterStat attack;

		protected Timer updateTimer;
		protected Random random;

		private ViewRangeHandler viewHandler;
		private List<Point> lastViewedPoints = new List<Point>();

		public Entity(Level owner, Point location)
		{
			this.level = owner;
			this.location = location;
		}

		public virtual void setStats(Glyph sprite, float maxHealth, float moveSpeed, float agility, float defence, float strength, float attack)
		{
			this.sprite = sprite;

			this.maxHealth = new CharacterStat(maxHealth);
			this.moveSpeed = new CharacterStat(moveSpeed);
			this.agility = new CharacterStat(agility);
			this.defence = new CharacterStat(defence);
			this.strength = new CharacterStat(strength);
			this.attack = new CharacterStat(attack);

			updateTimer = new Timer(this.moveSpeed.Value);
			viewHandler = new ViewRangeHandler(5);//range
		}

		public override void update()
		{
			if(updateTimer.complete()) {
				updateTimer.start();
				randomMove();
				updateViewRange();
			}

			updateTimer.update();
		}

		public List<Point> getLastViewPoints()
		{
			return lastViewedPoints;
		} 

		protected void updateViewRange()
		{
			viewHandler.scanArea(level.world, location);
			lastViewedPoints = viewHandler.viewedPoints;
		}

		private void randomMove()
		{
			random = new Random();
			int x = random.Next(-1, 2);
			int y = random.Next(-1, 2);

			if(!move(new Point(location.X + x, location.Y + y)))
				randomMove();
		}

		/// <summary>attempts to move to newLoc, handles logic</summary>
		public virtual bool move(Point newLoc)
		{
			bool movedTile = false;

			if(level.world[newLoc.X, newLoc.Y].topObject != null) {
				//no move do logic
				//VConsole.writeLine("Hit not a wall!");
				if(level.world[newLoc.X, newLoc.Y].topObject is Teleporter) {
					say("getting ready to Teleporting from entity");
					Teleporter t = (Teleporter)level.world[newLoc.X, newLoc.Y].topObject;
					t.teleport(this);
				}
			}
			else if(level.world[newLoc.X, newLoc.Y].isSolid) {
				//VConsole.writeLine("Hit a wall!");
			}
			else {
				//nothing infront, so move
				//say("Moved");
				setActor(newLoc);
				movedTile = true;
			}

			return movedTile;
		}

		/// <summary>Changes only the point, not the map[,] dont normally call this function</summary>
		public void hardSetLocation(Point newLoc)
		{
			location = newLoc;
		}

		/// <summary>sets location to newLoc,</summary>
		private void setActor(Point newLoc)
		{
			level.world[location.X, location.Y].topObject = null;
			level.world[newLoc.X, newLoc.Y].topObject = this;
			location = newLoc;
		}

		public void say(String text)
		{
			VConsole.writeLine($"{name}: {text}");
		}

		public Point getLocation()
		{
			return location;
		}

		public bool canSee(int x, int y)
		{
			return viewHandler.viewedPoints.Contains(new Point(x, y));
		}
	}
}