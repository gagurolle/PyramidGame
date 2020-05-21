using System;
namespace ConsoleGame_Pyramid
{
    public class Pyramid_block
    {
        static string[] _pyramid_block = new string[5];
        public Pyramid_block()
        {
            _pyramid_block[0] = "    ||    ";
            _pyramid_block[1] = "   |==|   ";
            _pyramid_block[2] = "   |===|  ";
            _pyramid_block[3] = "   |====| ";
            _pyramid_block[4] = "   |=====|";
        }
        public string block(int number)
        {
            return _pyramid_block[number];
        }

    }
    class Pyramid_Base
    {
        int checked_block_x = 0;
        int checked_block_y = 0;
        int chose_block_x = 0;
        int chose_block_y = 0;
        int cycle = 0;
        bool chose_flag = false;

        static Pyramid_block pyramidblock = new Pyramid_block();

        int[,] Field = { { 1, 0, 0 }, { 2, 0, 0 }, { 3, 0, 0 }, { 4, 0, 0 } };

        private void iterator() { cycle++; }

        private void end_game()
        {

            if (Field[0, 2] == 1)
            {
                Console.Clear();
                Console.SetWindowSize(50, 20);
                Console.Title = "Игра:  Пирамида";
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Молодец! Игра окончена");
                Console.WriteLine("Шагов было сделано - {0}", cycle);
                Console.WriteLine("Чтобы выйти из игры, нажмите любую кнопку...");
                Console.Read();
                Environment.Exit(0);
            }
        }

        public void set_column(string letter)
        {
            switch (letter)
            {
                case "d":
                    if (chose_flag == true)
                    {
                        if (chose_block_x + 1 < 3)
                        {

                            if (Field[chose_block_y, chose_block_x] > check_active_block2(chose_block_x + 1) && check_active_block2(chose_block_x + 1) != 0)
                            {
                                try
                                {
                                    if (Field[chose_block_y, chose_block_x] > check_active_block2(chose_block_x + 2) && check_active_block2(chose_block_x + 2) != 0)
                                    {

                                    }
                                    else
                                    {
                                        if (checked_block_x + 2 < 3)
                                        {
                                            move_block(chose_block_x + 2);
                                        }
                                    }
                                }
                                catch { }
                            }
                            else
                            {
                                if (chose_block_x + 1 < 3)
                                {
                                    move_block(chose_block_x + 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (checked_block_x + 1 < 3)
                        {
                            checked_block_x++;
                        }
                    }
                    break;
                case "a":
                    if (chose_flag == true)
                    {
                        if (chose_block_x - 1 >= 0)
                        {
                            if (Field[chose_block_y, chose_block_x] > check_active_block2(chose_block_x - 1) && check_active_block2(chose_block_x - 1) != 0)
                            {
                                try
                                {
                                    if (Field[chose_block_y, chose_block_x] > check_active_block2(chose_block_x - 2) && check_active_block2(chose_block_x - 2) != 0)
                                    {

                                    }
                                    else
                                    {
                                        if (checked_block_x - 2 >= 0)
                                        {
                                            move_block(chose_block_x - 2);
                                        }
                                    }
                                }
                                catch { }
                            }
                            else
                            {
                                if (chose_block_x - 1 >= 0)
                                {
                                    move_block(chose_block_x - 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (checked_block_x - 1 >= 0)
                        {
                            checked_block_x--;
                        }
                    }
                    break;
                case "f":
                    if (chose_flag == false)
                    {
                        chose_block_x = checked_block_x;
                        chose_block_y = checked_block_y;
                        chose_flag = true;
                    }
                    else
                    {
                        chose_flag = false;
                    }
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
            }
            if (checked_block_x >= 0 && checked_block_x < 3)
                check_active_block(checked_block_x);
        }
        public int check_active_block(int x)
        {
            int y = 0;
            while (y < 3 && Field[y, x] == 0)
            {
                y++;
            }

            checked_block_x = x;
            checked_block_y = y;

            return Field[y, x];
        }

        public int check_active_block2(int x)
        {
            int y = 0;
            while (y < 3 && Field[y, x] == 0)
            {
                y++;
            }

            return Field[y, x];
        }

        public void move_block(int x)
        {
            iterator();
            if (x > 2 || x < 0 || Field[chose_block_y, chose_block_x] < Field[0, x])
            {
                Console.WriteLine("сюда нельзя!");
            }
            else
            {
                Field[0, x] = Field[chose_block_y, chose_block_x];
                int i = 0 + 1;
                while (i <= 3 && Field[i, x] == 0)//Сначала сравнение с i, чтобы выйти из цикла до проверки элемента массива
                {
                    Field[i, x] = Field[i - 1, x];
                    Field[i - 1, x] = 0;

                    ++i;
                }
                Field[chose_block_y, chose_block_x] = 0;

                checked_block_x = x;
                checked_block_y = i;
                if (i < 3 && Field[i, x] > Field[i + 1, x])
                {
                    move_block(x + 1);
                }
                chose_flag = false;
            }
        }

        public Pyramid_Base()
        {
            Console.SetWindowSize(50, 20);
            Console.Title = "Игра:  Пирамида";
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Правила просты - нужно переместить блоки \n на последнюю ось в таком же порядке, \nв каком они расположены изначально.");
            Console.WriteLine("Больший блок нельзя поместить на меньший блок.");
            Console.WriteLine("Желтым подсвечивается в режиме выбора блока");
            Console.WriteLine("Красным подсвечивается выбранный блок");
            Console.WriteLine("Управление: (eng) - вводишь символ,\n а затем жмешь Enter");
            Console.WriteLine("a - движение/выбор влево");
            Console.WriteLine("d - движение/выбор вправо");
            Console.WriteLine("f - выбор/сброс выбора блока");
            Console.WriteLine("q - выход из игры\n\n");
            Console.WriteLine("Для начала игры, нажмите любую клавишу");
            Console.Read();
            Paint_Base();
        }

        public void Paint_Base()
        {
            Console.Clear();
            Console.SetWindowSize(50, 20);
            Console.Title = "Игра:  Пирамида";
            Console.WriteLine();
            Console.WriteLine();
            for (int y = 0; y < 4; y++)
            {
                int x = 0;
                if (Field[y, x] == Field[chose_block_y, chose_block_x] && chose_flag == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (Field[y, x] == Field[checked_block_y, checked_block_x] && chose_flag == false)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }


                Console.Write(pyramidblock.block(Field[y, x]));
                Console.ResetColor();

                if (Field[y, x + 1] == Field[chose_block_y, chose_block_x] && chose_flag == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (Field[y, x + 1] == Field[checked_block_y, checked_block_x] && chose_flag == false)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write(pyramidblock.block(Field[y, x + 1]));
                Console.ResetColor();

                if (Field[y, x + 2] == Field[chose_block_y, chose_block_x] && chose_flag == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (Field[y, x + 2] == Field[checked_block_y, checked_block_x] && chose_flag == false)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write(pyramidblock.block(Field[y, x + 2]));
                Console.ResetColor();
                Console.WriteLine();

            }

            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();
            Console.WriteLine("x = " + checked_block_x + " " + "y= " + checked_block_y);
            Console.WriteLine("chose x = " + chose_block_x + " " + "chose y= " + chose_block_y);
            end_game();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string s = null;
            Pyramid_Base pyramid_base = new Pyramid_Base();
            while (true)
            {
                pyramid_base.Paint_Base();
                s = Console.ReadLine();
                pyramid_base.set_column(s);
            }
        }
    }
    }