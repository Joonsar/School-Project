using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class Enemies
    {
        private GameController gc = GameController.Instance;
        private List<Enemy> enemies;
        private Random rand = new Random();

        //string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //pitäskö nää vaan muuttaa, että kaikki pötkössä ja sieltä arvotaan. tuntuu vähän turhalta tää leveli juttu.
        private string enemyDescriptiosOne = "Pari sillon tällön,Saunakaljat mukana,Muutamat aina maistuu,Juhlatilaisuuksissa kilistää ei muuten,Eläkkeellä voi pari konjakkia ottaa!,Lähtee yhdelle ja pysyy siinä,Näprää tietokoneella,Vaimo sano että yks vaan.,Harvoin juo. mutta sitten tulee ongelmia.,Kovaääninen juhlija. usein muisti pätkii.,Yllättäen juo, mutta aina kalja kädessä.,Väittää olevansa raitis, mutta salaa juo.,Kokenut, nauttii viskiä.,Salamajuoppo napostelee juotuaan.,Tupakoi ja juo usein yksin.,Pitää viinistä. mutta ei juo koskaan paljon.,Tykkää maistella erilaisia juomia.,Sosiaalinen juoja joka rakastaa bileitä.,Pieni lasi punaviiniä päivässä pitää terveenä.,Kovaa juovien porukassa. mutta ei itse ole kova juomaan.";

        private string enemyDescriptiosTwo = "Koko vkl putkeen!,Sixpäkki duunin jälkee heti pöytää!,Juo pelkkää olutta ja korvaa kaiken sanomansa PERKELE huudolla,Juo salaa.. hyi!";
        private string enemyDescriptionsThree = "melkonen juoppo,Välillä huiliiki,Juo käänit ja aiheuttaa pahennusta kylillä,Välillä heilahtaa viikko kaks..,Katkolta kotiin alkon kautta";
        private string enemyDescriptionsFour = "Kaikki tauluu mitä löytyy,Kusen tuoksu hiipii nenää jo kaukaa,Täyspäivänen duuni pysyy tönössä..";
        private string enemyDescriptionsFive = "Nesteeltä lasolia vaikka pikkasen iho kellertääki";

        private string enemyNamesOne = "Tissuttelija Tauno,Tuoppi Matti,Junnu Jannu,Repa duunari,Seppo sivistyneesti,Naapurin Pena,Puisto-Paavo,Rappukäytävän Rauno,Hepuli Henkka,Joskus Joonas,Iltaisin Ilkka,Kaarle Kustaa,Kaisa-Maija,Hilkka Hiljainen,Vihainen Väinö,Tuhma-Tuulia,Puliveivi-Petteri,Korjari-Kalle,Kuura-Kaisa,Kierosilmä-Kalle,Kukko-Kustaa,Vihreä-Vilho,Röyhkeä-Riku,Sähäkkä-Sari,Sini-Simo,Karski-Kalle,Leijuva-Liisa,Pimeyden Pekka,Suksi-Sakari,Timanttinen Tiina,Ukko-Pekka,Vilkas-Ville";

        //private string enemyNamesOne = "Tissuttelija Tauno,Tuoppi Matti,Junnu Jannu,Repa duunari,Seppo sivistyneesti,Naapurin Pena,Puisto-Paavo,Rappukäytävän Rauno,Hepuli Henkka,Joskus Joonas,Iltaisin Ilkka";
        private string enemyNamesTwo = "Juoppo Jaska,Piilojuoppo Pekka,Rapajuoppu Reino,Pelkkä Keijo,Ex nyrkkeilijä puistosta";

        private string enemyNamesThree = "Semi pro,Lasse lähti lapasesta,Ihan vaan Seppo,Taiteilija Thomas";
        private string enemyNamesFour = "Pelle Pöhnä,Märkäkorva Marko,Pimeyden Reino,Viinapiru Väinö";
        private string enemyNamesFive = "Pro,Puiston Jaska,Ihan vaan ammattilainen,Delirium topi,Kadun mies,Puiston asukki";

        private ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        public List<ConsoleColor> c;

        public Enemies()
        {
            this.enemies = new List<Enemy>();
            this.c = colors.ToList<ConsoleColor>();
            this.c.Remove(ConsoleColor.Black);
        }

        public List<Enemy> GetEnemyListByLevel(int lvl, int howMany)
        {
            var allNames = new string(enemyNamesOne + "," + enemyNamesTwo + "," + enemyNamesThree + "," + enemyNamesFour + "," + enemyNamesFive);
            int count = 0;
            List<string> randomizedNameList = new List<string>();
            List<string> randomizedDescriptionList = new List<string>();
            if (lvl > 10)
            {
                randomizedNameList = new List<string>(allNames.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptionsFive.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else if (lvl > 8)
            {
                randomizedNameList = new List<string>(allNames.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptionsFour.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else if (lvl > 5)
            {
                randomizedNameList = new List<string>(enemyNamesThree.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptionsThree.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else if (lvl > 2)
            {
                randomizedNameList = new List<string>(allNames.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptiosTwo.Split(',').ToList().OrderBy(item => rand.Next()));
            }
            else
            {
                randomizedNameList = new List<string>(allNames.Split(',').ToList().OrderBy(item => rand.Next()));
                //randomizedNameList = new List<string>(enemyNamesOne.Split(',').ToList().OrderBy(item => rand.Next()));
                randomizedDescriptionList = new List<string>(enemyDescriptiosOne.Split(',').ToList().OrderBy(item => rand.Next()));
            }

            while (count < howMany)
            {
                //char randomChar1 = charString[rand.Next(0, charString.Length)];
                ConsoleColor color = this.c[rand.Next(0, colors.Length - 1)];
                string name = randomizedNameList[rand.Next(0, randomizedNameList.Count())];
                enemies.Add(new Enemy(name, randomizedDescriptionList[rand.Next(0, randomizedDescriptionList.Count())], new Position(rand.Next(1, gc.Map.Width), rand.Next(1, gc.Map.Height)), name[0], color, 300 + 300 * lvl, 10 + 10 * lvl, lvl));
                count++;
            }
            return enemies;
        }
    }
}