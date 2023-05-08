using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDV_Test_Cards
{
    internal class CardDeck
    {
        List<List<string>> decks = new List<List<string>>(); //список для готовых колод
        //Можно было бы сделать словарь с названием колоды и самой колоды, чтобы каждый раз заново
        //не искать расположение колоды, но я это не сделал :/

        static Random rnd = new Random(900);

        public void CreateDeck(string nameDeck, int deckSize) //метод создания колоды (1)
        {
            for (int i = 0; i < decks.Count; i++) //цикл поиска
            {
                if (decks[i][0] == nameDeck) //соответственно, получим первую колоду, если имена одинаковы
                {
                    Console.WriteLine("Уже есть колода с таким именем");
                    return;
                }
            }
            if (deckSize != 52 && deckSize != 36)
            {
                Console.WriteLine("Несоответствующий размер (надо 52 или 36)");
                return;
            }

            List<string> cards = new List<string>(); //список для одной колоды

            List<string> fullSuitCard = new List<string>() { "CLUBS", "DIAMONDS", "HEARTS", "SPADES" };

            List<string> fullValueCard = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "JACK", "QUEEN", "KING", "ACE" };

            int iterForSmallDeck = 0;
            if (deckSize == 36) //если в колоде будет 36 карт
            {
                iterForSmallDeck = 4;
            }

            for (int i = 0; i < 4; i++) //заполнение колоды карт мастями
            {
                for (int j = 0; j < 13 - iterForSmallDeck; j++)
                {
                    cards.Add($"{fullSuitCard[i]},");
                }
            }

            for (int i = 0; i < deckSize; i++) //добавление мастям достоинства карт
            {
                cards[i] += $"{fullValueCard[(i % (13 - iterForSmallDeck)) + iterForSmallDeck]}"; //+учет 36 карт
            }

            cards.Insert(0, $"{nameDeck}"); //добавим название созданной колоде

            decks.Add(cards);
        }

        public void DeleteDeck(string nameDeck) //удалить колоду по имени (2)
        {
            for (int i = 0; i < decks.Count; i++)
            {
                if (decks[i][0] == nameDeck) //ищем
                {
                    decks.RemoveAt(i); //соответственно, удалятся все колоды с одинаковым названием (я не знаю, как надо: оставлю так)
                    i--;
                }
            }

            Console.WriteLine("Нет такой колоды");
            return;
        }

        public void GetNamesDecks() //получить имена всех колод (3)
        {
            //вот под "получить" имена колод - не совсем понял. В целом, чтобы иметь некий массив с ними - сделать нетрудно. Но оставлю так
            for (int i = 0; i < decks.Count; i++)
            {
                Console.WriteLine(decks[i][0]); //вывод имен колод
            }
        }

        public void Shuffle(string nameDeck, int simpleShuf) //перемешать колоду (4)
        {
            List<string> deckForShuffle = new List<string>();
            int numberDeck = -1; //запомними начальную колоду

            for (int i = 0; i < decks.Count; i++)
            {
                if (decks[i][0] == nameDeck) //поиск
                {
                    deckForShuffle = decks[i];
                    numberDeck = i;
                }
            }
            if (numberDeck == -1) //если не нашли колоду
            {
                Console.WriteLine("Нет такой колоды");
                return;
            }

            int rightBor = deckForShuffle.Count;
            int leftBor = 1;

            List<string> newDeck = new List<string>(); //ну вот надо еще один список)
            newDeck.Add(deckForShuffle[0]);

            if (simpleShuf == 1)
            {
                List<bool> arrForUniq = new List<bool>();
                arrForUniq.Add(false);
                for (int i = leftBor; i < rightBor; i++)
                {
                    arrForUniq.Add(true); //сначала от 0 до rightBor все true
                }

                for (int i = leftBor; i < rightBor; i++)
                {
                    int pos = rnd.Next(leftBor, rightBor);
                    if (arrForUniq[pos] == false) //если "отключен", то шаг назад
                    {
                        i--;
                        continue;
                    }
                    else //иначе же, отключаем и вставляем в новый
                    {
                        arrForUniq[pos] = false;
                        newDeck.Add(decks[numberDeck][pos]);
                    }
                }
                decks[numberDeck] = newDeck;
            }

            if (simpleShuf == 2)
            {
                for (int i = 1; i < deckForShuffle.Count; i++)
                {
                    newDeck.Add($"{deckForShuffle[i]}");
                }

                for (int i = 0; i < 30; i++)
                {
                    List<string> temporaryCards = new List<string>();

                    int lCut = rnd.Next(leftBor, rightBor / 2);
                    int rCut = rnd.Next(rightBor / 2, rightBor);

                    for (int j = lCut; j < rCut; j++)
                    {
                        temporaryCards.Add(newDeck[j]);
                    }

                    newDeck.RemoveRange(lCut, (rCut - lCut));

                    int posForInsert = rnd.Next(leftBor, newDeck.Count);

                    for (int j = 0; j < temporaryCards.Count; j++)
                    {
                        newDeck.Insert(posForInsert, temporaryCards[j]);
                    }
                }
                decks[numberDeck] = newDeck;
            }

        }

        public List<string> GetDeck(string nameDeck, int status) //получить колоду по имени (5)
        {
            for (int i = 0; i < decks.Count; i++) //цикл поиска
            {
                if (decks[i][0] == nameDeck) //соответственно, получим первую колоду, если имена одинаковы
                {


                    if (status == 1)
                    {
                        return decks[i]; //в текущем состоянии
                    }
                    if (status == 2)
                    {
                        CreateDeck("ForDelete", decks[i].Count - 1);

                        for (int j = 1; j < decks[i].Count; j++)
                        {
                            decks[i][j] = decks[decks.Count - 1][j];
                        }
                        DeleteDeck("ForDelete");
                        return decks[i];
                    }
                    if (status == 3)
                    {
                        Console.WriteLine("Введите способ перемешивания (1 или 2)");
                        Shuffle(decks[i][0], Convert.ToInt32(Console.ReadLine()));
                        return decks[i];
                    }
                }

            }

            Console.WriteLine("Нет такой колоды");
            return null;
        }

        public void ShowDeck(List<string> deckForShow) //вывести в консоль колоду (6)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nDeck - {deckForShow[0]}:");

            for (int i = 1; i < deckForShow.Count; i++)
            {
                string str = deckForShow[i];
                if (Convert.ToString(str[0]) == "C" || Convert.ToString(str[0]) == "S")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                if (Convert.ToString(str[0]) == "D" || Convert.ToString(str[0]) == "H")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine($"{i} - {deckForShow[i]}");
            }
            Console.ResetColor();
        }
    }
}
