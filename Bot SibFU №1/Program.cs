using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Telegram.Bot.Types.InputFiles;
using File = System.IO.File;

namespace Bot_SibFU__1
{
    class Program
    {
        static TelegramBotClient Bot;

        private static Quiz quiz;
        private static Dictionary<long, QuestionState> States;
        private static Dictionary<long, int> UserScores;
        private static Dictionary<long, int> UserCount;
        private static int x = 0;
        private static int y = 0;
        private static string StateFileName = "state.json";
        private static string ScoreFileName = "score.json";
        private static string CountFileName = "count.json";

        static void Main(string[] args)
        {
            quiz = new Quiz();
            if (File.Exists(StateFileName))
            {
                var json = File.ReadAllText(StateFileName);
                States = JsonConvert.DeserializeObject<Dictionary<long, QuestionState>>(json);
            }
            else
            {
                States = new Dictionary<long, QuestionState>();
            }

            if (File.Exists(CountFileName))
            {
                var json = File.ReadAllText(CountFileName);
                UserCount = JsonConvert.DeserializeObject<Dictionary<long, int>>(json);
            }
            else
            {
                UserCount = new Dictionary<long, int>();
            }

            if (File.Exists(ScoreFileName))
            {
                var json = File.ReadAllText(ScoreFileName);
                UserScores = JsonConvert.DeserializeObject<Dictionary<long, int>>(json);
            }
            else
            {
                UserScores = new Dictionary<long, int>();
            }

            Bot = new TelegramBotClient("1614455435:AAFSDrjpaQD8CcHvup9AOl0PpQu1A35opIU"); 

            Bot.OnMessage += BotOnMessageReceived;

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();
            Console.ReadLine();
            Console.ReadLine();
            var stateJson = JsonConvert.SerializeObject(States);
            File.WriteAllText(StateFileName, stateJson);

            var countJson = JsonConvert.SerializeObject(UserCount);
            File.WriteAllText(ScoreFileName, countJson);

            var scoreJson = JsonConvert.SerializeObject(UserScores);
            File.WriteAllText(ScoreFileName, scoreJson);

            Bot.StopReceiving();
            Console.ReadLine();
        }
        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            string imagePath = null;

            if (message == null || message.Type != MessageType.Text)
                return;

            string name = $"{message.From.FirstName}{message.From.LastName}";

            Console.WriteLine($"{DateTime.Now} ---- {name} - {message.Chat.Id} - отправил сообщение: '{message.Text}'");

