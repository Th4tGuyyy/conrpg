using ConsoleGameEngine;
using System;
using System.Collections.Generic;

namespace rpgtest2020
{
	internal class ViewRangeHandler
	{
		private class RangeList
		{
			private class RangeNode
			{
				public double min, max;
				public RangeNode next;

				public RangeNode(double min, double max)
				{
					this.min = min; this.max = max;
				}

				/*public bool Equals(object obj)
				{
					RangeNode r = (RangeNode)obj;
					return min == r.min && max == r.max;
				}*/
			}

			private RangeNode top;

			public RangeList() => top = null;

			public void add(double min, double max)
			{
				RangeNode newNode = new RangeNode(min, max);
				newNode.next = top;
				top = newNode;
			}

			public bool contains(double value)
			{
				bool found = false;
				RangeNode current = top;
				while(current != null && !found) {
					if(current.min <= value && value <= current.max)
						found = true;
					current = current.next;
				}

				return found;
			}

			public void clear() => top = null;
		}

		private RangeList bannedRadients = new RangeList();
		public List<Point> viewedPoints;

		private int viewRange = 5;
		private static double margin = 0.05;
		private static double radStep = 0.05;//0.05
		private static double radStart = 0.00000001;

		public ViewRangeHandler(int range)
		{
			viewRange = range;
			viewedPoints = new List<Point>();
		}

		/*
			***
			*@*  -> *****
			***     *   *		but a circle
					* @ *
					*   *
					*****
			 */

		public void scanArea(Tile[,] map, Point start)
		{
			viewedPoints.Clear();
			bannedRadients.clear();

			for(int i = 0; i < 2 + viewRange; i++)
				scanRing(map, start, i);
		}

		private void scanRing(Tile[,] map, Point start, double radius)
		{
			for(double i = radStart; i < 2 * Math.PI; i += radStep) {
				Point e = viewPoint(map, start, radius, i);
				if(e != new Point(-1, -1))
					viewedPoints.Add(e);
			}
		}

		private Point viewPoint(Tile[,] map, Point start, double radius, double radient)
		{
			Point viewedPoint = new Point(-1, -1);
			int x = (int)(radius * Math.Cos(radient)) + start.X;
			int y = (int)(radius * Math.Sin(radient)) + start.Y;

			if(x < map.GetLength(0) && y < map.GetLength(1) && y >= 0 && x >= 0) {
				/*if (map[x, y].isSolid)
				{
					viewedPoint = new Point(x, y);
					bannedRadients.add(radient - margin, radient + margin);
				}*/
				if(!map[x, y].isTransparent && !bannedRadients.contains(radient)) {
					bannedRadients.add(radient - margin, radient + margin);
					viewedPoint = new Point(x, y);
				}
				else if(!bannedRadients.contains(radient)/* && x != start.X && y != start.Y*/) {
					viewedPoint = new Point(x, y);
				}//valid tile
			}

			return viewedPoint;
		}
	}
}