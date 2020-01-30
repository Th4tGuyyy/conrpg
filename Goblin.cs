using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Goblin : Enemy
	{
		int viewRange = 10;
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
		public Goblin(Level owner, Point location) : base(owner,location)
		{
		}


		public override void update()
		{
			base.update();
		}

		public void aStarMove(Point target)
		{

		}
	}
}
