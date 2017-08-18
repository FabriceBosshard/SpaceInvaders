using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using Newtonsoft.Json;

namespace SpaceInvaders.Shop
{
    static class ShopValues
    {
        static NotifyHandler nh = NotifyHandler.InstanceCreation();
        private static int _highscore;
        private static int _waveScore;

        public static void LoadFromJson()
        {
            string obj = File.ReadAllText(@"..\\..\\Shop\\Shop.json");
            TempShopValue temp = JsonConvert.DeserializeObject<TempShopValue>(obj);
            nh = NotifyHandler.InstanceCreation();
            nh.VBombCount = temp.VBombCount;
            nh.Apokalypsecount = temp.Apokalypsecount;
            nh.GoldAmount = temp.GoldAmount;
            nh.SupernovaCount = temp.SupernovaCount;
            nh.HighestScore = temp.HighestScore;
            _highscore = temp.HighestScore;
            nh.WaveHighScore = temp.WaveHighScore;
            _waveScore = temp.WaveHighScore;
            nh.LivesExceed = temp.Lives;

        }

        public static TempShopValue getNewest()
        {
            string obj = File.ReadAllText(@"..\\..\\Shop\\Shop.json");
            TempShopValue temp = JsonConvert.DeserializeObject<TempShopValue>(obj);
            return temp;
        }

        public static void saveToJson()
        {
            TempShopValue obj = new TempShopValue();
            obj.Apokalypsecount = nh.Apokalypsecount;
            obj.GoldAmount = nh.GoldAmount;
            obj.SupernovaCount = nh.SupernovaCount;
            obj.VBombCount = nh.VBombCount;
            obj.Lives = nh.LivesExceed;
            if (_highscore < nh.Score)
            {
                obj.HighestScore = nh.Score;
            }
            else
            {
                obj.HighestScore = _highscore;
            }
            if (_waveScore < nh.Waves)
            {
                obj.WaveHighScore = nh.Waves;
            }
            else
            {
                obj.WaveHighScore = _waveScore;
            }
        
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(@"..\\..\\Shop\\Shop.json", json);
        }
    }
}
//TODo implement multiple lasers 5
//TODO seperate Bomb and laser and Special count  (So you can shoot a bomb while Laser is Shooting)4
//TODO implement heart item functionality