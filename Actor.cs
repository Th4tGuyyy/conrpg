using ConsoleGameEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpgtest2020
{
	abstract class Actor : GameData
	{
		//inventory
		//stats
		//name
		//lvl = based off stats
		//location


		protected String name;
		protected int health;
		public Glyph sprite;

		protected CharacterStat maxHealth;
		protected CharacterStat moveSpeed;
		protected CharacterStat agility;
		protected CharacterStat defence;
		protected CharacterStat strength;
		protected CharacterStat attack;


		protected Timer updateTimer;
		protected Random random;

		protected Level level;
		protected Point location;


		public Actor(Level owner, Point location)
		{
			this.level = owner;
			this.location = location;
		}

		public virtual void setStats(Glyph sprite,float maxHealth, float moveSpeed, float agility, float defence, float strength, float attack)
		{
			this.sprite = sprite;

			this.maxHealth = new CharacterStat(maxHealth);
			this.moveSpeed = new CharacterStat(moveSpeed);
			this.agility = new CharacterStat(agility);
			this.defence = new CharacterStat(defence);
			this.strength = new CharacterStat(strength);
			this.attack = new CharacterStat(attack);

			updateTimer = new Timer(this.moveSpeed.Value);
		}

		public virtual void update()
		{
			if(updateTimer.complete()) {
				updateTimer.start();
				randomMove();
			}

			updateTimer.update();

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

			if(level.world[newLoc.X, newLoc.Y].entity != null){
				//no move do logic
				VConsole.writeLine("Hit not a wall!");
			}
			else if(level.world[newLoc.X, newLoc.Y].isSolid) {
				VConsole.writeLine("Hit a wall!");
			}
			else {
				//nothing infront, so move
				setActor(newLoc);
				movedTile = true;
			}

			return movedTile;
		}

		/// <summary>sets location to newLoc,</summary>
		private void setActor(Point newLoc)
		{
			level.world[location.X, location.Y].entity = null;
			level.world[newLoc.X, newLoc.Y].entity = this;
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



		

	}
}
