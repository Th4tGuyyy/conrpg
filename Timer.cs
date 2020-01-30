﻿using System;
using System.Collections.Generic;
using System.Text;

namespace rpgtest2020
{
	class Timer : GameData
	{
		private readonly float coolDown;
		private float timer;
		private bool canCount = true;
		private bool doOnce = false;


		public Timer(float coolDown) { this.coolDown = coolDown; start(); }

		/// <summary>
		/// Expected to get called every frame, increacements timer 
		/// </summary>
		public void update()
		{

			if(timer > 0f && canCount)
				timer -= GAME.DeltaTime / (float)GAME.GetFramerate();
			else if(timer <= 0f && !doOnce)
				stop();
			
		}

		/// <summary>
		/// returns if the timer is complete(stopped running)
		/// </summary>
		/// <returns>timer <=0 </returns>
		public bool complete()
		{
			return timer <= 0; 
		}

		/// <summary>starts timer, or restarts it</summary>
		public void start()
		{
			timer = coolDown;
			canCount = true;
			doOnce = false;
		}

		/// <summary>Stops timer </summary>
		public void stop()
		{
			canCount = false;
			doOnce = true;
			timer = 0;
		}
	}
}
