using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casino_V3
{
    [Serializable]
    public class Player
    {
        public Player(string name, string password, int cash)
        {
            Name = name;
            Password = password;
            Cash = cash;
            TypeOfRate = TypeRate.noth;
        }

        public Player() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Rate { get; set; }
        public int Cash { get; set; }

        [NonSerialized]
        public bool CorrectTransition;


        public TypeRate TypeOfRate { get; set; }

        public enum ColorRate : int
        {
            black = 0,
            white
        }

        public enum SectorRate : int
        {
            sector1_8 = 0,
            sector9_16,
            sector17_24,
            sector25_32,
        }

        public enum TypeRate : int
        {
            noth = 0,
            zero,
            color,
            sector
        }
    }
}

