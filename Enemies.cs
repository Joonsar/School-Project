using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Enemies
    {
        List<Enemy> enemies;
        Random rand =  new Random();
        string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string enemyDescriptiosOne = "Pari sillon tällön,Saunakaljat mukana,Muutamat aina maistuu,Juhlatilaisuuksissa kilistää ei muuten,Eläkkeellä voi pari konjakkia ottaa!,Lähtee yhdelle ja pysyy siinä";
        string enemyDescriptiosTwo = "Koko vkl putkeen!,Sixpäkki duunin jälkee heti pöytää!,Juo pelkkää olutta ja korvaa kaiken sanomansa PERKELE huudolla,Juo salaa.. hyi!";
        string enemyDescriptionsThree = "melkonen juoppo,Välillä huiliiki,Juo käänit ja aiheuttaa pahennusta kylillä,Välillä heilahtaa viikko kaks..,Katkolta alkon kautta";
        string enemyDescriptionsFour = "Kaikki tauluu mitä löytyy,Kusen tuoksu hiipii nenää jo kaukaa,Täyspäivänen duuni pysyy tönössä..";
        string enemyDescriptionsFive = "Nesteeltä lasolia vaikka pikkasen iho kellertääki";

        string enemyNamesOne = "Tissuttelija Tauno,Tuoppi Matti,Junnu Jannu,Repa duunari,Seppo sivistyneesti";
        String enemyNamesTwo = "Juoppo Jaska,Piilojuoppo Pekka,Rapajuoppu Reino,Pelkkä Keijo,Ex nyrkkeilijä puistosta";
        string enemyNamesThree = "Semi pro,Lasse lähti lapasesta,Ihan vaan Seppo,Taiteilija Thomas";
        string enemyNamesFour = "Pelle Pöhnä,Märkäkorva Marko,Pimeyden Reino,Viinapiru Väinö";
        string enemyNamesFive = "Pro,Puiston Jaska,Ihan vaan ammattilainen,Delirium topi,Kadun mies,Puiston asukki";
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        public Enemies()
        {
           this.enemies = new List<Enemy>();
            
        }
        public List<Enemy> GetEnemyListByLevel(int lvl, int howMany)
        {

            int count = 0;
            List<string> randomizedNameList = new List<string>();
            List<string> randomizedDescriptionList = new List<string>();
            if(lvl > 10)
            {
                randomizedNameList = new List<string>(enemyNamesFive.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptionsFive.Split(',').ToList().OrderBy(item => rand.Next()));
                
            }
            else if(lvl > 8)
            {
                randomizedNameList = new List<string>(enemyNamesFour.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptionsFour.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else if(lvl > 5)
            {
                randomizedNameList = new List<string>(enemyNamesThree.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptionsThree.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else if(lvl > 2)
            {
                randomizedNameList = new List<string>(enemyNamesTwo.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptiosTwo.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else
            {
                randomizedNameList = new List<string>(enemyNamesOne.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptiosOne.Split(',').ToList().OrderBy(item => rand.Next()));
            }

            while (count < howMany)
            {
                char randomChar1 = charString[rand.Next(0, charString.Length)];
                ConsoleColor color = colors[rand.Next(0,colors.Length)];
                enemies.Add(new Enemy((randomizedNameList[rand.Next(0, randomizedNameList.Count())]), randomizedDescriptionList[rand.Next(0, randomizedDescriptionList.Count())], new Position(rand.Next(1, 40), rand.Next(1, 15)), randomChar1, color, 300*1*lvl, 10*1*lvl, 0+lvl));
                count++;
            }
            return enemies;
        }

    }

}
