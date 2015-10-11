using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _2014._03._02_BlackJack
{
    interface ICardeble
    {
        Card GetCard();
        bool SetCard(Card card);
        int Count();
    }
    class Card
    {
        enum Suit { Hearts = 3, Diamonds, Clubs, Spades };
        enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
        int[] score = new[] { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        Suit suit = Suit.Hearts;
        Rank rank = Rank.Ace;

        public int GetScore()
        {
            return score[(int)this.rank];
        }
        public Card(int i)
        {
            int shfl = i / 13;
            suit += shfl;
            shfl = i % 13;
            rank += shfl;
        }
        public int ToString()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.WriteLine(" _____");
            Console.SetCursorPosition(left, top + 1);
            Console.WriteLine("|" + (char)(int)suit + "    |");
            Console.SetCursorPosition(left, top + 2);
            Console.WriteLine("|     |");
            Console.SetCursorPosition(left, top + 3);
            Console.WriteLine("|     |");
            Console.SetCursorPosition(left, top + 4);
            Console.Write("|" + rank);
            Console.CursorLeft = left + 6;
            Console.WriteLine("|");
            Console.SetCursorPosition(left, top + 5);
            Console.WriteLine("|     |");
            Console.SetCursorPosition(left, top + 6);
            Console.WriteLine("|     |");
            Console.SetCursorPosition(left, top + 7);
            left = Console.CursorLeft + 7;
            Console.WriteLine("|____" + (char)(int)suit + "|");
            //Thread.Sleep(1000);
            return left;
        }
    }
    class Deck : ICardeble
    {
        Random rand = new Random();
        int count;
        int div;

        public int Div
        {
            get { return div; }
            set { div = value; }
        }

        public int Count()
        {
            return count;
        }
        List<Card> cards;
        public Deck()
        {
            count = 0;
            cards = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                cards.Add(new Card(i));
                count++;
            }
            Shuffle();
        }
        public void Shuffle()
        {
            for (int i = 0; i < 100000; i++)
            {
                cards.Reverse(rand.Next(0, count - 1), 2);
            }
        }
        public void TestPrint()
        {
            for (int i = 0; i < count; i++)
            {
                cards[i].ToString();
            }
        }
        public Card GetCard()
        {
            if (count > div)
            {
                Card tmp = cards.Last();
                cards.RemoveAt(count - 1);
                count--;
                return tmp;
            }
            return null;
        }
        public bool SetCard(Card card)
        {
            cards.Add(card);
            count++;
            return true;
        }
    }
    class Player : ICardeble
    {
        static int kol = 0;
        int count;

        public int Count()
        {
            return count;
        }
        public int Kol
        {
            get { return kol; }
        }
        static public int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        int bet;

        public int Bet
        {
            get { return bet; }
            set { bet = value; }
        }
        List<Card> cards;

        internal List<Card> Cards
        {
            get { return cards; }
            set { cards = value; }
        }
        public Player()
        {
            kol++;
            count = 0;
            //score = 0;
            cards = new List<Card>();
        }
        public Player(int scr)
        {
            kol++;
            count = 0;
            score = scr;
            cards = new List<Card>();
        }
        public Card GetCard()
        {
            Card tmp = cards.Last();
            cards.RemoveAt(count - 1);
            count--;
            return tmp;
        }
        public bool SetCard(Card card)
        {
            if (card != null)
            {
                cards.Add(card);
                count++;
                return true;
            }
            else { return false; }
        }
        public int GetScore()
        {
            int aceCount = 0;
            int get = 0;
            for (int i = 0; i < count; i++)
            {
                get += cards[i].GetScore();
                if (cards[i].GetScore() == 11) { aceCount++; }
            }
            if (get > 21)
            {
                while (aceCount > 0 && get > 21)
                {
                    get -= 10;
                    aceCount--;
                }
            }
            return get;
        }
        public void Print()
        {
            for (int i = 0; i < count; i++)
            {
                cards[i].ToString();
            }
        }
        public bool Upped(ICardeble otboi)
        {
            if (GetScore() > 21)
            {
                while (count > 0)
                {
                    otboi.SetCard(this.GetCard());
                }
                return true;
            }
            return false;
        }
    }
    class Computer : ICardeble
    {
        int count;

        public int Count()
        {
            return count;
        }
        List<Card> cards;

        internal List<Card> Cards
        {
            get { return cards; }
            set { cards = value; }
        }
        public Computer()
        {
            count = 0;
            cards = new List<Card>();
        }
        public Card GetCard()
        {
            Card tmp = cards.Last();
            cards.RemoveAt(count - 1);
            count--;
            return tmp;
        }
        public bool SetCard(Card card)
        {
            if (card != null)
            {
                cards.Add(card);
                count++;
                return true;
            }
            else { return false; }
        }
        public void Print()
        {
            for (int i = 0; i < count; i++)
            {
                cards[i].ToString();
            }
        }
        public int GetScore()
        {
            int aceCount = 0;
            int get = 0;
            for (int i = 0; i < count; i++)
            {
                get += cards[i].GetScore();
                if (cards[i].GetScore() == 11) { aceCount++; }
            }
            if (get > 21)
            {
                while (aceCount > 0 && get > 21)
                {
                    get -= 10;
                    aceCount--;
                }
            }
            return get;
        }
    }
    class Otboi : ICardeble
    {
        int count;

        public int Count()
        {
            return count;
        }
        List<Card> cards;
        public Otboi()
        {
            count = 0;
            cards = new List<Card>();
        }
        public Card GetCard()
        {
            Card tmp = cards.Last();
            cards.RemoveAt(count - 1);
            count--;
            return tmp;
        }
        public bool SetCard(Card card)
        {
            cards.Add(card);
            count++;
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region init
            bool splited = false;
            bool surrended = false;
            bool blackJack = false;
            bool doubleSplit = false;
            string tmp;
            int tmpI;
            string divTmp;
            int div;
            int num = 0;
            bool finish = false;
            bool firstCompCard = false;
            Deck deck= new Deck();
            Player[] players = new Player[6];
            players[0] = new Player(100);
            Computer computer = new Computer(); 
            Otboi otboi = new Otboi();
            //deck.TestPrint();
            Console.WriteLine("Разделите колоду (от 13 до 18)");
            //divTmp = Console.ReadLine();
            //div = int.Parse(divTmp);
            div = 14;
            deck.Div = div;
            #endregion
            do
            {
                #region first
                firstCompCard = false;
                surrended = false;
                finish = false;
                splited = false;
                Console.WriteLine("Делайте ставку");
                //tmp = Console.ReadLine();
                //players[0].Bet = int.Parse(tmp);
                players[0].Bet = 10;
                players[0].Score -= players[0].Bet;
                if (!players[0].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[0].SetCard(deck.GetCard()); }
                Print(firstCompCard, computer, players);
                if (!computer.SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); computer.SetCard(deck.GetCard()); }
                Print(firstCompCard, computer, players);
                if (!players[0].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[0].SetCard(deck.GetCard()); }
                Print(firstCompCard, computer, players);
                if (!computer.SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); computer.SetCard(deck.GetCard()); }
                Print(firstCompCard, computer, players);
                #endregion
                #region insurance
                if (computer.Cards[1].GetScore() == 11 && (players[0].GetScore() != 21))
                {
                    Console.WriteLine("Страховка? (1 - да; 2 - нет)");
                    tmp = Console.ReadLine();
                    tmpI = int.Parse(tmp);
                    if (tmpI == 1)
                    {
                        Player.score -= (players[0].Bet / 2);
                        players[0].Bet += (players[0].Bet / 2);
                        firstCompCard = true;
                        if (computer.Cards[0].GetScore() == 10)
                        {
                            Print(firstCompCard, computer, players);
                            finish = true;
                            surrended = true;
                            Console.WriteLine("Loose");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            players[0].Bet = (players[0].Bet * 2 / 3);
                            Print(firstCompCard, computer, players);
                        }
                    }
                    if (tmpI == 2)
                    {
                        firstCompCard = true;
                        Print(firstCompCard, computer, players);
                        if (computer.Cards[0].GetScore() == 10)
                        {
                            finish = true;
                            surrended = true;
                            Console.WriteLine("Loose");
                            Thread.Sleep(1000);
                            //players[0] = null;
                        }
                    }
                }
                #endregion
                while (num < players.Count() && !finish && !surrended)
                {
                    if (players[num] != null)
                    {
                        #region blackJack
                        if (players[num].Count() == 2 && players[num].GetScore() == 21 && !splited)
                        {
                            if (computer.Cards[1].GetScore() == 11)
                            {
                                Console.WriteLine("Равные деньги? (1 - да; 2 - нет)");
                                tmp = Console.ReadLine();
                                tmpI = int.Parse(tmp);
                                if (tmpI == 1)
                                {
                                    players[0].Score += 2 * players[num].Bet;
                                    finish = true;
                                    Console.WriteLine("Win");
                                    Thread.Sleep(3000);
                                    blackJack = true;
                                    break;
                                }
                                if (tmpI == 2)
                                {
                                    firstCompCard = true;
                                    Print(firstCompCard, computer, players);
                                    if (computer.Cards[0].GetScore() == 10)
                                    {
                                        players[0].Score += players[0].Bet;
                                        finish = true;
                                        Console.WriteLine("Ничья");
                                        Thread.Sleep(3000);
                                        blackJack = true;
                                        break;
                                    }
                                    else
                                    {
                                        blackJack = true;
                                        Console.WriteLine("Win");
                                        Thread.Sleep(3000);
                                        Player.score += (players[0].Bet + players[0].Bet * 3 / 2);
                                    }
                                }
                            }
                            else
                            {
                                Player.score += (players[0].Bet + players[0].Bet * 3 / 2);
                                finish = true;
                                blackJack = true;
                                Console.WriteLine("Win");
                                Thread.Sleep(3000);
                                break;
                            }
                        }
                        #endregion
                        #region split
                        if (players[num].Count() == 2 && players[num].Cards[0].GetScore() == players[num].Cards[1].GetScore() && players[num].Cards[0].GetScore() != 11)
                        {
                            Console.WriteLine("Split (1 - yes, 2 - no)?");
                            tmp = Console.ReadLine();
                            tmpI = int.Parse(tmp);
                            if (tmpI == 1)
                            {
                                if (players[1] == null)
                                {
                                    players[1] = new Player();
                                    players[1].Bet = players[0].Bet;
                                    Player.score -= players[0].Bet;
                                    players[1].SetCard(players[num].GetCard());
                                    if (!players[1].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[1].SetCard(deck.GetCard()); }
                                    if (!players[num].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[num].SetCard(deck.GetCard()); }
                                }
                                else
                                {
                                    if (players[2] == null)
                                    {
                                        players[2] = new Player();
                                        players[2].Bet = players[0].Bet;
                                        Player.score -= players[0].Bet;
                                        players[2].SetCard(players[num].GetCard());
                                        if (!players[2].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[2].SetCard(deck.GetCard()); }
                                        if (!players[num].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[num].SetCard(deck.GetCard()); }
                                    }
                                    else
                                    {
                                        players[3] = new Player();
                                        players[3].Bet = players[0].Bet;
                                        Player.score -= players[0].Bet;
                                        players[3].SetCard(players[num].GetCard());
                                        if (!players[3].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[3].SetCard(deck.GetCard()); }
                                        if (!players[num].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[num].SetCard(deck.GetCard()); }
                                    }
                                }
                                splited = true;
                            }
                            doubleSplit = true;
                            Print(firstCompCard, computer, players);
                        }
                        else
                        {
                            doubleSplit = false;
                            if (players[num].Count() == 2 && players[num].Cards[0].GetScore() == players[num].Cards[1].GetScore() && players[num].Cards[0].GetScore() == 11 && !splited)
                            {
                                Console.WriteLine("Split (1 - yes, 2 - no)?");
                                tmp = Console.ReadLine();
                                tmpI = int.Parse(tmp);
                                if (tmpI == 1)
                                {
                                    splited = true;
                                    players[num + 1] = new Player();
                                    players[num + 1].Bet = players[num].Bet;
                                    players[num].Score -= players[num].Bet;
                                    players[num + 1].SetCard(players[num].GetCard());
                                    if (!players[num].SetCard(deck.GetCard())) { ToDeck(deck, otboi); players[num].SetCard(deck.GetCard()); }
                                    if (!players[num + 1].SetCard(deck.GetCard())) { ToDeck(deck, otboi); players[num + 1].SetCard(deck.GetCard()); }
                                    Print(firstCompCard, computer, players);
                                    finish = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        if (!doubleSplit)
                        {
                            #region next
                            Console.WriteLine("Введите 1 - ещё; 2 - дальше; 3 - сбросить; 4 - удвоение");
                            tmp = Console.ReadLine();
                            tmpI = int.Parse(tmp);
                            switch (tmpI)
                            {
                                case 1:
                                    {
                                        do
                                        {
                                            if (!players[num].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[num].SetCard(deck.GetCard()); }
                                            Print(firstCompCard, computer, players);
                                            Thread.Sleep(1000);
                                            if (players[num].Upped(otboi)) { if (num != 0) { players[num] = null; } tmpI = 2; num++; break; }
                                            Print(firstCompCard, computer, players);
                                            Console.WriteLine("Введите 1 - ещё; 2 - дальше;");
                                            tmp = Console.ReadLine();
                                            tmpI = int.Parse(tmp);
                                        } while (tmpI == 1);
                                        //if (tmpI == 2) { num++; }
                                        break;
                                    }
                                case 2:
                                    {
                                        num++;
                                        break;
                                    }
                                case 3:
                                    {
                                        while (players[num].Count() > 0)
                                        {
                                            otboi.SetCard(players[num].GetCard());
                                        }
                                        Player.score += (players[num].Bet / 2);
                                        players[num].Bet = 0;
                                        if (num == 0)
                                        {
                                            finish = true;
                                            surrended = true;
                                        }
                                        num++;
                                        Print(firstCompCard, computer, players);
                                        break;
                                    }
                                case 4:
                                    {
                                        Player.score -= players[num].Bet;
                                        players[num].Bet *= 2;
                                        if (!players[num].SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); players[num].SetCard(deck.GetCard()); }
                                        Print(firstCompCard, computer, players);
                                        Thread.Sleep(1000);
                                        if (players[num].Upped(otboi)) { /*if (num == 0) { surrended = true; }*/ num++; break; }
                                        Print(firstCompCard, computer, players);
                                        num++;
                                        break;
                                    }
                                default: break;
                            }
                        }
                        #endregion
                    }
                    else { num++; }
                }
                firstCompCard = true;
                Print(firstCompCard, computer, players);       
                if (!surrended)
                {
                    #region comp
                    if (!blackJack)
                    {
                        firstCompCard = true;
                        while (computer.GetScore() < 17)
                        {
                            if (!computer.SetCard(deck.GetCard())) { div = ToDeck(deck, otboi); deck.Shuffle(); computer.SetCard(deck.GetCard()); }
                            Print(firstCompCard, computer, players);
                        }
                        num = 0;
                        for (int i = 0; i < players.Count(); i++)
                        {
                            int compScore = computer.GetScore();
                            if (players[i] != null)
                            {
                                if (players[i].GetScore() > 21)
                                {
                                    Console.WriteLine("Loose");
                                }
                                if (players[i].GetScore() > compScore && players[i].GetScore() < 22)
                                {
                                    Console.WriteLine("Win");
                                    players[0].Score += (players[i].Bet * 2);
                                }
                                if (players[i].GetScore() < compScore)
                                {
                                    if (compScore < 22 || players[i].GetScore() == 0)
                                    {
                                        Console.WriteLine("Loose");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Win");
                                        players[0].Score += (players[i].Bet * 2);
                                    }
                                }
                                if (players[i].GetScore() == compScore)
                                {
                                    Console.WriteLine("Ничья");
                                    players[0].Score += players[i].Bet;
                                }
                                num++;
                            }
                            else { num++; }
                            if (i != 0) { players[i] = null; }
                        }
                    }
                    Thread.Sleep(2000);
                    finish = true;
                }
                #endregion
                else
                {
                    Console.WriteLine("Loose");
                    Thread.Sleep(1000);
                }
                if (players[0] == null)
                {
                    players[0] = new Player();
                }
                num = 0;
                ToOtboi(otboi, computer, players);
                for (int i = 1; i < players.Count(); i++)
                {
                    players[i] = null;
                }
                Console.Clear();
                Console.SetCursorPosition(0, 0);
            } while (true);
        }
        static void Print(bool fCC, Computer comp, params Player[] plrs)
        {
            Console.Clear();
            int left = 0;
            Console.SetCursorPosition(5, 3);
            if (comp.Cards.Count() != 0)
            {
                if (fCC)
                {
                    comp.Cards[0].ToString();
                }
                else
                {
                    Console.WriteLine(" _____");
                    for (int i = 0; i < 6; i++)
                    {
                        Console.CursorLeft = 5;
                        Console.WriteLine("|" + "xxxxx" + "|");
                    }
                    Console.CursorLeft = 5;
                    Console.WriteLine(" _____");
                }
                for (int i = 1; i < comp.Cards.Count(); i++)
                {
                    Console.SetCursorPosition(5 + 7 * i + 2 * i, 3);
                    comp.Cards[i].ToString();
                }
                if (fCC)
                {
                    Console.CursorTop -= 1;
                    Console.CursorLeft += comp.Cards.Count() * 10 + 2;
                    Console.WriteLine(comp.GetScore());
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            //int t = plrs.Count();
            for (int j = 0; j < plrs.Count(); j++)
            {
                if (plrs[j] != null)
                {
                    Console.SetCursorPosition(5 + left, 13);
                    Console.WriteLine(plrs[j].Bet);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.SetCursorPosition(left + 5, 16);
                    for (int i = 1; i < plrs[j].Count() + 1; i++)
                    {
                        left = plrs[j].Cards[i - 1].ToString();
                        Console.SetCursorPosition(left + 2, 16);
                    }
                    Console.WriteLine(plrs[j].GetScore());
                    //left += 5;
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop + 9);
            Console.WriteLine("Money = " + plrs[0].Score);
        }
        static int ToDeck(Deck deck, params ICardeble[] unit)
        {
            int div = 0;
            string divTmp;
            for (int i = 0; i < unit.Count(); i++)
            {
                while (unit[i] != null && unit[i].Count() > 0)
                {
                    deck.SetCard(unit[i].GetCard());
                }
                deck.Shuffle();
                Console.WriteLine("Разделите колоду (от 13 до 18)");
                divTmp = Console.ReadLine();
                div = int.Parse(divTmp);
            }

            return div;
        }
        static void ToOtboi(Otboi otboi, Computer comp, params ICardeble[] unit)
        {
            for (int i = 0; i < unit.Count(); i++)
            {
                while (unit[i] != null && unit[i].Count() > 0)
                {
                    otboi.SetCard(unit[i].GetCard());
                }
                while (comp.Count() > 0)
                {
                    otboi.SetCard(comp.GetCard());
                }
            }
        }
    }
}