            switch (message.Text)
            {
                case "/start":
                    Bot.OnMessage -= BotOnMessageTest;
                    string text = "Доброго времени суток!\n        Вас приветствует бот Психологической службы СФУ.\nНиже расположены разделы, где собрана полезная информация о психологическом консультировании, также Вы можете записаться на консультацию.\n        Для начала давайте поговорим о том, что такое психологическое консультирование.\n\n        Психологическое консультирование – это профессиональная помощь клиенту в поиске разрешения его проблемной ситуации.\n\n        В нашей службе работают профессиональные психологи, имеющие соответствующую подготовку, которые ориентированы на краткосрочную помощь.\n\n        Все консультации бесплатны.\n        Время консультации - 50 минут.\n        Мы придерживаемся правилу конфидициальности, поэтому не разглашаем данные о Вас за исключением некоторых моментов (они обсуждаются с психологом перед началом консультации).";
                    await Bot.SendTextMessageAsync(message.From.Id, text);


                    var replyKeyboard2 = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Наши психологи"),
                            new KeyboardButton("Запись")
                        },
                        new[]
                        {
                            new KeyboardButton("План безопасности"),
                            new KeyboardButton("Исследование своего состояния")
                        },
                        new[]
                        {
                            new KeyboardButton("Рекомендации к улучшению состояния")
                        },
                        new[]
                        {
                            new KeyboardButton("Телефон доверия"),
                        },
                        new[]
                        {
                            new KeyboardButton("Если есть готовность к длительной терапии")
                        },
                        new[]
                        {
                            new KeyboardButton("Про психиатрическую помощь"),
                            new KeyboardButton("Предложения к нам ")
                        },
                        new[]
                        {
                            new KeyboardButton("Литература")
                        }
                    });
                    await Bot.SendStickerAsync(message.From.Id, "CAACAgIAAxkBAAEBdo5gzuaUtGQyjmN0Kmt2cNIDoUvMvwACVA4AAoTCeEowxh4wlq9DmB8E", replyMarkup: replyKeyboard2);

                    break;
                case "/help":
                    Bot.OnMessage -= BotOnMessageTest;

                    break;
                case "Наши психологи":
                    Bot.OnMessage -= BotOnMessageTest;
                    //imagePath = Path.Combine(Environment.CurrentDirectory, "rrr.jpg");
                    //await using (var Stream = File.OpenRead(imagePath))
                    //{
                    //    await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    //}
                    //await Bot.SendTextMessageAsync(e.Message.From.Id,
                    //    @"Сыыыыыррр");
                    imagePath = Path.Combine(Environment.CurrentDirectory, "rrr.jpg");
                    await using (var Stream = File.OpenRead(imagePath))
                    {
                        await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    }
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Валерия Матушкина 
Начальник Психологической службы СФУ. 
Магистр по направлению «Психология развития».
Стаж работы: 10 лет. 
Профессиональные интересы: коммуникация, конфликты, переговоры, манипуляции, копинг-поведение, обучение взрослых, групповая работа и тренерство.
Индивидуальное психологическое консультирование: трудности в коммуникации, межличностные отношения, самоопределение.
Профессиональный подход в работе: краткосрочное психологическое консультирование
Любимая цитата: «Не бойтесь ввязаться в бой, гранаты поднесут потом».");
                    imagePath = Path.Combine(Environment.CurrentDirectory, "Gorlova.jpg");
                    await using (var Stream = File.OpenRead(imagePath))
                    {
                        await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    }
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Наталья Владимировна Горлова
Психолог Психологической службы СФУ. 
Психолог, преподаватель психологии, специализация - «Психологическое консультирование» Российский государственный гуманитарный университет, Институт психологии им. Л.С.Выготского (Москва); магистерская степень по консультированию МА in Counselling with distinction. 
Опыт консультативной работы — более 10 лет.
Профессиональный подход в работе: экзистенциальный подход, техники когнитивно-поведенческой психологии и другие. 
Индивидуальное психологическое консультирование: трудности во взаимоотношениях; трудности выбора, неуверенность, тревоги, страхи; эмоциональное напряжение в периоды кризисов; тяжелое эмоциональное состояние (утраты, горе и др.); агрессия, насилие, буллинг; трудности в профессиональном развитии (кризисы, эмоциональное выгорание, профориентация); личностное самоопределение, поиск себя; экзистенциальные вопросы (жизненные смыслы, цели, ценности, потеря смыслов, бессмысленность, поиск смыслов)
");
                    imagePath = Path.Combine(Environment.CurrentDirectory, "Varfolomeeva.jpg");
                    await using (var Stream = File.OpenRead(imagePath))
                    {
                        await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    }
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Варфоломеева Юлия Сергеевна
Психолог Психологической службы СФУ.
Магистр по направлению «Конфликтология».
Профессиональные интересы: конструктивная психология конфликта, коммуникация, переговоры, манипуляции, копинг-поведение, обучение взрослых, групповая работа и тренерство.
Индивидуальное психологическое консультирование: трудности в коммуникации, межличностные отношения, самоопределение.
Профессиональный подход в работе: гештальт-терапия, краткосрочное психологическое консультирование.
Любимая цитата: «Дорогу осилит идущий».
");
                    imagePath = Path.Combine(Environment.CurrentDirectory, "Ivannikova.jpg");
                    await using (var Stream = File.OpenRead(imagePath))
                    {
                        await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    }
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Иванникова Полина Вячеславовна
Психолог Психологической службы СФУ.
Магистр по направлению «Семейное консультирование».
Стаж работы: 3 года
Профессиональный подход в работе: краткосрочное консультирование
");
                    imagePath = Path.Combine(Environment.CurrentDirectory, "Lyubimova.jpg");
                    await using (var Stream = File.OpenRead(imagePath))
                    {
                        await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    }
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Любимова Мария Олеговна
Психолог Психологической службы СФУ
Бакалавр психологии ИППС СФУ.когнитивно - поведенческий терапевт
Профессиональные интересы: групповое и индивидуальное консультирование, тренинговые программы, психологическое просвещение
Индивидуальное психологическое консультирование: страх, тревога, проблемы взаимоотношений, прокрастинация
Профессиональный подход в работе: когнитивно - поведенческая терапия
Любимая цитата: «Вы - это вы, и то, что хорошо для кого - то другого, не обязательно хорошо или плохо для вас». 
");
                    imagePath = Path.Combine(Environment.CurrentDirectory, "Konovalova.jpg");
                    await using (var Stream = File.OpenRead(imagePath))
                    {
                        await Bot.SendPhotoAsync(e.Message.From.Id, new InputOnlineFile(Stream));
                    }
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Коновалова Валерия Олеговна
Психолог Психологической службы СФУ
Ббакалавр психологии ИППС СФУ
Профессиональный подход в работе: краткосрочное консультирование
Стаж работы: 3 
Любимая цитата: «Лавры одного лишь хотения есть сухие листья, которые никогда не зацветут»
");
                    break;
                case "Запись":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                        @" Записаться можно тут
https://vk.com/infops_sfu?w=app5688600_-171811692");
                    break;
                case "План безопасности":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                        @"Если Вы почувствовали себя поддавлено, этот раздел может Вам помочь.

Возьмите лист бумаги и ответьте на вопросы:

        1. По каким сигналам я понимаю, что чувствую себя плохо?
(сигналы могут включать в себя мысли, настроения или поведение)

        2. Что я могу сейчас сделать, чтобы отвлечься?

        3. Кому я могу позвонить? Кто может выслушать меня и поддержать?

        4. Я могу обратиться за срочной психологической помощью:
Телефон доверия: 8-800-200-01-22
Психологическая служба: +7(950)971-10-67");
                    break;
                case "Исследование своего состояния":
                    y = 1;
                    x = 0;
                    Bot.OnMessage += BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                        @"В данном разделе вам предлогаеться пройти небольшой тест Бека состоящий из 21 вопроса. Просим Вас отвечать честно и внимательно. 
В качестве ответа нужно написать цифру от 0 до 3. 
Для начала теста введите любое слово.");
                    break;
                case "Рекомендации к улучшению состояния":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"В непростом для нас эмоциональном состоянии иногда мы не знаем, чем себя занять, чтобы стало легче. Для вас мы предлагаем несколько рекомендаций, которые вы сможете применить.");
                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"1. дыхательные практики https://www.youtube.com/embed/kpw3PXLPFgQ");
                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"2. мышечная релаксация https://www.youtube.com/embed/zl24-hACr2A");
                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"3. хобби: подумайте, чем вам нравится заниматься ? Что приносит удовольствие ? А может вы давно хотели попробовать новое занятие ? Выберите для себя что - то определенное и 
