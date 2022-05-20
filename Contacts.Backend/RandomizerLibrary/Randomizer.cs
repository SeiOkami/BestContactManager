﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomizerLibrary
{
    public static class Randomizer
    {

        private readonly static Random random = new();

        private readonly static char[] symbolsMail = "abcdefghijklmnopqrstuvwxyz".ToArray();

        private readonly static string[] FirstWomanNames =
            {
                "Ава","Августа","Августина","Авдотья","Аврора","Агапия","Агата","Агафия","Агафья","Аглая","Агния","Агриппина",
                "Ада","Аделаида","Аделина","Аделия","Адель","Адриана","Азалия","Аида","Аксинья","Алана","Алевтина","Алекса",
                "Александра","Александрина","Алёна","Алеся","Алина","Алиса","Алла","Альба","Альбина","Аля","Амалия","Амелия",
                "Анастасия","Ангелина","Анжела","Анжелика","Анисья","Анна","Антонина","Анфиса","Аполлинария","Арабелла","Ариана",
                "Арина","Арсена","Ассоль","Ася","Аэлита","Бажена","Беатрис","Бела","Белла","Берта","Богдана","Божена","Бриллиант",
                "Бронислава","Бьянка","Бэлла","Валентина","Валерия","Ванда","Варвара","Василина","Василиса","Васса","Венера","Вера",
                "Вероника","Вета","Викторина","Виктория","Вилена","Виола","Виолетта","Вит","Вита","Виталина","Виталия","Влада","Владана",
                "Владислава","Владлена","Волга","Габриэлла","Галина","Гвоздика","Георгина","Гертруда","Глафира","Грета","Даздраперма",
                "Даниэла","Дания","Дара","Дарина","Дария","Декабрина","Дарья","Дженна","Диамара","Диана","Дина","Долорес","Доминика",
                "Домна","Домника","Дора","Ева","Евангелина","Ева","Евгения","Евдокия","Екатерина","Елен","Алёна","Елена","Елизавета","Есения",
                "Ефросинья","Жаклин","Жанна","Жозефина","Жоржина","Залина","Зара","Зинаида","Зиновия","Зита","Злата","Златослава","Зореслава",
                "Зоя","Иванна","Ивета","Иветта","Ида","Изабелла","Иллирика","Илона","Инесса","Инна","Интерна","Иоанна","Иона","Ира","Ираида",
                "Ирина","Ирма","Искра,Исталина","Иулиания","Июлия","Ия","Калерия","Камилла","Капитолина","Кара","Карина","Кармия","Каролина",
                "Катерина","Кира","Клавдия","Клара","Клементина","Кора","Корнелия","Кристина","Ксения","Лада","Лана","Лара","Лариса","Лаура",
                "Ленина","Леокадия","Леона","Лера","Леся","Лета","Лиана","Лидия","Лика","Лили","Лилиана","Лилия","Лина","Лия","Лола","Лолита","Лора",
                "Луиза","Лукерья","Лукина","Лукия","Любава","Любовь","Людмила","Люсиль","Люсьена","Люция","Люче","Ляля","Магда","Магдалена","Мадлен",
                "Майя","Макария","Маргарита","Мариа","Марианна","Марика","Марина","Мария","Марта","Марфа","Марьяна","Мелания","Мила","Милада","Милана",
                "Милен","Милена","Милица","Милослава","Мира","Мирослава","Мирра","Мия","Моника","Муза","Надежда","Нана","Наталия","Наталья","Нева",
                "Нелли","Неонилла","Ника","Николь","Нина","Нинель","Нора","Одетта","Оксана","Октябрина","Олеся","Оливия","Олимпиада","Ольга","Павла",
                "Павлина","Патриция","Паула","Пейтон","Пелагея","Платонида","Победа","Полина","Прасковья","Рада","Раиса","Рамина","Ребекка","Револа",
                "Регина","Резеда","Рена","Рената","Рианна","Рикарда","Римма","Рина","Рита","Рогнеда","Роза","Роксолана","Ростислава","Русалина",
                "Руслана","Руфина","Сабина","Сабрина","Саломея","Сандра","Сара","Сауле","Светлана","Святослава","Севастьяна","Сентябрина","Серафима",
                "Сесилия","Сильвия","Снежана","Соломия","Соня","София","Софья","Союза","Спартакиада","Станислава","Стелла","Стефания","Сусанна",
                "Таисия","Тамара","Тамила","Татьяна","Таяна","Теона","Тереза","Тина","Ульяна","Урсула","Услада","Устинья","Фаина","Фёкла","Флора","Флоренс",
                "Флорентина","Флоренция","Флориана","Фотиния","Хилари","Хома","Цагана","Цветана","Цецилия","Челси","Шарлотта","Шейла","Эвелина",
                "Элеонора","Элиана","Элиза","Элизар","Элина","Элла","Эльвина","Эльвира","Эля","Эмили","Эмилия","Эмма","Эрмина","Эсмеральда","Этель",
                "Юлианна","Юлия","Юния","Ядвига","Яна","Янина","Ярина","Ярослава"
            };

        private readonly static string[] FirstManNames =
            {

            "Абрам","Авангард","Августин","Авдей","Аверкий","Аверьян","Авксентий","Авраам","Агап","Агапий","Агапит","Агафодор",
                "Агафон","Адам","Адриан","Акакий","Аким","Алан","Александр","Алексей","Алексий","Алик","Алмаз","Алтай","Альберт",
                "Амур","Анастасий","Анатолий","Андрей","Аникий","Анисим","Антон","Антоний","Антонин","Ануфрий","Анфим","Аполлон",
                "Аристарх","Аркадий","Арман","Арсен","Арсений","Арсентий","Артём","Артемий","Артур","Архип","Афанасий","Блез","Богдан",
                "Борис","Борислав","Бронислав","Вадим","Вакула","Валентин","Валерий","Вальдемар","Варлаам","Варлен","Василий","Василиск",
                "Васой","Вассиан","Велиор","Вемир","Вениамин","Виктор","Вильгельм","Виталий","Владимир","Владислав","Владлен","Влас",
                "Всеволод","Вячеслав","Гавриил","Гарри","Геласий","Геннадий","Генри","Генрих","Георгий","Герасим","Герман","Германн","Гималай",
                "Глеб","Гордей","Григорий","Густав","Давид","Давыд","Далис","Дамиан","Дамир","Даниил","Данислав","Даниэль","Дарий","Демид",
                "Демьян","Денис","Джеймс","Джозеф","Димитрий","Дино","Диомид","Дмитрий","Доминик","Донат","Досифей","Евгений","Евдоким","Евсей",
                "Евстахий","Егор","Егорий","Елисей","Емельян","Епифан","Еремей","Ерофей","Есфирь","Ефим","Ефрем","Жерар","Захар","Зенон","Зигмунд",
                "Зиновий","Зосим","Иван","Игнат","Игнатий","Игорь","Иеремия","Иероним","Измаил","Иисус","Иларион","Илья","Иммануил","Иннокентий",
                "Иоанн","Иосиф","Ипатий","Ипполит","Ираклий","Исаак","Исаакий","Исидор","Исмаил","Июлий","Казимир","Калистрат","Камиль","Карл",
                "Карлен","Карп","Ким","Кир","Кирилл","Клавдий","Клаус","Клим","Климент","Коминтерн","Кондрат","Конрад","Констанс","Константин",
                "Констанций","Корнелий","Кристиан","Кузьма","Лаврентий","Лазарь","Лев","Леград","Ленслав","Леон","Леонард","Леонид","Леонтий",
                "Леопольд","Лесь","Лоренс","Лука","Лукиан","Лукий","Лукиллиан","Лукьян","Люблен","Любомир","Людвиг","Людовик","Люций","Май","Майкл",
                "Макар","Макарий","Максим","Максимилиан","Мар","Марат","Марк","Маркел","Марсель","Мартин","Мартын","Матвей","Мелентий","Мефодий",
                "Мечислав","Мика","Микула","Милослав","Мирон","Мирослав","Митрофан","Михаил","Михей","Модест","Моисей","Мстислав","Мэтью",
                "Назар","Наум","Нектарий","Нестор","Никита","Никифор","Никодим","Никола","Николай","Нильс","Огюст","Олег","Оливер","Орест",
                "Орландо","Осип","Оскар","Остап","Остин","Павел","Павсикакий","Панас","Панкрат","Пантелеимон","Пантелеймон","Парамон","Патрик",
                "Пахом","Пахомий","Педро","Первомай","Перри","Пётр","Пимен","Питирим","Платон","Поликарп","Потап","Прокоп","Прохор","Радий",
                "Радик","Радомир","Радослав","Разумник","Раймонд","Рамон","Рафаэль","Рей","Ренат","Ригор","Ринат","Ричард","Роберт","Родион",
                "Ролан","Роман","Ростислав","Рубен","Рудольф","Руслан","Руф","Рэй","Савва","Савелий","Самсон","Самуил","Светослав","Свирид",
                "Святополк","Святослав","Севастьян","Северьян","Семён","Серапион","Серафим","Серафион","Сергей","Сергий","Сесил","Сидор","Совл",
                "Созонт","Сонар","Спартак","Спиридон","Стален","Станислав","Степан","Стефан","Султан","Тарас","Тимофей","Тимур","Тихон",
                "Томас","Трифон","Трофим","Трудомир","Тумас","Уалент","Уалентин","Устин","Фаддей","Февралин","Фёдор","Федот","Феликс","Феодор",
                "Феофан","Феофилакт","Филарет","Филимон","Филипп","Флор","Фома","Фридрих","Харитон","Христиан","Христофор","Цезарь","Цецилий",
                "Цицерон","Чарльз","Чеслав","Шарль","Эдгар","Эдуард","Эльдар","Эмиль","Эрик","Эркюль","Эрмин","Эрнест","Эузебио","Юлиан","Юлий",
                "Юргаг","Юрий","Юстиниан","Юстус","Яков","Ян","Яромир","Ярослав"

            };

        private readonly static string[] MiddleWomanNames = {
            "Августовна", "Акимовна", "Александровна", "Алексеевна", "Анатольевна", "Андреевна", "Андрониковна",
            "Антоновна", "Аркадьевна", "Афанасьевна", "Батьковна", "Богдановна", "Борисовна", "Валентиновна",
            "Валерьевна", "Васильевна", "Вахтанговна", "Вениаминовна", "Викторовна", "Виссарионовна", "Витальевна",
            "Владимировна", "Вячеславовна", "Гавриловна", "Гаджиевна", "Геннадьевна", "Генриховна", "Георгиевна",
            "Глебовна", "Григорьевна", "Денисовна", "Дмитриевна", "Евгеньевна", "Евдокимовна", "Ивановна", "Игнатьевна",
            "Игоревна", "Ильгизовна", "Ильмировна", "Ильнуровна", "Ильсуровна", "Иоанновна", "Иосифовна", "Исаевна",
            "Каллиниковна", "Каллистратовна", "Константиновна", "Ксенофонтьевна", "Леонидовна", "Львовна", "Магомедовна",
            "Магометовна", "Макаровна", "Максимилиановна", "Максимовна", "Марковна", "Михайловна", "Натановна", "Никандровна",
            "Никаноровна", "Никитична", "Никитовна", "Никифоровна", "Никодимовна", "Николаевна", "Никоновна", "Олеговна",
            "Осиповна", "Павловна", "Петровна", "Платоновна", "Прохоровна", "Романовна", "Рудольфовна", "Рустамовна",
            "Семёновна", "Сергеевна", "Сидоровна", "Сильвестровна", "Соломоновна", "Станиславовна", "Степановна", "Тимофеевна",
            "Фёдоровна", "Филипповна", "Юрьевна", "Яковлевна", "Ярославовна"
        };

        private readonly static string[] MiddleManNames = {
            "Ааронович", "Абрамович", "Августович", "Авдеевич", "Аверьянович", "Адамович", "Адрианович",
            "Акимович", "Аксёнович", "Александрович", "Алексеевич", "Анатольевич", "Андреевич", "Андроникович",
            "Анисимович", "Антипович", "Антонович", "Ануфриевич", "Аркадьевич", "Арсенович", "Арсеньевич",
            "Артёмович", "Артемьевич", "Артурович", "Архипович", "Афанасьевич", "Батькович", "Бедросович",
            "Бенедиктович", "Богданович", "Бориславич", "Бориславович", "Борисович", "Борисыч", "Брониславович",
            "Ваганович", "Вадимович", "Валентинович", "Валерианович", "Валерьевич", "Валерьянович", "Васильевич",
            "Вахтангович", "Венедиктович", "Вениаминович", "Викентьевич", "Викторович", "Виленович", "Вилорович",
            "Виссарионович", "Витальевич", "Владиленович", "Владимирович", "Владиславович", "Владленович",
            "Власович", "Вольфович", "Всеволодович", "Вячеславович", "Гавриилович", "Гаврилович", "Гаджиевич",
            "Геннадиевич", "Геннадьевич", "Генрихович", "Георгиевич", "Герасимович", "Германович", "Гертрудович",
            "Глебович", "Гордеевич", "Григорьевич", "Гурьевич", "Давидович", "Давыдович", "Даниилович",
            "Данилович", "Демидович", "Демьянович", "Денисович", "Димитриевич", "Дмитриевич", "Дорофеевич",
            "Евгеньевич", "Евграфович", "Евдокимович", "Евсеевич", "Евстигнеевич", "Егорович", "Елизарович",
            "Елисеевич", "Емельянович", "Еремеевич", "Ермилович", "Ермолаевич", "Ерофеевич", "Ефимович",
            "Ефимьевич", "Ефремович", "Ефстафьевич", "Жанович", "Жоресович", "Захарович", "Захарьевич",
            "Зиновьевич", "Ибрагимович", "Иванович", "Иваныч", "Игнатович", "Игнатьевич", "Игоревич",
            "Измаилович", "Изотович", "Израилевич", "Иларионович", "Ильгизович", "Ильич", "Ильмирович",
            "Ильнурович", "Ильсурович", "Ильясович", "Иоаннович", "Иосипович", "Иосифович", "Исаевич",
            "Исидорович", "Каллиникович", "Каллистратович", "Константинович", "Леонидович", "Леонович",
            "Леонтьевич", "Львович", "Магомедович", "Магометович", "Макарович", "Максимилианович",
            "Максимович", "Маркович", "Матвеевич", "Михайлович", "Михалыч", "Натанович", "Наумович",
            "Никандрович", "Никанорович", "Никитич", "Никитович", "Никифорович", "Никодимович", "Николаевич",
            "Никонович", "Олегович", "Осипович", "Павлович", "Петрович", "Платонович", "Прохорович",
            "Романович", "Ростиславович", "Рудольфович", "Русланович", "Рустамович", "Семёнович", "Сергеевич",
            "Сидорович", "Сильвестрович", "Соломонович", "Степанович", "Тарасович", "Теймуразович",
            "Терентьевич", "Тимофеевич", "Тимурович", "Тихонович", "Трифонович", "Трофимович", "Устинович",
            "Фадеевич", "Фёдорович", "Федосеевич", "Федосьевич", "Федотович", "Феликсович", "Феодосьевич",
            "Феоктистович", "Феофанович", "Филатович", "Филимонович", "Филиппович", "Фокич", "Фомич", "Фролович",
            "Харитонович", "Харламович", "Харлампович", "Харлампьевич", "Чеславович", "Эдгардович", "Эдгарович",
            "Эдуардович", "Эдуардович", "Юлианович", "Юльевич", "Юрьевич", "Яковлевич", "Якубович", "Ярославович"
        };

        private readonly static string[] LastNames =
            {
            "Смирнов","Иванов","Кузнецов","Соколов","Попов","Лебедев","Козлов","Новиков","Морозов","Петров","Волков","Соловьёв",
                "Васильев","Зайцев","Павлов","Семёнов","Голубев","Виноградов","Богданов","Воробьёв","Фёдоров","Михайлов","Беляев",
                "Тарасов","Белов","Комаров","Орлов","Киселёв","Макаров","Андреев","Ковалёв","Ильин","Гусев","Титов","Кузьмин","Кудрявцев",
                "Баранов","Куликов","Алексеев","Степанов","Яковлев","Сорокин","Сергеев","Романов","Захаров","Борисов","Королёв","Герасимов",
                "Пономарёв","Григорьев","Лазарев","Медведев","Ершов","Никитин","Соболев","Рябов","Поляков","Цветков","Данилов","Жуков",
                "Фролов","Журавлёв","Николаев","Крылов","Максимов","Сидоров","Осипов","Белоусов","Федотов","Дорофеев","Егоров","Матвеев",
                "Бобров","Дмитриев","Калинин","Анисимов","Петухов","Антонов","Тимофеев","Никифоров","Веселов","Филиппов","Марков",
                "Большаков","Суханов","Миронов","Ширяев","Александров","Коновалов","Шестаков","Казаков","Ефимов","Денисов","Громов","Фомин",
                "Давыдов","Мельников","Щербаков","Блинов","Колесников","Карпов","Афанасьев","Власов","Маслов","Исаков","Тихонов","Аксёнов",
                "Гаврилов","Родионов","Котов","Горбунов","Кудряшов","Быков","Зуев","Третьяков","Савельев","Панов","Рыбаков","Суворов","Абрамов",
                "Воронов","Мухин","Архипов","Трофимов","Мартынов","Емельянов","Горшков","Чернов","Овчинников","Селезнёв","Панфилов","Копылов",
                "Михеев","Галкин","Назаров","Лобанов","Лукин","Беляков","Потапов","Некрасов","Хохлов","Жданов","Наумов","Шилов","Воронцов","Ермаков",
                "Дроздов","Игнатьев","Савин","Логинов","Сафонов","Капустин","Кириллов","Моисеев","Елисеев","Кошелев","Костин","Горбачёв","Орехов",
                "Ефремов","Исаев","Евдокимов","Калашников","Кабанов","Носков","Юдин","Кулагин","Лапин","Прохоров","Нестеров","Харитонов","Агафонов",
                "Муравьёв","Ларионов","Федосеев","Зимин","Пахомов","Шубин","Игнатов","Филатов","Крюков","Рогов","Кулаков","Терентьев","Молчанов",
                "Владимиров","Артемьев","Гурьев","Зиновьев","Гришин","Кононов","Дементьев","Ситников","Симонов","Мишин","Фадеев","Комиссаров",
                "Мамонтов","Носов","Гуляев","Шаров","Устинов","Вишняков","Евсеев","Лаврентьев","Брагин","Константинов","Корнилов","Авдеев","Зыков",
                "Бирюков","Шарапов","Никонов","Щукин","Дьячков","Одинцов","Сазонов","Якушев","Красильников","Гордеев","Самойлов","Князев","Беспалов",
                "Уваров","Шашков","Бобылёв","Доронин","Белозёров","Рожков","Самсонов","Мясников","Лихачёв","Буров","Сысоев","Фомичёв","Русаков",
                "Стрелков","Гущин","Тетерин","Колобов","Субботин","Фокин","Блохин","Селиверстов","Пестов","Кондратьев","Силин","Меркушев","Лыткин","Туров"
        };

        private readonly static string[][] FirstNames = { FirstWomanNames, FirstManNames };
        private readonly static string[][] MiddleNames = { MiddleWomanNames, MiddleManNames };
        private readonly static string[] LastNamesEnding = { "а", "" };

        /// <summary>
        /// Генерирует полное имя (Имя Фамилия)
        /// </summary>
        /// <returns>Имя Фамилия</returns>
        public static string RandomFullName(bool andMiddleName = false)
        {
            var sex = (byte)random.Next(0, 2);

            var firstNames = FirstNames[sex];
            var firstName = firstNames[random.Next(0, firstNames.Length)];

            var lastName = LastNames[random.Next(0, LastNames.Length)] + LastNamesEnding[sex];

            var fullName = $"{firstName} {lastName}";

            if (andMiddleName)
            {
                var middleNames = MiddleNames[sex];
                var middleName = middleNames[random.Next(0, middleNames.Length)];
                fullName += " " + middleName;
            }

            return fullName;

        }

        /// <summary>
        /// Случайный адрес электронной почты
        /// </summary>
        /// <param name="MinLength">Минимальная длина</param>
        /// <param name="MaxLength">Максимальная длина</param>
        /// <param name="hosting">Хостинг</param>
        /// <returns>Адрес электронной почты</returns>
        public static string RandomMail(int MinLength = 5, int MaxLength = 10, string hosting = "@gmail.com")
        {
            var length = random.Next(MinLength, MaxLength);

            var result = new StringBuilder();
            while (result.Length < length)
            {
                var symbol = symbolsMail[random.Next(0, symbolsMail.Length)];
                result.Append(symbol);
            }

            result.Append(hosting);

            return result.ToString();
        }


        /// <summary>
        /// Случайный набор символов
        /// </summary>
        /// <param name="MinLength">Минимальная длина</param>
        /// <param name="MaxLength">Максимальная длина</param>
        /// <returns>Набор символов</returns>
        public static string RandomWorld(int MinLength = 5, int MaxLength = 10)
        {
            return RandomMail(MinLength, MaxLength, "");
        }


        /// <summary>
        /// Генерирует случайный номер телефора
        /// </summary>
        /// <returns>Номер телефона</returns>
        public static string RandomPhone()
        {
            return "+"
                + random.Next(1, 9)
                + String.Format("{0: (###) }", random.Next(111, 999))
                + String.Format("{0:###-####}", random.Next(1111111, 9999999));
        }

        /// <summary>
        /// Генерирует случайную дату
        /// </summary>
        /// <param name="minDate">Минимальная граница</param>
        /// <param name="maxDate">Максимальная граница</param>
        /// <returns>Дата</returns>
        public static DateTime RandomDate(DateTime? minDate = null, DateTime? maxDate = null)
        {
            minDate ??= DateTime.MinValue;
            maxDate ??= DateTime.MaxValue;

            var minDays = -(DateTime.Today - minDate).Value.Days;
            var maxDays = +(maxDate - DateTime.Today).Value.Days;
            var ranDays = random.Next(minDays, maxDays);
            var ranDate = DateTime.Today.AddDays(ranDays).Date;

            return ranDate;
        }

        /// <summary>
        /// Случайное булевное значение
        /// </summary>
        /// <returns>Булевное значение</returns>
        public static bool RandomBool()
        {
            return random.Next(0, 2) == 1;
        }

    }
}
