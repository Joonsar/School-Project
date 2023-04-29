using System;
using System.Collections.Generic;
using System.Linq;

namespace School_Project
{
    public class Enemies
    {
        private GameController gc = GameController.Instance;
        private Random rand = new Random();
        private List<ConsoleColor> colors = new List<ConsoleColor>((ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor)));

        //string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string[] enemyDescriptions = new string[]
        {
            "Pari sillon tällön","Saunakaljat mukana","Muutamat aina maistuu","Juhlatilaisuuksissa kilistää ei muuten","Eläkkeellä voi pari konjakkia ottaa!",
            "Lähtee yhdelle ja pysyy siinä","Näprää tietokoneella","Vaimo sano että yks vaan.","Harvoin juo, mutta sitten tulee ongelmia.",
            "Kovaääninen juhlija, usein muisti pätkii.","Yllättäen juo, mutta aina kalja kädessä.","Väittää olevansa raitis, mutta salaa juo.",
            "Kokenut","Nauttii viskiä.","Salamajuoppo napostelee juotuaan.","Tupakoi ja juo usein yksin.","Pitää viinistä, mutta ei juo koskaan paljon.",
            "Tykkää maistella erilaisia juomia.","Sosiaalinen juoja joka rakastaa bileitä.","Pieni lasi punaviiniä päivässä pitää terveenä.",
            "Kovaa juovien porukassa, mutta ei itse ole kova juomaan.","Koko vkl putkeen!","Sixpäkki duunin jälkee heti pöytää!",
            "Juo pelkkää olutta ja korvaa kaiken sanomansa PERKELE huudolla","Juo salaa.. hyi!","melkonen juoppo,Välillä huiliiki",
            "Juo käänit ja aiheuttaa pahennusta kylillä","Välillä heilahtaa viikko kaks..","Katkolta kotiin alkon kautta","Kaikki tauluu mitä löytyy",
            "Virtsan tuoksu hiipii nenää jo kaukaa","Täyspäivänen duuni pysyy tönössä..","Nesteeltä lasolia vaikka pikkasen iho kellertääki",
            "Tuntuu olevan aina juhlissa mukana, missä tahansa.","Lempparijuoma on tequila, mutta kelpaa mikä tahansa poltteleva.","Häröilee joka kerta ja aiheuttaa aina draamaa.",
            "Onnistuu aina juomaan itsensä sammaksi asti.","Ei osaa pitää juomistaan erillään työpäivän jälkeen.","On ihan kiva tyyppi muulloin, mutta juodessaan muuttuu täysin.",
            "Saa päähänsä huumorin vain itse humalassa ollessaan.","Pysyy tiukassa dieetissä viikon ajan, kunnes lähtee viikonlopuksi juhlimaan kaiken takaisin.",
            "Tuntuu keskittyvän pelkästään juomansa määrään, eikä juuri muuhun.","Tykkää kokeilla kaikkea uutta ja trendikästä.",
            "Juo pitkään, mutta pysyy kunnossa ilman sammumisia.","Tietää, mitä haluaa, juomisen suhteen ja muutenkin.",
            "Juo aina liikaa, mutta osaa ainakin nauraa sille itsekin.","Väittää olevansa elämää suurempi, mutta humalassa tulee syvät suvantovaiheet.",
            "Tykkää juoda yksin kotonaan, mutta sitten lähtee pahasti hukkaan jossain baarissa.","Vaarallisen korkeakierroksinen juoma-asia, joka heittelee mieltä ja ruumista.",
            "Pitää vähän kaikista juomista, mutta sekaan mahtuu monesti myös aineita.","Juot kaikesta huolimatta ilostuneena ja hyväntuulisena.",
            "Juot juuri sen verran, että alkaa puhua itsestään kolmannessa persoonassa.","Juot vain kotona tai luonnon helmassa, jolloin kukaan ei näe hänen käytöstään.",
            "Ei tee koskaan hommia ennen kuin juo ensin aamukahvin ja paloviinan.","On elämänsä kunnossa vain juodessaan, muuten vetää koko ajan ylihilseen.",
            "Juot kännissä vain yhden tunnin karuselli-ajelulaitteessa.","On pitänyt paussia juomisesta jo viisi vuotta, mutta silloin tällöin kostautuu entisellä kuumalla asialla.",
            "Juoksee maratoneja oluttölkki kädessä.","Juonut niin paljon vettä, että se muuttui viiniksi.","Ei tiedä mitään muuta kuin tislaamisesta.","Pitää baarissa juotuaan erilaisia kilpailuja, mutta häviää aina.",
            "Löysi elämäntehtävänsä valmistaa maailman vahvimman alkoholin.","Korvaa aina veden viinillä, sillä se on terveellisempää.","Opetteli juomaan veden sijaan viskiä, sillä vesi on liian tylsää.",
            "Väittää olevansa elävä viinikellari, mutta ei ole koskaan maistanut viiniä.","On juonut kaikenlaista, mutta ei koskaan jättänyt huonon maun vuoksi juomatta.",
            "Käyttää juomatikut hammasharjanaan, sillä se pitää hampaat terveinä.","Pitää sitruunan mausta, mutta lisää sitä aina vodkaan.",
            "Tehnyt päänsä kokoiseen shottilasiin tequilan ja onnistui juomaan sen.","Opetteli juomaan yhtäjaksoisesti koko yön juhlissa, mutta hävisi seuraavana päivänä aivotoimintaa.",
            "On keksinyt uuden lajin, jossa sekoitetaan viiniä ja maitoa.","Luulee olevansa supersankari, joka voi juoda mitä tahansa ilman seurauksia.",
            "Onnistuu juomaan itsensä humalaan vain yhdellä tuopillisella.","Pitää vaaleasta viinistä, mutta lisää siihen aina punaviiniä.","Juoksee maratonin, jonka varrella on pelkkiä baareja.",
            "Juoksee maratonin humalassa.","Yrittää keksiä uusia käyttötarkoituksia oluelle.","Korvaa aina maitonsa oluella, sillä se on terveellisempää.",
            "Käyttää olutta marinadina kaikessa.","Pitää kaikkea juomaa sokeripitoisena, jotta se maistuu paremmalta.","Lisää aina juomaan valkosipulia, sillä se on hyväksi terveydelle."
        };

