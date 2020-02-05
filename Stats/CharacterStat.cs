using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace rpgtest2020
{
	internal class CharacterStat
	{
		public float baseValue;

		public virtual float Value {
			get {
				if(isDirty || baseValue != lastBaseValue) {
					lastBaseValue = baseValue;
					_value = calcFinalValue();
					isDirty = false;
				}
				return _value;
			}
		}

		protected bool isDirty = true;
		protected float _value;
		protected float lastBaseValue = float.MinValue;

		protected readonly List<StatModifier> statModifiers;
		public readonly ReadOnlyCollection<StatModifier> StatModifiers;

		public CharacterStat(float baseValue)
		{
			this.baseValue = baseValue;
			statModifiers = new List<StatModifier>();
			StatModifiers = statModifiers.AsReadOnly();
		}

		public virtual void addModifier(StatModifier mod)
		{
			isDirty = true;
			statModifiers.Add(mod);
			statModifiers.Sort(compareModifierOrder);
		}

		protected virtual int compareModifierOrder(StatModifier a, StatModifier b)
		{
			if(a.order < b.order)
				return -1;
			else if(a.order > b.order)
				return 1;
			return 0;// a==b
		}

		public virtual bool removeModifier(StatModifier mod)
		{
			if(statModifiers.Remove(mod)) {
				isDirty = true;
				return true;
			}
			return false;
		}

		public virtual bool removeAllModifiersFromSource(object source)
		{
			bool didRemove = false;

			for(int i = statModifiers.Count - 1; i > 0; i++) {
				if(statModifiers[i].source == source) {
					isDirty = true;
					didRemove = true;
					statModifiers.RemoveAt(i);
				}
			}
			return didRemove;
		}

		protected virtual float calcFinalValue()
		{
			float finalValue = baseValue;
			float sumPercentAdd = 0;

			for(int i = 0; i < statModifiers.Count; i++) {
				StatModifier mod = statModifiers[i];

				if(mod.type == StatModType.Flat) {
					finalValue += statModifiers[i].value;
				}
				else if(mod.type == StatModType.PercentAdd) {
					sumPercentAdd += mod.value;
					if(i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd) {
						finalValue *= 1 + sumPercentAdd;
					}
				}
				else if(mod.type == StatModType.PercentMult) {
					finalValue *= 1 + mod.value;
				}
			}
			//15.0001 = != 15
			return (float)Math.Round(finalValue, 4);
		}
	}
}