4. фильм / сериал.По этой ссылке вы найдете подборку фильмов на разные темы https://www.tvzavr.ru
5. разговор с близким человеком.Подумайте, с кем вы можете сейчас провести время ? От кого почувствуете тепло и 
6. занятие спортом.Сходите на прогулку, на пробежку, сделайте зарядку.");
                    break;
                case "Телефон доверия":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                        @"В любой момент Вам захочется с кем-то поделиться своим состоянием. Вы можете позвонить на номера срочной психологической помощи:

  • Телефон доверия экстренной психологической помощи: 8-800-200-01-22 (круглосуточно)

  • Телефон доверия психологической службы в Красноярске (КГБУЗ «Красноярский краевой психоневрологический диспансер № 1»): 211-56-42 (рабочие часы: будни с 8:00 по 18:00, выходные и праздничные дни круглосуточно)

  • Телефон доверия МУ МВД России 'Красноярское': 245 - 96 - 46 

  • 8 499 901 0201 Региональная общественная организация Независимый Благотворительный Центр помощи пережившим сексуальное насилие «Сёстры»");
                    break;
                case "Если есть готовность к длительной терапии":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                        @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                    break;
                case "Про психиатрическую помощь":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                     @"Многие люди не обращаются за психиатрической помощью из-за определенных страхов. В этом разделе кратко освятим тему работы психиатра.  

        Кто такой психиатр? Какое отличие от психолога и психотерапевта?
        Для начала давайте поймем, что психолог – это специалист с высшим психологическим образованием. Он не имеет право выписывать лекарство и проводить психотерапию. 
        Психотерапевт – специалист с медицинским образованием, который прошел специальную переподготовку. 
        Психиатр – специалист с медицинским образованием, который консультирует, а также может выписывать медикаменты и ставить официальный диагноз. К психиатру обращаются в случаях, когда происходят «системные ошибки» в мышлении и поведении: скачки настроения, склонность к самоповреждению и суицидальные мысли, бредовые идеи и галлюцинации. 

        Что будет, если пойду к психиатру? 
        На приеме вы просто побеседуете. С первого раза никто диагноз не ставит, для этого собирается целая комиссия, которая как раз и определяет заболевание. 
        В государственных учреждениях существует 2 типа баз данных: для «легких» (для тех, кто не опасен для общества, никто не интересуется этой категорией, кроме спецслужб), для «учета» (для тех, кто потенциально может нарушить спокойствие). Человек попадает в базы, если обратился в государственное учреждение, частные клиники не обязаны фиксировать состояние обратившегося. 

        Меня поставят на учет? 
        Постановка на учет происходит только после госпитализации при постановке тяжелого 
психического или неврологического расстройств. Существует два типа учета: психиатрический и консультативный. Фактически психиатрический учет в ПНД означает, что человек находится под регулярным врачебным наблюдением. Когда диагноз установлен, врач назначает лечение, выстраивает метод терапии с учетом жалоб и соматического состояния пациента. На учет в психоневрологический диспансер ставятся люди с тяжелыми заболеваниями или состояниями, при которых нужен контроль специалиста. Консультативный учет такого наблюдения не предполагает. 

        Кто может узнать информацию о моем диагнозе? 
        Диагноз могут сообщить в трех случаях: самому пациенту, родственникам, если пациенту до 15 лет (до 18 лет — если состояние угрожает жизни и здоровью), и по прокурорскому запросу. Во всех остальных случаях, например когда работодатель запрашивает информацию, диспансер не сообщает диагноз, но информирует об отсутствии или наличии противопоказаний к работе. Важно, что перечень работодателей, которые имеют право запрашивать такую информацию, ограничен и строго регламентирован. Информацию о наличии противопоказаний имеют право запрашивать силовые организации, система общественного транспорта (РЖД, авиация, пассажирский городской транспорт), учебные заведения.");
                    break;
                case "Предложения к нам":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                        @"Доверие - важнейшая часть взаимодействия психолога и клиента. И мы хотим выразить огромную благодарность студентам, которые обращаются к нам за помощью, за их доверие.
https://docs.google.com/forms/d/e/1FAIpQLScu9QaKtrXrRO_z-LidR6RQnyJHeynYEpDpV0rhhhN7K9KCyQ/viewform");
                    break;
                case "Литература":
                    Bot.OnMessage -= BotOnMessageTest;
                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                    @"Для вас мы составили подборку литературы на различные темы: 

     Про расстройства личности 

        • “С ума сойти” Зайниев Антон, Варламова Дарья

        • “Победи депрессию, прежде чем она победит тебя” Роберт Лихи

        • “Свобода от тревоги” Роберт Лихи

        • Про насилие (физическое, сексуальное, эмоциональное, домашнее) 
         32 книги в открытом доступе о проблеме насилия (домашнего, сексуального, о гендерном вопросе):