        private string[] enemyNames = new string[]
        {
            "Tissuttelija Tauno","Tuoppi Matti","Junnu Jannu","Repa duunari","Seppo sivistyneesti","Naapurin Pena","Puisto-Paavo","Rappukäytävän Rauno","Hepuli Henkka","Joskus Joonas",
            "Iltaisin Ilkka","Kaarle Kustaa","Kaisa-Maija","Hilkka Hiljainen","Vihainen Väinö","Tuhma-Tuulia","Puliveivi-Petteri","Korjari-Kalle","Kuura-Kaisa","Kierosilmä-Kalle",
            "Kukko-Kustaa","Vihreä-Vilho","Röyhkeä-Riku","Sähäkkä-Sari","Sini-Simo","Karski-Kalle","Leijuva-Liisa","Pimeyden Pekka","Suksi-Sakari","Timanttinen Tiina","Ukko-Pekka",
            "Vilkas-Ville","Juoppo Jaska","Piilojuoppo Pekka","Rapajuoppu Reino","Pelkkä Keijo","Ex nyrkkeilijä puistosta","Semi pro","Lasse lähtilapasesta","Ihan vaan Seppo","Taiteilija Thomas",
            "Pelle Pöhnä","Märkäkorva Marko","Pimeyden Reino","Viinapiru Väinö","Pro","Puiston Jaska","Ihan vaan ammattilainen","Delirium topi","Kadun mies","Puiston asukki",
            "Huligaani Heikki", "Äkäinen Aki", "Väkivaltainen Valtteri", "Kähärä-Kalle", "Vino-Ville", "Kauhea Kusti", "Rotsi-Rami", "Jyrä-Janne", "Hinaaja Henri", "Myrsky-Matti",
            "Riehakas Rami", "Köriläs Kauko", "Sähikäinen Sari", "Muukalainen Martti", "Huutava Heimo", "Roistava Raimo", "Viikinki-Veikko", "Sokeus-Satu", "Jallumainen Jarkko",
            "Räyhäävä Raisa", "Rehti-Reijo", "Kuolinki-Kari", "Mutkikas Matti", "Nyrkkeilevä Nanna", "Mustahuuli Mika", "Anarkista Antti", "Pimpottaja Päivi", "Karjuva Kari", "Pöljä Pasi",
            "Vieteri-Ville","Pikku-Päivi","Höpinä-Helena","Hämy-Hannu","Ruoste-Raimo","Näätä-Niina","Rauni Rämäpää","Mörkö-Matti","Loistava Lassi","Vauhti-Valtteri","Onnen-Onni",
            "Hän,Oikea Hän","Lupaus-Laura","Heinäsirkka-Helmi","Sisu-Sampsa","Pajunkissapoika-Pekka","Jippo-Jarmo","Siivetön-Sampo","Juuri-Juha","Särkänsilmä-Sari","Salamakypärä-Simo",
            "Loisto-Liisa","Etsivä-Eemeli","Räjähtävä-Roope","Pikkupomo-Pekka","Herrasväki-Helena","Pölyinen-Pekka","Lohikäärme-Leena","Divari-David","Tohina-Timo","Riemurinnassa-Riikka",
            "Jännityksen-Jaska","Tämä-Timo","Piru-Pete","Tunturi-Taru","Kyhmyinen-Kaija","Nenäliina-Niilo","Härski-Hilma","Riemukas-Risto","Jäykistävä-Jani","Tihkuisa-Tuomas",
            "Viheltelijä-Veeti","Käpylän-Kirsi","Uhuva-Urho","Sopuli-Saara","Punaposki-Pertti","Seksikäs-Salla","Mummotunneli-Mikko","Puhurin-Pirkka","Robotti-Roope","Kevätpörriäinen-Kaisa",
            "Riemukupla-Roope","Nyyhky-Niina","Mykerö-Maija","Sutjakka-Sakari","Uutisankka-Ulla","Kilja-Kylli","Silkkisukka-Sakke","Tukkapölly-Tuomas","Romanttinen Raimo","Verbaalinen-Ville",
            "Hippiveli-Hannu","Jatkuva-Juha","Kohiseva-Kuisma","Hirviö-Hanna","Ryminä-Risto","Kirsikkapuu-Kaisa","Kiljukaula-Kalle","Riitelevä-Raija","Topakka-Tuula","Tähtitieteellinen-Timo"
        };

        public Enemies()
        {
            this.colors.Remove(ConsoleColor.Black);
            this.colors.Remove(ConsoleColor.Green);
        }

        public List<Enemy> GetEnemyListByLevel(int lvl, int howMany)
        {
            List<Enemy> enemies = new List<Enemy>();
            List<string> randomizedNameList = new List<string>(enemyNames.OrderBy(item => rand.Next()));
            List<string> randomizedDescriptionList = new List<string>(enemyDescriptions.OrderBy(item => rand.Next()));

            for (int i = 0; i < howMany; i++)
            {
                ConsoleColor color = this.colors[rand.Next(0, this.colors.Count)];
                string name = randomizedNameList[rand.Next(0, randomizedNameList.Count)];
                enemies.Add(new Enemy(name, randomizedDescriptionList[rand.Next(0, randomizedDescriptionList.Count)], new Position(rand.Next(1, gc.Map.Width), rand.Next(1, gc.Map.Height)), name[0], color, 300 + 300 * lvl, 10 + 10 * lvl, lvl));
            }
            return enemies;
        }
    }
}