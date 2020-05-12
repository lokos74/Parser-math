using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        
        static int kolsk = 0; 
        static int[] kolmasbuf1= new int [10];
        static string[,] masbuf = new string[10,2];
        static string[,,] masbuf1 = new string[10,10,2];
        static int kolx = 0; 
        static string[,] masz = new string[50,2];


        static int pars(string dat, int num)
        {
            int x=0;
            string [] nach=new string[2];           
            int kol1 = dat.Length;// длина выражения
                     for (int i1 = 0; i1 < kol1; i1++) // начало функции ввода выражения в массив
                            {
                                if (!(dat[i1]==','))//если цифра то прибавляем к другим цифрам
                                {                                  
                                     nach[x] = nach[x] + dat[i1];                                   
                                }
                                else // если нет то записываем в следующий элемент массива
                                {
                                    x++;
                                };                     
                            }
            int ret;
            if (num == 0)
                ret = Convert.ToInt32(nach[0]);
            else
                ret = Convert.ToInt32(nach[1]);
            return ret;
        }


        static void strdel(ref string name1,string dat)
        {
           string name=name1;

           name = name.Remove(Convert.ToInt32(pars(dat, 0)), (Convert.ToInt32(pars(dat, 1)) + 1) - Convert.ToInt32(pars(dat, 0)));
           name1 = name;
        }

        static void strins(ref string name1, int nach, string dat)
        {
            name1 = name1.Insert(nach,dat);
        }

        static Boolean skob(ref string[,] mas)
        {
            bool sc=false;
            int kol = mas.Length/2;
            for (int i = 0; i < kol; i++)
            {
                if (!(mas[i,0] == null))
                if ((mas[i,0][0] == '(') || (mas[i,0][0] == ')'))
                {
                    sc = true;
                }
            }
                return sc;
        }

        static void masdel(ref string[,,] mas,int virag, int start, int dlin)// метод для удаления значений массива
        {
            string[,,] arr = new string[10,10,2];
            arr = mas;
            for (int i = start; i < 10 - dlin; i++)
            {
                arr[virag, i, 0] = mas[virag,i+dlin,0];
                arr[virag, i, 1] = mas[virag,i+dlin,1];
            }
            for (int i = 10 - dlin; i < 10; i++)
            {
                arr[virag, i, 0] = null;
                arr[virag, i, 1] = null;
            }
            mas = arr;
        }

        static string[,] poisksk(string virag)
        { 
            int kol = virag.Length;
            string [,] mas = new string [10,2];
            int x = 0; //функция поиска скобок
            for (int i = 0; i < kol; i++)
            {
                if ((virag[i] == '(') || (virag[i] == ')'))
                {                   
                        mas[x,0] = mas[x,0] + virag[i];
                        mas[x, 1] = mas[x, 1] + i;
                    x++;
                    kolsk = x-1 ; 
                    }
                    
                }

            return mas;
        }

        static void virag_v_masbuf(string virag,int num)
        {
            int x1 = 0;
            int kol1 = virag.Length;// длина выражения
                            for (int i1 = 0; i1 < kol1; i1++) // начало функции ввода выражения в массив
                            {   if (i1>0)
                                if ((virag[i1] == '-') & (!(char.IsDigit(virag[i1 - 1]))))
                                {
                                    masbuf1[num,x1,0] = masbuf1[num,x1,0] + virag[i1];
                                    i1++;
                                }
                                    

                                if (char.IsDigit(virag[i1]) || (virag[i1]==','))//если цифра то прибавляем к другим цифрам
                                {
                                     
                                     masbuf1[num,x1,0] = masbuf1[num,x1,0] + virag[i1];                                   
                                }
                                else // если нет то записываем в следующий элемент массива
                                {
                                    if ((virag[i1 + 1] == '-') & ((virag[i1]=='-')))
                                    {
                                        x1++;
                                       masbuf1[num, x1, 0] = "+";
                                       string dat = Convert.ToString(i1+1)+','+Convert.ToString(i1+1);
                                       strdel(ref virag,dat);
                                       kol1--;
                                    }
                                    else
                                    {
                                        x1++;// счетчик массива masbuf1
                                        masbuf1[num, x1, 0] = ""; 
                                        masbuf1[num,x1,0] = masbuf1[num,x1,0] + virag[i1];
                                    }   
                                     x1++;     
                                        
                                   
                                   
                                  };

                            } kolx++;
                            kolmasbuf1[num] = x1;

        }


        static void virvsk(ref string [,] masz, string name)
        {
           int x = 0;
           int x1 = 0;

            for (int i = 0; i < kolsk; i++) // цикл перебора скобок 
            {
                if (!(masz[i, 0] == null))
                    if ((masz[i, 0][0] == '(') & (masz[i+1, 0][0] == ')'))// если скобки () идут подряд то...
                        {   // вырезаем выражение для парсинга и замены
                            masbuf[x,0] = name.Substring(Convert.ToInt32(masz[i, 1]) + 1, Convert.ToInt32(masz[i+1, 1]) - Convert.ToInt32(masz[i, 1])-1);
                            masbuf[x, 1] = masz[i, 1]+','+masz[i+1,1];
                            virag_v_masbuf(masbuf[x, 0], x);                          
                            x1=0;
                            x++;// счетчик массива masbuf
                            kolx = x;
                        }
                 }
            // конец функцци
        }


        static void virvchis()
        { 
            //kolx кол во найденых выражений
            int x = 0;
            for (int i1 = 0; i1 < kolx; i1++)
            {
                while (!(masbuf1[i1, 2, 0] == null))
                {
                    for (int i = 0; i < kolmasbuf1[i1]; i++)
                    {
                        if (!(masbuf1[i1, i, 0] == null))
                            if (!(masbuf1[i1, i, 0].Length>1))
                            if (!char.IsDigit(masbuf1[i1, i, 0][0]))// если не число то определяем операцию
                            {
                                if (masbuf1[i1, i, 0][0] == '*')
                                {
                                    float buf1 = Convert.ToSingle(masbuf1[i1, i - 1, 0]) * Convert.ToSingle(masbuf1[i1, i + 1, 0]);                                    
                                    masbuf1[i1, i, 1] = masbuf1[i1, i, 1] + buf1;
                                    masbuf1[i1, i - 1, 0] = "";
                                    masbuf1[i1, i - 1, 0] = masbuf1[i1, i - 1, 0] + buf1;
                                    masdel(ref masbuf1, i1, i, 2);
                                    masbuf1[i1, 0, 1] = "";
                                    masbuf1[i1, 0, 1] = masbuf1[i1, 0, 1] + buf1;
                                    i = 0;

                                }
                                else if (masbuf1[i1, i, 0][0] == '/')
                                {
                                    float buf1 = Convert.ToSingle(masbuf1[i1, i - 1, 0]) / Convert.ToSingle(masbuf1[i1, i + 1, 0]);                                    
                                    masbuf1[i1, i, 1] = masbuf1[i1, i, 1] + buf1;
                                    masbuf1[i1, i - 1, 0] = "";
                                    masbuf1[i1, i - 1, 0] = masbuf1[i1, i - 1, 0] + buf1;
                                    masdel(ref masbuf1, i1, i, 2);
                                    masbuf1[i1, 0, 1] = "";
                                    masbuf1[i1, 0, 1] = masbuf1[i1, 0, 1] + buf1;
                                    i = 0;
                                }
                            }
                    }
                    for (int i = 0; i < kolmasbuf1[i1]; i++)
                    {
                        if (!(masbuf1[i1, i, 0] == null))
                            if (!(masbuf1[i1, i, 0].Length>1))
                            if (!char.IsDigit(masbuf1[i1, i, 0][0]))
                            {
                                if (masbuf1[i1, i, 0][0] == '+')
                                {
                                    float buf1 = Convert.ToSingle(masbuf1[i1, i - 1, 0]) + Convert.ToSingle(masbuf1[i1, i + 1, 0]);                                    
                                    masbuf1[i1, i, 1] = masbuf1[i1, i, 1] + buf1;
                                    masbuf1[i1, i - 1, 0] = "";
                                    masbuf1[i1, i - 1, 0] = masbuf1[i1, i - 1, 0] + buf1;
                                    masdel(ref masbuf1, i1, i, 2);
                                    masbuf1[i1, 0, 1] = "";
                                    masbuf1[i1, 0, 1] = masbuf1[i1, 0, 1] + buf1;
                                    i = 0;
                                }
                                else if (masbuf1[i1, i, 0][0] == '-')
                                {
                                    float buf1 = Convert.ToSingle(masbuf1[i1, i - 1, 0]) - Convert.ToSingle(masbuf1[i1, i + 1, 0]);                                    
                                    masbuf1[i1, i, 1] = masbuf1[i1, i, 1] + buf1;
                                    masbuf1[i1, i - 1, 0] = "";
                                    masbuf1[i1, i - 1, 0] = masbuf1[i1, i - 1, 0] + buf1;
                                    masdel(ref masbuf1, i1, i, 2);
                                    masbuf1[i1, 0, 1] = "";
                                    masbuf1[i1, 0, 1] = masbuf1[i1, 0, 1] + buf1;
                                    i = 0;
                                }

                            }

                    }
                }
            }
        }

        static bool proverka(string name)
            {
                int dl = name.Length;
                bool pro = true;
                for (int i = 0; i < dl; i++)
                {
                    if ((name[i] == '+') || (name[i] == ' ') || (name[i] == '-') || (name[i] == '*') || (name[i] == '/') || (name[i] == ',') || (name[i] == '(') || (name[i] == ')') || (char.IsDigit(name[i])))
                    {
                        pro = pro & true;
                    }
                    else pro = pro & false;
                }
                return pro;

            }

        static void raschet(string name)
          { 
            if (proverka(name))
           {
               masz = poisksk(name);
               // поиск внутренних скобок.
               while (skob(ref masz))
               {
                   masz = poisksk(name);
                   virvsk(ref masz, name);
                   virvchis();
                   strdel(ref name, masbuf[0, 1]);
                   strins(ref name, pars(masbuf[0, 1], 0), masbuf1[0, 0, 0]);
                   masz = poisksk(name);
                   Array.Clear(masbuf, 0, 20);
                   Array.Clear(masbuf1, 0, 200);
                   Console.WriteLine(name);
               }
               virag_v_masbuf(name, 0);
               virvchis();
               Console.WriteLine("Результат="+ masbuf1[0, 0, 0]);
                Array.Clear(masbuf1, 0, 200);
           }
           else 
           { 
               Console.WriteLine("Выражение содержит не поддерживаемые символы!");
               
           } 

          }

        static void Main(string[] args)
        {          
            string name = "231-53,2+(453*465-(325+32,5*23*45/5)-(324+2-343))-(32*56)+342";
            Console.WriteLine("Пример выражения");
            raschet(name); 
            Console.WriteLine("Введите свое выражение без пробелов");
 link1:     name = Console.ReadLine();
            raschet(name);
            if (!proverka(name))goto link1;         
                
            System.Console.ReadLine();
        }
    }
}