https://library.nasiliu.net/

     Про созависимость 
        • «Женщины, которые любят слишком сильно» Р. Норвуд. В книге рассматриваются причины, по которым женщины выбирают партнеров, которые не отвечают им взаимностью. Как желание любить и сама любовь становятся страстью, зависимостью, привычкой, болезнью.

        • «Спасать или спасаться? Как избавитьcя от желания постоянно опекать других и начать думать о себе» М. Битти. Книга про стремление контролировать жизнь партнера, растворяться в нем, забыв о себе. Про созависимость, признаки и рекомендации. 

     Про коммуникацию
        • «Не рычите на собаку» Карен Прайор. (Книга в бихевиористском стиле про то, как на поведенческом уровне можно вносить изменения для построения отношений, 
взаимодействия с людьми).

        • «Мастерство общения Как найти общий язык с кем угодно» Пол МакГи. (В книге много примеров, описание ошибок во взаимодействии с людьми и того, как эти искажения могут влиять на коммуникацию)

        • «Как разговаривать с кем угодно» Марк Роудз. (В книге в большей степени не о коммуникации, а об общении и о старах. Скореее это про страх вступать во взаимодействие.)

        • «Договориться можно обо всем» Гевин Кеннеди (Книге про ситуацию продаж, но она хороша тем, что там рассказывается как правильно упокавать свой инетерс в словесную форму)

        • «Как побороть застенчивость» Ф. Зимбардо (В книге описаны техники по преодолению застенчивости и управлении эмоциями во взаимодействии в целом)

        • «Погодите, как вы сказали?» Д. Райан (О книге хорошие отзывы. В книге в основном делается акцент на умении задавать вопросы (и в ситуации взаимодействия и вопросы риторического характера для обсуждения ситуации, понимания ее границ и т.д)

        • «Искусство объяснять» Ли Лефевр (Как и многие книги МИФа хорошо иллюстрирована, есть схемы, но и много лишнего. Хороша для визуального представления о коммуникации, о возможных помехах)
");
                    break;

                case "/keyboard":
                    Bot.OnMessage -= BotOnMessageTest;
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Наши психологи"),
                            new KeyboardButton("Запись")
                        },
                        new[]
                        {
                            new KeyboardButton("План безопасности") ,
                            new KeyboardButton("Исследование своего состояния")
                        },
                        new[]
                        {
                            new KeyboardButton("Рекомендации к улучшению состояния")
                        },
                        new[]
                        {
                            new KeyboardButton("Телефон доверия"),
                        },
                        new[]
                        {
                            new KeyboardButton("Если есть готовность к длительной терапии")
                        },
                        new[]
                        {
                            new KeyboardButton("Про психиатрическую помощь"),
                            new KeyboardButton("Предложения к нам ")
                        },
                        new[]
                        {
                            new KeyboardButton("Литература")
                        }
                    });
                    break;
                default:
                    if (y < 1)
                    {
                        await Bot.SendTextMessageAsync(e.Message.From.Id,
                            @"Извини, я не разобрал твоё сообщение");
                    }
                    break;
            }
        }
        private static async void BotOnMessageTest(object sender, MessageEventArgs e)
        {
            var chatId = e.Message.Chat.Id;

            if (!States.TryGetValue(chatId, out var state))
            {
                state = new QuestionState();
                States[chatId] = state;
            }

            if (state.CurrentItem == null)
            {
                state.CurrentItem = quiz.NextQuestion();
            }
            var question = state.CurrentItem;
            var tryAnswer = e.Message.Text;
            if (e.Message.Text == "Исследование своего состояния")
            {
                NewRound(chatId);
                Bot.OnMessage -= BotOnMessageTest;
            }
            if (tryAnswer == question.Answer)
            {
                var fromId = e.Message.From.Id;
                quiz.NextIndex();
                state.CurrentItem = quiz.NextQuestion();
                x++;
                if (UserCount.ContainsKey(fromId))
                {
                    UserCount[fromId] += 0;
                }
                else
                {
                    UserCount[fromId] = +0;
                }
                if (UserScores.ContainsKey(fromId))
                {
                    UserScores[fromId] += 0;
                }
                else
                {
                    UserScores[fromId] = +0;
                }
                if (x >= 21)
                {
                    if (UserCount[fromId] <= 9)
                    {
                        y = 0;
                        await Bot.SendTextMessageAsync(chatId,
                            $"0-9 отсутствие депрессивных симптомов.");
                        await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                        UserCount[fromId] = 0;
                        UserScores[fromId] = 0;
                        await Bot.SendTextMessageAsync(e.Message.From.Id, @"В непростом для нас эмоциональном состоянии иногда мы не знаем, чем себя занять, чтобы стало легче. Для вас мы предлагаем несколько рекомендаций, которые вы сможете применить.");
                        await Bot.SendTextMessageAsync(e.Message.From.Id, @"1. дыхательные практики https://www.youtube.com/watch?v=kpw3PXLPFgQ ");
                        await Bot.SendTextMessageAsync(e.Message.From.Id, @"2. мышечная релаксация https://www.youtube.com/watch?v=zl24-hACr2A ");
                        await Bot.SendTextMessageAsync(e.Message.From.Id, @"3. хобби: подумайте, чем вам нравится заниматься ? Что приносит удовольствие ? А может вы давно хотели попробовать новое занятие ? Выберите для себя что - то определенное и 
4. фильм / сериал.По этой ссылке вы найдете подборку фильмов на разные темы https://www.tvzavr.ru
5. разговор с близким человеком.Подумайте, с кем вы можете сейчас провести время ? От кого почувствуете тепло и 
6. занятие спортом.Сходите на прогулку, на пробежку, сделайте зарядку.");
                        Bot.OnMessage -= BotOnMessageTest;
                    }
                    if (UserCount[fromId] > 9 && UserCount[fromId] <= 18)
                    {
                        y = 0;
                        await Bot.SendTextMessageAsync(chatId,
                            $"10-18 легкая депрессия.");
                        await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                        UserCount[fromId] = 0;
                        UserScores[fromId] = 0;
                        await Bot.SendTextMessageAsync(e.Message.From.Id,
                            @"Если Вы почувствовали себя поддавлено, этот раздел может Вам помочь.

Возьмите лист бумаги и ответьте на вопросы:

        1. По каким сигналам я понимаю, что чувствую себя плохо?
(сигналы могут включать в себя мысли, настроения или поведение)

        2. Что я могу сейчас сделать, чтобы отвлечься?

        3. Кому я могу позвонить? Кто может выслушать меня и поддержать?

        4. Я могу обратиться за срочной психологической помощью:
Телефон доверия: 8-800-200-01-22
Психологическая служба: +7(950)971-10-67");
                        await Bot.SendTextMessageAsync(e.Message.From.Id,
                            @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                        Bot.OnMessage -= BotOnMessageTest;
                    }
                    if (UserCount[fromId] > 18 && UserCount[fromId] <= 29)
                    {
                        y = 0;
                        await Bot.SendTextMessageAsync(chatId,
                            $"19- 29 умеренная депрессия, критический уровень.");
                        await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                        UserCount[fromId] = 0;
                        UserScores[fromId] = 0;
                        await Bot.SendTextMessageAsync(e.Message.From.Id,
                            @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                        Bot.OnMessage -= BotOnMessageTest;
                    }

                    if (UserCount[fromId] > 29 && UserCount[fromId] <= 63)
                    {
                        y = 0;
                        await Bot.SendTextMessageAsync(chatId,
                            $"30-63 явно выраженная депрессивная симптоматика.");
                        await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                        UserCount[fromId] = 0;
                        UserScores[fromId] = 0;
                        await Bot.SendTextMessageAsync(e.Message.From.Id,
                            @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                        Bot.OnMessage -= BotOnMessageTest;
                    }
                }
                if (x < 21)
                {
                    await Bot.SendTextMessageAsync(chatId, state.CurrentItem.Question);
                }
            }
            else
            {
                if (tryAnswer == question.Answer1)
                {
                    var fromId = e.Message.From.Id;
                    quiz.NextIndex();
                    state.CurrentItem = quiz.NextQuestion();
                    x++;
                    if (UserCount.ContainsKey(fromId))
                    {
                        UserCount[fromId] += 1;
                    }
                    else
                    {
                        UserCount[fromId] = +1;
                    }
                    if (UserScores.ContainsKey(fromId))
                    {
                        UserScores[fromId] += 1;
                    }
                    else
                    {
                        UserScores[fromId] = 1;
                    }
                    if (x >= 21)
                    {
                        if (UserCount[fromId] <= 9)
                        {
                            y = 0;
                            await Bot.SendTextMessageAsync(chatId,
                                $"отсутствие депрессивных симптомов.");
                            await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                            UserCount[fromId] = 0;
                            UserScores[fromId] = 0;
                            await Bot.SendTextMessageAsync(e.Message.From.Id,
                                @"Если Вы почувствовали себя поддавлено, этот раздел может Вам помочь.

Возьмите лист бумаги и ответьте на вопросы:

        1. По каким сигналам я понимаю, что чувствую себя плохо?
(сигналы могут включать в себя мысли, настроения или поведение)

        2. Что я могу сейчас сделать, чтобы отвлечься?

        3. Кому я могу позвонить? Кто может выслушать меня и поддержать?

        4. Я могу обратиться за срочной психологической помощью:
Телефон доверия: 8-800-200-01-22
Психологическая служба: +7(950)971-10-67");
                            Bot.OnMessage -= BotOnMessageTest;
                        }

                        if (UserCount[fromId] > 9 && UserCount[fromId] <= 18)
                        {
                            y = 0;
                            await Bot.SendTextMessageAsync(chatId,
                                $"легкая депрессия, астено-субдепрессивная симптоматика, м.б. у соматических больных или невротический уровень.");
                            await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                            UserCount[fromId] = 0;
                            UserScores[fromId] = 0;
                            await Bot.SendTextMessageAsync(e.Message.From.Id, @"В непростом для нас эмоциональном состоянии иногда мы не знаем, чем себя занять, чтобы стало легче. Для вас мы предлагаем несколько рекомендаций, которые вы сможете применить.");
                            await Bot.SendTextMessageAsync(e.Message.From.Id, @"1. дыхательные практики https://www.youtube.com/watch?v=kpw3PXLPFgQ ");
                            await Bot.SendTextMessageAsync(e.Message.From.Id, @"2. мышечная релаксация https://www.youtube.com/watch?v=zl24-hACr2A ");
                            await Bot.SendTextMessageAsync(e.Message.From.Id, @"3. хобби: подумайте, чем вам нравится заниматься ? Что приносит удовольствие ? А может вы давно хотели попробовать новое занятие ? Выберите для себя что - то определенное и 
4. фильм / сериал.По этой ссылке вы найдете подборку фильмов на разные темы https://www.tvzavr.ru
5. разговор с близким человеком.Подумайте, с кем вы можете сейчас провести время ? От кого почувствуете тепло и 
6. занятие спортом.Сходите на прогулку, на пробежку, сделайте зарядку.");
                            await Bot.SendTextMessageAsync(e.Message.From.Id,
                                @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                            Bot.OnMessage -= BotOnMessageTest;
                        }
                        if (UserCount[fromId] > 18 && UserCount[fromId] <= 29)
                        {
                            y = 0;
                            await Bot.SendTextMessageAsync(chatId,
                                $"умеренная депрессия, критический уровень.");
                            await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                            UserCount[fromId] = 0;
                            UserScores[fromId] = 0;
                            await Bot.SendTextMessageAsync(e.Message.From.Id,
                                @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                            Bot.OnMessage -= BotOnMessageTest;
                        }

                        if (UserCount[fromId] > 29 && UserCount[fromId] <= 63)
                        {
                            y = 0;
                            await Bot.SendTextMessageAsync(chatId,
                                $"явно выраженная депрессивная симптоматика, не исключена эндогенность");
                            await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                            UserCount[fromId] = 0;
                            UserScores[fromId] = 0;
                            await Bot.SendTextMessageAsync(e.Message.From.Id,
                                @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                            Bot.OnMessage -= BotOnMessageTest;
                        }
                    }
                    if (x < 21)
                    {
                        await Bot.SendTextMessageAsync(chatId, state.CurrentItem.Question);
                    }
                }
                else
                {
                    if (tryAnswer == question.Answer2)
                    {
                        var fromId = e.Message.From.Id;
                        quiz.NextIndex();
                        state.CurrentItem = quiz.NextQuestion();
                        x++;
                        if (UserCount.ContainsKey(fromId))
                        {
                            UserCount[fromId] += 2;
                        }
                        else
                        {
                            UserCount[fromId] = 2;
                        }
                        if (UserScores.ContainsKey(fromId))
                        {
                            UserScores[fromId] += 2;
                        }
                        else
                        {
                            UserScores[fromId] = 2;
                        }
                        if (x >= 21)
                        {
                            if (UserCount[fromId] <= 9)
                            {
                                y = 0;
                                await Bot.SendTextMessageAsync(chatId,
                                    $"отсутствие депрессивных симптомов.");
                                await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                UserCount[fromId] = 0;
                                UserScores[fromId] = 0;
                                await Bot.SendTextMessageAsync(e.Message.From.Id,
                                    @"Если Вы почувствовали себя поддавлено, этот раздел может Вам помочь.

Возьмите лист бумаги и ответьте на вопросы:

        1. По каким сигналам я понимаю, что чувствую себя плохо?
(сигналы могут включать в себя мысли, настроения или поведение)

        2. Что я могу сейчас сделать, чтобы отвлечься?

        3. Кому я могу позвонить? Кто может выслушать меня и поддержать?

        4. Я могу обратиться за срочной психологической помощью:
Телефон доверия: 8-800-200-01-22
Психологическая служба: +7(950)971-10-67");
                                Bot.OnMessage -= BotOnMessageTest;
                            }

                            if (UserCount[fromId] > 9 && UserCount[fromId] <= 18)
                            {
                                y = 0;
                                await Bot.SendTextMessageAsync(chatId,
                                    $"легкая депрессия, астено-субдепрессивная симптоматика, м.б. у соматических больных или невротический уровень.");
                                await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                UserCount[fromId] = 0;
                                UserScores[fromId] = 0;
                                await Bot.SendTextMessageAsync(e.Message.From.Id, @"В непростом для нас эмоциональном состоянии иногда мы не знаем, чем себя занять, чтобы стало легче. Для вас мы предлагаем несколько рекомендаций, которые вы сможете применить.");
                                await Bot.SendTextMessageAsync(e.Message.From.Id, @"1. дыхательные практики https://www.youtube.com/watch?v=kpw3PXLPFgQ ");
                                await Bot.SendTextMessageAsync(e.Message.From.Id, @"2. мышечная релаксация https://www.youtube.com/watch?v=zl24-hACr2A ");
                                await Bot.SendTextMessageAsync(e.Message.From.Id, @"3. хобби: подумайте, чем вам нравится заниматься ? Что приносит удовольствие ? А может вы давно хотели попробовать новое занятие ? Выберите для себя что - то определенное и 
4. фильм / сериал.По этой ссылке вы найдете подборку фильмов на разные темы https://www.tvzavr.ru
5. разговор с близким человеком.Подумайте, с кем вы можете сейчас провести время ? От кого почувствуете тепло и 
6. занятие спортом.Сходите на прогулку, на пробежку, сделайте зарядку.");
                                await Bot.SendTextMessageAsync(e.Message.From.Id,
                                    @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                                Bot.OnMessage -= BotOnMessageTest;
                            }
                            if (UserCount[fromId] > 18 && UserCount[fromId] <= 29)
                            {
                                y = 0;
                                await Bot.SendTextMessageAsync(chatId,
                                    $"умеренная депрессия, критический уровень.");
                                await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                UserCount[fromId] = 0;
                                UserScores[fromId] = 0;
                                await Bot.SendTextMessageAsync(e.Message.From.Id,
                                    @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                                Bot.OnMessage -= BotOnMessageTest;
                            }

                            if (UserCount[fromId] > 29 && UserCount[fromId] <= 63)
                            {
                                y = 0;
                                await Bot.SendTextMessageAsync(chatId,
                                    $"явно выраженная депрессивная симптоматика, не исключена эндогенность");
                                await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                UserCount[fromId] = 0;
                                UserScores[fromId] = 0;
                                await Bot.SendTextMessageAsync(e.Message.From.Id,
                                    @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                                Bot.OnMessage -= BotOnMessageTest;
                            }
                        }
                        if (x < 21)
                        {
                            await Bot.SendTextMessageAsync(chatId, state.CurrentItem.Question);
                        }
                    }
                    else
                    {
                        if (tryAnswer == question.Answer3)
                        {
                            var fromId = e.Message.From.Id;

                            quiz.NextIndex();
                            state.CurrentItem = quiz.NextQuestion();
                            x++;
                            if (UserCount.ContainsKey(fromId))
                            {
                                UserCount[fromId] += 3;
                            }
                            else
                            {
                                UserCount[fromId] = +3;
                            }
                            if (UserScores.ContainsKey(fromId))
                            {
                                UserScores[fromId] += 3;
                            }
                            else
                            {
                                UserScores[fromId] = 3;
                            }

                            if (x >= 21)
                            {
                                if (UserCount[fromId] <= 9)
                                {
                                    y = 0;
                                    await Bot.SendTextMessageAsync(chatId,
                                        $"отсутствие депрессивных симптомов.");
                                    await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                    UserCount[fromId] = 0;
                                    UserScores[fromId] = 0;
                                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                                        @"Если Вы почувствовали себя поддавлено, этот раздел может Вам помочь.

Возьмите лист бумаги и ответьте на вопросы:

        1. По каким сигналам я понимаю, что чувствую себя плохо?
(сигналы могут включать в себя мысли, настроения или поведение)

        2. Что я могу сейчас сделать, чтобы отвлечься?

        3. Кому я могу позвонить? Кто может выслушать меня и поддержать?

        4. Я могу обратиться за срочной психологической помощью:
Телефон доверия: 8-800-200-01-22
Психологическая служба: +7(950)971-10-67");
                                    Bot.OnMessage -= BotOnMessageTest;
                                }

                                if (UserCount[fromId] > 9 && UserCount[fromId] <= 18)
                                {
                                    y = 0;
                                    await Bot.SendTextMessageAsync(chatId,
                                        $"легкая депрессия, астено-субдепрессивная симптоматика, м.б. у соматических больных или невротический уровень.");
                                    await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                    UserCount[fromId] = 0;
                                    UserScores[fromId] = 0;
                                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"В непростом для нас эмоциональном состоянии иногда мы не знаем, чем себя занять, чтобы стало легче. Для вас мы предлагаем несколько рекомендаций, которые вы сможете применить.");
                                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"1. дыхательные практики https://www.youtube.com/watch?v=kpw3PXLPFgQ ");
                                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"2. мышечная релаксация https://www.youtube.com/watch?v=zl24-hACr2A ");
                                    await Bot.SendTextMessageAsync(e.Message.From.Id, @"3. хобби: подумайте, чем вам нравится заниматься ? Что приносит удовольствие ? А может вы давно хотели попробовать новое занятие ? Выберите для себя что - то определенное и 
4. фильм / сериал.По этой ссылке вы найдете подборку фильмов на разные темы https://www.tvzavr.ru
5. разговор с близким человеком.Подумайте, с кем вы можете сейчас провести время ? От кого почувствуете тепло и 
6. занятие спортом.Сходите на прогулку, на пробежку, сделайте зарядку.");
                                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                                        @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                                    Bot.OnMessage -= BotOnMessageTest;
                                }
                                if (UserCount[fromId] > 18 && UserCount[fromId] <= 29)
                                {
                                    y = 0;
                                    await Bot.SendTextMessageAsync(chatId,
                                        $"умеренная депрессия, критический уровень.");
                                    await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                    UserCount[fromId] = 0;
                                    UserScores[fromId] = 0;
                                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                                        @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                                    Bot.OnMessage -= BotOnMessageTest;
                                }

                                if (UserCount[fromId] > 29 && UserCount[fromId] <= 70)
                                {
                                    y = 0;
                                    await Bot.SendTextMessageAsync(chatId,
                                        $"явно выраженная депрессивная симптоматика, не исключена эндогенность");
                                    await Bot.SendTextMessageAsync(chatId, $"Колличество набранных баллов: {UserScores[fromId]}");
                                    UserCount[fromId] = 0;
                                    UserScores[fromId] = 0;
                                    await Bot.SendTextMessageAsync(e.Message.From.Id,
                                        @"
        В рамках нашей психологической службы мы не можем проводить длительную терапию, поэтому мы можем рекомендовать вам обратиться к нашим коллегам из частной практики. 

        https://vk.com/kate_psy когнитивный терапевт 

        https://vk.com/id7781016 клинический психолог 

        https://vk.com/id2342932 семейный психолог

        https://vitacentr24.ru/ Медицина для души (Психотерапевтическая,психологическая, психиатрическая, неврологическая помощь) ");
                                    Bot.OnMessage -= BotOnMessageTest;
                                }
                            }
                            if (x < 21)
                            {
                                await Bot.SendTextMessageAsync(chatId, state.CurrentItem.Question);
                            }
                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(chatId, state.DisplayQuestion);
                        }
                    }
                }
            }
        }
        public static void NewRound(long chatId)
        {
            if (!States.TryGetValue(chatId, out var state))
            {
                state = new QuestionState();
                States[chatId] = state;
            }
            state.CurrentItem = quiz.NextQuestion();
        }
    }
    public class Quiz
    {
        public List<QuestionItem> Questions { get; set; }
        private int count;
        public int CountForTest = 0;
        public Quiz(string path = "Test.txt")
        {
            string[] lines2 = {
"0 – Я не чувствую себя несчастным. \n1 – Я чувствую себя несчастным. \n2 – Я все время несчастен и не могу освободиться от этого чувства. \n3 – Я настолько несчастен и опечален, что не могу этого вынести.|0|1|2|3",
"0 – Думая о будущем, я не чувствую себя особенно разочарованным. \n1 – Думая о будущем, я чувствую себя разочарованным. \n2 – Я чувствую, что мне нечего ждать в будущем. \n3 – Я чувствую, что будущее безнадежно и ничего не изменится к лучшему.|0|1|2|3",
"0 – Я не чувствую себя неудачником. \n1 – Я чувствую, что у меня было больше неудач, чем у большинства других людей. \n2 – Когда я оглядываюсь на прожитую жизнь, все, что я вижу, это череды неудач. \n3 – Я чувствую себя полным неудачником.|0|1|2|3",
"0 – Я получаю столько же удовольствия от жизни, как и раньше. \n1 – Я не получаю столько же удовольствия от жизни, как и раньше. \n2 – Я не получаю настоящего удовлетворения от чего бы то ни было. \n3 – Я всем неудовлетворен, и мне все надоело.|0|1|2|3",
"0 – Я не чувствую себя особенно виноватым. \n1 – Довольно часто я чувствую себя виноватым. \n2 – Почти всегда я чувствую себя виноватым. \n3 – Я чувствую себя виноватым все время.|0|1|2|3",
"0 – Я не чувствую, что меня за что-то наказывают. \n1 – Я чувствую, что могу быть наказан за что-то. \n2 – Я ожидаю, что меня накажут. \n3 – Я чувствую, что меня наказывают за что-то.|0|1|2|3",
"0 – Я не испытываю разочарование в себе. \n1 – Я разочарован в себе. \n2 – Я внушаю себе отвращение. \n3 – Я ненавижу себя.|0|1|2|3",
"0 – У меня нет чувства, что я в чем-то хуже других. \n1 – Я самокритичен и признаю свои слабости и ошибки. \n2 – Я все время виню себя за свои ошибки. \n3 – Я виню себя за все плохое, что происходит.|0|1|2|3",
"0 – У меня нет мыслей о том, чтобы покончить с собой. \n1 – У меня есть мысли о том, чтобы покончить с собой, но я этого не делаю. \n2 – Я хотел бы покончить жизнь самоубийством. \n3 – Я бы покончил с собой, если бы представился удобный случай.|0|1|2|3",
"0 – Я плачу не больше, чем обычно. \n1 – Сейчас я плачу больше обычного. \n2 – Я теперь все время плачу. \n3 – Раньше я еще мог плакать, но теперь не смогу, даже если захочу.|0|1|2|3",
"0 – Сейчас я не более раздражителен, чем обычно. \n1 – Я раздражаюсь легче, чем раньше, даже по пустякам. \n2 – Сейчас я все время раздражен. \n3 – Меня уже ничто не раздражает, потому что все стало безразлично.|0|1|2|3",
"0 – Я не потерял интереса к другим людям. \n1 – У меня меньше интереса к другим людям, чем раньше. \n2 – Я почти утратил интерес к другим людям. \n3 – Я потерял всякий интерес к другим людям.|0|1|2|3",
"0 – Я способен принимать решения так же, как всегда. \n1 – Я откладываю принятие решений чаще, чем обычно. \n2 – Я испытываю больше трудностей в принятии решений, чем прежде. \n3 – Я больше не могу принимать каких-либо решений.|0|1|2|3",
"0 – Я не чувствую, что я выгляжу хуже, чем обычно. \n1 – Я обеспокоен, что выгляжу постаревшим и непривлекательным. \n2 – Я чувствую, что изменения, происходящие в моей внешности, сделали меня непривлекательным. \n3 – Я уверен, что выгляжу безобразным.|0|1|2|3",
"0 – Я могу работать так же, как раньше. \n1 – Мне надо приложить дополнительные усилия, чтобы начать что-либо делать. \n2 – Я с большим трудом заставляю себя что-либо сделать. \n3 – Я вообще не могу работать.|0|1|2|3",
"0 – Я могу спать так же хорошо, как и обычно. \n1 – Я сплю не так хорошо, как всегда. \n2 – Я просыпаюсь на 1-2 часа раньше, чем обычно и с трудом могу заснуть снова. \n3 – Я просыпаюсь на несколько часов раньше обычного и не могу снова заснуть.|0|1|2|3",
"0 – Я устаю не больше обычного. \n1 – Я устаю легче обычного. \n2 – Я устаю почти от всего того, что делаю. \n3 – Я слишком устал, чтобы делать что бы то ни было.|0|1|2|3",
"0 – Мой аппетит не хуже, чем обычно. \n1 – У меня не такой хороший аппетит, как был раньше. \n2 – Сейчас мой аппетит стал намного хуже. \n3 – Я вообще потерял аппетит.|0|1|2|3",
"0 – Если в последнее время я и потерял в весе, то очень немного. \n1 – Я потерял в весе более 2 кг. \n2 – Я потерял в весе более 4 кг. \n3 – Я потерял в весе более 6 кг.|0|1|2|3",
"0 – Я беспокоюсь о своем здоровье не больше, чем обычно. \n1 – Меня беспокоят такие проблемы, как различные боли, расстройства желудка, запоры. \n2 – Я настолько обеспокоен своим здоровьем, что мне даже трудно думать о чем-нибудь другом. \n3 – Я до такой степени обеспокоен своим здоровьем, что вообще ни о чем не могу думать.|0|1|2|3",
"0 – Я не замечал каких-либо изменений в моих сексуальных интересах. \n1 – Я меньше, чем обычно интересуюсь сексом. \n2 – Сейчас я намного меньше интересуюсь сексом. \n3 – Я совершенно утратил интерес к сексу.|0|1|2|3"
 };
            var lines = File.ReadAllLines(path);
            Questions = lines2
                .Select(line => line.Split('|'))
                .Select(line => new QuestionItem
                {
                    Question = line[0],
                    Answer = line[1],
                    Answer1 = line[2],
                    Answer2 = line[3],
                    Answer3 = line[4]
                })
                .ToList();
            count = Questions.Count;
            CountForTest = 0;
        }
        public QuestionItem NextQuestion()
        {
            if (CountForTest == 21)
            {
                CountForTest = 0;
            }
            var question = Questions[CountForTest];
            return question;
        }
        public int NextIndex()
        {
            CountForTest += 1;
            return CountForTest;
        }
    }
    public class QuestionItem
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
    }
    public class QuestionState
    {
        public QuestionItem CurrentItem { get; set; }
        public string DisplayQuestion => $"{CurrentItem.Question}";
    }
}