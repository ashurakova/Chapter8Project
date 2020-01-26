using System;
using System.Collections.Generic;

namespace BankLibrary.Collections
{
    public class Collections
    {
        public static void CallCommonInfoCollection()
        {
            List<CommonInfo> Info1 = new List<CommonInfo>();
            Info1.Add(new CommonInfo() { TypeInfo = "Операции с депозитным счетом не ограничены временными параметрами."});
            Info1.Add(new CommonInfo() { TypeInfo = "Операции со счетом до востребования ограничены 30 дневным сроком: только по истечение 30 дней допускается изменение счета."});
            Info1.Insert(0, new CommonInfo() { TypeInfo = "В нашем банке существует 2 типа счетов: депозитные счета и счета до востребования."});
            Info1.Insert(1, new CommonInfo() { TypeInfo = "А если кому-то что-то не нравится, никто никого не держит. Хорошего дня!"});
            Info1.RemoveAt(1);
            foreach (CommonInfo i in Info1)
            {
                Console.WriteLine(i.TypeInfo);
            }
        }

        public static void CallQueueCollection()
        {
            Dictionary<int, string> Language = new Dictionary<int, string>(5);
            Language.Add(1, "Русский");
            Language.Add(2, "English");
            Language.Add(3, "Deutsch");
            Language.Add(4, "Français");
            Language.Add(5, "Espaniol");
            Console.WriteLine("\n" + "Доступные языки:");
            foreach (KeyValuePair<int, string> keyValue in Language)
            {
                Console.WriteLine(keyValue.Key + "-" + keyValue.Value);
            }
            Console.WriteLine("\n" + "Введите цифру, чтобы выбрать язык:");
            bool IsInputAlive = false;
                while (!IsInputAlive)
                {
                    try
                    {   
                        int LangNum = Convert.ToInt32(Console.ReadLine());
                        if (LangNum == 1)
                        {
                            Console.WriteLine($"Вы выбрали {Language[1]} язык.");
                            IsInputAlive = true;
                        }
                        else if (LangNum > 1 && LangNum < 6)
                        {
                            Console.WriteLine($"\n"+"Извините, данный язык не поддерживается.");
                            Language.Remove(2);
                            Language.Remove(3);
                            Language.Remove(4);
                            Language.Remove(5);
                            Console.WriteLine("На данный момент доступны следующие языки:");
                            foreach (string p in Language.Values)
                            {
                                Console.WriteLine(p);
                            }
                            Console.WriteLine($"Данный язык выбран по умолчанию.");
                            IsInputAlive = true;
                        }
                        else
                        {
                            Console.WriteLine($"Язык под номером {LangNum} в списке не найден. Пожалуйста, выберите язык из списка предложенных: ");
                            foreach (KeyValuePair<int, string> keyValue in Language)
                            {
                                Console.WriteLine(keyValue.Key + "-" + keyValue.Value);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Введенное значение некорректно. Пожалуйста, для выбора языка введите соответствующую ему цифру:");
                        foreach (KeyValuePair<int, string> keyValue in Language)
                        {
                            Console.WriteLine(keyValue.Key + "-" + keyValue.Value);
                        }
                    }
                }
        }

        public static void CallDictionaryCollection()
        {
            Queue<PeopleQueue> people = new Queue<PeopleQueue>();
            people.Enqueue(new PeopleQueue() { Name = "Алёна" });
            people.Enqueue(new PeopleQueue() { Name = "Жора" });
            Console.WriteLine("\n"+"Количество посетителей в очереди на данный момент - {0}:", people.Count);
            foreach (PeopleQueue p in people)
            {
                Console.WriteLine(p.Name);
            }
            PeopleQueue firstPerson = people.Peek();
            bool deletionInFinished = false;
            while (!deletionInFinished)
            {
                Console.WriteLine($"{firstPerson.Name} - первый посетитель в очереди. Удалить данного посетителя из очереди?"+"\n"+"Пожалуйста, введите 'Да', чтобы удалить первого посетителя из очереди, или введите 'Нет', чтобы оставить первого посетителя в очереди.");
                string delQueue = Console.ReadLine();
                if (delQueue.ToLower() == "да")
                {
                    PeopleQueue deletedPers = people.Dequeue();
                    Console.WriteLine($"Посетитель {firstPerson.Name}  удален из очереди.");
                    deletionInFinished = true;
                }
                else if (delQueue.ToLower()=="нет")
                {
                    Console.WriteLine($"Посетитель {firstPerson.Name} не удален из очереди.");
                    deletionInFinished = true;
                }
                else
                {
                    Console.WriteLine("Введенное значение неккоректно.");
                }
            }
            Console.WriteLine("Посетители, находящиеся в очереди в данный момент:");
            foreach (PeopleQueue p in people)
            {
                Console.WriteLine(p.Name);
            }
            Console.WriteLine("");
        }
    }
}