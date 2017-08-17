using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceInvaders.Shop
{
    static class ShopValues
    {

        public static void LoadFromJson()
        {
            string obj = File.ReadAllText(@"C:\Users\A639359\Documents\GitHub\SpaceInvaders\SpaceInvaders\Shop\Shop.json");
            TempShopValue temp = JsonConvert.DeserializeObject<TempShopValue>(obj);
            NotifyHandler nh = NotifyHandler.InstanceCreation();
            nh.VBombCount = temp.VBombCount;
            nh.Apokalypsecount = temp.Apokalypsecount;
            nh.GoldAmount = temp.GoldAmount;
            nh.SupernovaCount = temp.SupernovaCount;

            Debug.WriteLine(""+nh.GoldAmount+nh.Apokalypsecount);
        }

        public static void saveToJson(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(@"C:\Users\A639359\Documents\GitHub\SpaceInvaders\SpaceInvaders\Shop\Shop.json", json);
        }
    }
}
//TODO evaluate Goldfunction retrieval 2
//TODO make new Items6
//TODO shop user interface 5
//Todo implement skills 1
//TOdo optimize Look of board 215
//TODO implement pause correctly 4
//TODo implement multiple lasers
//TODO seperate Bomb and laser and Special count  (So you can shoot a bomb while Laser is Shooting)4
//TODO Save to JSON 3