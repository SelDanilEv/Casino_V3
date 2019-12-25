using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Casino_V3
{
    public static class DataBase
    {
        public static List<Player> ActivPlayers = new List<Player>();

        private static DataContractJsonSerializer xmlformatter = new DataContractJsonSerializer(typeof(List<Player>));


        public static void Serialize(string path, List<Player> users)
        {
            using (FileStream fs = new FileStream(path + ".json", FileMode.OpenOrCreate))
            {
                xmlformatter.WriteObject(fs, users);
            }
        }

        public static void DeSerialize(string path, ref List<Player> users)
        {
            using (FileStream fs = new FileStream(path + ".json", FileMode.Open))
            {
                users = (List<Player>)xmlformatter.ReadObject(fs);
            }
        }

        public static bool AddPlayer(Player user)
        {
            if (user.Name == null || user.Password == null)
                return false;
            if (user.Name[0] == ' ' || user.Password[0] == ' ')
                return false;

            IEnumerable<Player> Check_Names = from us in ActivPlayers
                                            where us.Name == user.Name
                                            select us;
            if (Check_Names.Count() == 0)
            {
                user.Id = ActivPlayers.Count();
                ActivPlayers.Add(user);
                return true;
            }
            return false;
        }

        public static int Authorization(string name, string password)    //will be change on linq
        {
            int id = -2;
            foreach (Player user in ActivPlayers)
            {
                if (user.Name == name)
                {
                    if (user.Password == password)
                    {
                        id = user.Id;
                        break;
                    }
                    else
                    {
                        id = -1;
                    }
                }
            }
            return id;
        }

    }
}

