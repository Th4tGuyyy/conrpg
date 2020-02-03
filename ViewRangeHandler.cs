using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class ViewRangeHandler
	{
		class RangeList
		{
			class Node
			{
				public double min, max;
				public Node next;

				public Node(double min, double max) { this.min = min; this.max = max; }
				public override bool Equals(object obj)
				{
					Node r = (Node)obj;
					return min == r.min && max == r.max;
				}
			}

			Node top;

			public RangeList() => top = null;

			public void add(double min, double max)
			{
				Node newNode = new Node(min, max);
				newNode.next = top;
				top = newNode;
			}

			public bool contains(double value)
			{
				bool found = false;
				Node current = top;
				while (current != null && !found)
				{
					if (current.min <= value && value <= current.max)
						found = true;
					current = current.next;
				}

				return found;
			}

			public void clear() => top = null;

		}

		private RangeList bannedRadients = new RangeList();
		public List<Point> viewedPoints;

		int viewRange = 5;
		static double margin = 0.05;
		static double radStep = 0.05;//
		static double radStart = 0.00000001;

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

			for (int i = 0; i < 2 + viewRange; i++)
				scanRing(map,start, i);
		}

		private void scanRing(Tile[,]map, Point start, double radius)
		{
			for (double i = radStart; i < 2 * Math.PI; i += radStep)
			{
				Point e = viewPoint(map, start, radius, i);
				if (e != new Point(-1, -1))
					viewedPoints.Add(e);
			}
				
		}

		private Point viewPoint(Tile[,] map, Point start, double radius, double radient)
		{
			Point viewedPoint = new Point(-1,-1);
			int x = (int)(radius * Math.Cos(radient)) + start.X;
			int y = (int)(radius * Math.Sin(radient)) + start.Y;

			if (x < map.GetLength(0) && y < map.GetLength(1) && y >= 0 && x >= 0)
			{
				if (map[x, y].isSolid)
				{
					viewedPoint = new Point(x, y);
					bannedRadients.add(radient - margin, radient + margin);
				}
				else if (!map[x, y].isTransparent && !bannedRadients.contains(radient))
				{
					bannedRadients.add(radient - margin, radient + margin);
					viewedPoint = new Point(x, y);
				}
				else if (!bannedRadients.contains(radient)/* && x != start.X && y != start.Y*/)
				{
					viewedPoint = new Point(x, y);
				}//valid tile
			}




			return viewedPoint;
		}
	}
}
