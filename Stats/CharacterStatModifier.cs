using System;
using System.Collections.Generic;
using System.Text;

namespace rpgtest2020
{
    public enum Stats
    {
        MaxHealth,
        Armor,
        Strength,
        Dexterity
    }
    internal class CharacterStatModifier
    {
        private Stats stat_;
        private StatModifier modifier_;

        public Stats Stat {
            get { return stat_; }
            set { stat_ = value; }
        }

        public StatModifier Modifier {
            get { return modifier_; }
            set { modifier_ = value; }
        }

        public CharacterStatModifier(Stats stat, StatModifier modifier)
        {
            stat_ = stat;
            modifier_ = modifier;
        }
    }
}
