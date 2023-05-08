using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDV_Test_Cards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cards = new CardDeck();
            List<string> currentDeck = new List<string>();

            Console.WriteLine("\nВыйти из программы - ввести \"0\"");
            Console.WriteLine("Создать колоду - ввести \"1\"");
            Console.WriteLine("Удалить колоду по имени - ввести \"2\"");
            Console.WriteLine("Получить список названий колод - ввести \"3\"");
            Console.WriteLine("Перетасовать колоду - ввести \"4\"");
            Console.WriteLine("Получить колоду по имени - ввести \"5\"");
            Console.WriteLine("Вывести полученную колоду - ввести \"6\"");

            while (true)
            {

                int actionChoice = 0;
                while (true) //цикл проверки числа
                {
                    Console.WriteLine("Ввод желаемой команды:");
                    if ((int.TryParse(Console.ReadLine(), out int x)) && (x >= 0) && (x < 7)) //условие проверки ввода
                    {
                        actionChoice = x;
                        break; //выход из цикла проверки
                    }
                    Console.WriteLine("Введено некорректное значение!");
                }

                if (actionChoice == 0) //выход из программы
                    break;

                if (actionChoice == 1) //создать колоду
                {
                    Console.WriteLine("Введите имя и количество карт (через enter)");
                    string name = Console.ReadLine();
                    //bool creationTime = true;
                    int deckSize = Convert.ToInt32(Console.ReadLine()); //можно тоже добавить проверку на ввод

                    cards.CreateDeck($"{name}", deckSize);
                    continue;
                }

                if (actionChoice == 2) //удалить колоду
                {
                    Console.WriteLine("Введите имя колоды");
                    cards.DeleteDeck(Console.ReadLine());
                }

                if (actionChoice == 3) //названия колод
                {
                    cards.GetNamesDecks();
                    continue;
                }

                if (actionChoice == 4) //перетасовать
                {
                    Console.WriteLine("Введите имя колоды и способ (1 - простой, 2 - настоящий)");
                    cards.Shuffle(Console.ReadLine(), Convert.ToInt32(Console.ReadLine()));
                    continue;
                }

                if (actionChoice == 5) //получить колоду по имени
                {
                    Console.WriteLine("Введите имя колоды и состояние (1 - текущее, 2 - сортированная, 3 - перемешанная)");
                    currentDeck = cards.GetDeck(Console.ReadLine(), Convert.ToInt32(Console.ReadLine()));
                    continue;
                }

                if (actionChoice == 6) //вывести полученную колоду (проверка 5-го)
                {
                    if (currentDeck.Count > 0)
                    {
                        cards.ShowDeck(currentDeck);
                    }
                    else
                    {
                        Console.WriteLine("Колода не выбрана");
                    }
                    continue;
                }
            }

            Console.WriteLine("Завершение");
            Console.ReadKey();

        }
    }
}
