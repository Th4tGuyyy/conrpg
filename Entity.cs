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

		protected String name;
		protected int health;



		protected Timer updateTimer;
		protected Random random;

		protected ViewRangeHandler viewHandler;
		private List<Point> lastViewedPoints = new List<Point>();

		protected int lastUpdatedFrame;

		public Entity(Level owner, Point location)
		{
			this.level = owner;
			this.location = location;
		}

		public virtual void setStats(Glyph sprite, float maxHealth, float moveSpeed, float agility, float defence, float strength, float attack)
		{
			this.sprite = sprite;


			updateTimer = new Timer(moveSpeed);
			viewHandler = new ViewRangeHandler(10);//range

		}

		public override void update()
		{
			if(updateTimer.complete() && lastUpdatedFrame != GAME.FrameTotal) {
				//updateTimer.start();
				randomMove();
				updateViewRange();
				lastUpdatedFrame = GAME.FrameTotal;
			}

			updateTimer.update();
		}

		public override void onInteract(Entity activator)//when player interacts with entity
		{
			//handle default interaction for entitys
			kill();
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

				level.world[newLoc.X, newLoc.Y].topObject.onInteract(this);
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

		public void setSpeed(float newSpeed)
		{
			updateTimer.setCoolDown(newSpeed);
			updateTimer.complete();
		}

		protected virtual void kill()
		{
			level.world[location.X, location.Y].topObject = null;	
		}


	}
}