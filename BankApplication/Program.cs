using System;
using BankLibrary;
using System.Collections.Generic;
using System.Collections;


namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Коллекция №1
            List<CommonInfo> Info1 = new List<CommonInfo> ();
            Info1.Add(new CommonInfo() { TypeInfo = "Операции с депозитным счетом не ограничены временными параметрами." });
            Info1.Add(new CommonInfo() { TypeInfo = "Операции со счетом до востребования ограничены 30 дневным сроком: только по истечение 30 дней допускается изменение счета." });
            Info1.Insert(0, new CommonInfo() { TypeInfo = "В нашем банке существует 2 типа счетов: депозитные счета и счета до востребования." });
            Info1.Insert(1, new CommonInfo() { TypeInfo = "А если кому-то что-то не нравится, никто никого не держит. Хорошего дня!" });
            Info1.RemoveAt(1);
            foreach (CommonInfo i in Info1)
            {
                Console.WriteLine(i.TypeInfo);
            }

            // Коллекция №2
            Dictionary<int, string> Language = new Dictionary<int, string>(5);
            Language.Add(1, "Русский");
            Language.Add(2,"English");
            Language.Add(3, "Deutsch");
            Language.Add(4, "Français");
            Language.Add(5, "Espaniol");
            Console.WriteLine("Доступные языки:");
            foreach (KeyValuePair<int, string> keyValue in Language)
            {
                Console.WriteLine(keyValue.Key + "-" + keyValue.Value);
            }
            Console.WriteLine("Введите цифру, чтобы выбрать язык:");
            int LangNum = Convert.ToInt32(Console.ReadLine());
            if (LangNum==1)
            {
                Console.WriteLine ($"Вы выбрали {Language[1]} язык." );
            }
            else
            {
                Console.WriteLine($"Данный язык не поддерживается. Извините.");
                Language.Remove(2);
                Language.Remove(3);
                Language.Remove(4);
                Language.Remove(5);
                Console.WriteLine("На данный момент доступен только один язык:");
                foreach (string p in Language.Values)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine("Он выбран по умолчанию.");
            }

            //Коллекция №3
            Queue < PeopleQueue > people= new Queue<PeopleQueue>();
            people.Enqueue(new PeopleQueue() { Name = "Алёна" });
            people.Enqueue(new PeopleQueue() { Name="Жора"});
            
            Console.WriteLine("Сейчас в очереди {0} человека:", people.Count);
            foreach (PeopleQueue p in people)
            {
                Console.WriteLine(p.Name);
            }
            PeopleQueue firstPerson = people.Peek();
            Console.WriteLine($"{firstPerson.Name} - первый человек в очереди. Удалить ее из очереди?");
            string delQueue = Console.ReadLine();
            if (delQueue=="Да"||delQueue=="да"||delQueue=="Конечно")
            {
                PeopleQueue deletedPers = people.Dequeue();
            }
            Console.WriteLine("Сейчас в очереди {0} человека:", people.Count);
            foreach (PeopleQueue p in people)
            {
                Console.WriteLine(p.Name);
            }


            Bank<Account> bank = new Bank<Account>("ЮнитБанк");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen; // выводим список команд зеленым цветом
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("7. Сохранить информацию о счете");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                       

                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    // выводим сообщение об ошибке красным цветом
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета: 1. До востребования 2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                AddSumHandler,  // обработчик добавления средств на счет
                (o, e)=>Console.WriteLine(e.Message), // обработчик вывода средств
                (o, e) => Console.WriteLine(e.Message), // обработчик начислений процентов в виде лямбда-выражения
                CloseAccountHandler, // обработчик закрытия счета
                OpenAccountHandler); // обработчик открытия счета
            
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для вывода со счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму, чтобы положить на счет:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Введите id счета, который надо закрыть:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }
        // обработчики событий класса Account
        // обработчик открытия счета
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик добавления денег на счет
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик вывода средств
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Идем тратить деньги");
        }
        // обработчик закрытия счета
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
       
    }
   
}
