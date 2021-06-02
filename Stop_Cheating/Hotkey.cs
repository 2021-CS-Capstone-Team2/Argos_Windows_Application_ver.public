using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stop_Cheating
{
    class Hotkey
    {
        static char[] v = new char[3];
        static int index = 0;

        int size(char[] arr, int count)
        {
            int num = 0;

            for (int i = 0; i < count; i++)
            {
                if (arr[i] != 0)
                    num++;
            }

            return num;
        }

        void clear(char[] arr, int count)
        {
            for (int i = 0; i < count; i++)
            {
                arr[i] = (char)0;
            }
        }

        public int Detect_Hotkey(char input)
        {

            // 버퍼가 비었을 경우
            if (size(v, 3) == 0)
            {
                if (input == 'O' || input == 'I' || input == '>' || input == 'P')
                {

                    v[index] = input; // 특수키가 눌리면 버퍼에 push
                    index++;
                }
            }

            //버퍼가 1개 차있을 경우
            else if (size(v, 3) == 1)
            {

                if (v[0] == 'O')
                { // 버퍼에 있는게 ctrl일 경우
                    if (input == '{')
                    { // ctrl + tab
                        clear(v, 3);
                        index = 0;
                        return 4;
                    }
                    else if (input == 'H')
                    { // ctrl + page down
                        clear(v, 3);
                        index = 0;
                        return 5;
                    }
                    else if (input == 'G')
                    { // ctrl + page up
                        clear(v, 3);
                        index = 0;
                        return 6;
                    }
                    else if (input == '1' || input == '2' || input == '3' || input == '4' || input == '5' ||
                        input == '6' || input == '7' || input == '8' || input == '9')
                    { // ctrl + number
                        clear(v, 3);
                        index = 0;
                        return 17;
                    }
                    else if (input == 'x')
                    { // ctrl + x
                        clear(v, 3);
                        index = 0;
                        return 18;
                    }
                    else if (input == 'c')
                    { // ctrl + c
                        clear(v, 3);
                        index = 0;
                        return 19;
                    }
                    else if (input == 'v')
                    { // ctrl + v
                        clear(v, 3);
                        index = 0;
                        return 20;
                    }
                    else if (input == 'a')
                    { // ctrl + a
                        clear(v, 3);
                        index = 0;
                        return 21;
                    }
                    else if (input == 'z')
                    { // ctrl + z
                        clear(v, 3);
                        index = 0;
                        return 22;
                    }
                    else if (input == 'D')
                    { // ctrl + ESC
                        clear(v, 3);
                        index = 0;
                        return 23;
                    }

                    else if (input == 'P')
                    { // ctrl + alt 일 경우 버퍼에 쌓음
                        v[index] = 'P';
                        index++;
                        return 999;
                    }
                    else if (input == '>')
                    { // ctrl + window 일 경우 버퍼에 쌓음
                        v[index] = '>';
                        index++;
                        return 999;
                    }
                    else if (input == 'I')
                    { // ctrl + shift 일 경우 버퍼에 쌓음
                      //cout << "stack!" << endl;
                        v[index] = 'I';
                        index++;
                        return 999;
                    }

                }

                if (v[0] == 'P')
                { // 버퍼에 있는게 alt일 경우
                    if (input == '{')
                    { // alt + tab
                        clear(v, 3);
                        index = 0;
                        return 7;
                    }
                    else if (input == 'D')
                    { // alt + ESC
                        clear(v, 3);
                        index = 0;
                        return 9;
                    }

                    else if (input == 'O')
                    { // alt + ctrl 일 경우 버퍼에 쌓음
                      //cout << "stack!" << endl;
                        v[index] = 'O';
                        index++;
                        return 999;
                    }
                }

                if (v[0] == '>')
                { // 버퍼에 있는게 window일 경우
                    if (input == '{')
                    { // window + tab
                        clear(v, 3);
                        index = 0;
                        return 10;
                    }
                    else if (input == 'L' || input == ':' || input == 'Z' || input == 'X')
                    { // window + arrow

                        clear(v, 3);
                        index = 0;
                        return 14;
                    }
                    else if (input == 'g')
                    { // window + g
                        clear(v, 3);
                        index = 0;
                        return 28;
                    }
                    else if (input == 'e')
                    { // window + e
                        clear(v, 3);
                        index = 0;
                        return 29;
                    }
                    else if (input == 'v')
                    { // window + v
                        clear(v, 3);
                        index = 0;
                        return 30;
                    }

                    else if (input == 'O')
                    { // window + ctrl 일 경우 버퍼에 쌓음
                      //cout << "stack!" << endl;
                        v[index] = 'O';
                        index++;
                        return 999;
                    }
                    else if (input == 'I')
                    { // window + shift 일 경우 버퍼에 쌓음
                      //cout << "stack!" << endl;
                        v[index] = 'I';
                        index++;
                        return 999;
                    }
                }

                if (v[0] == 'I')
                { // 버퍼에 있는게 shift 일 경우
                    if (input == 'O')
                    { // shift + ctrl 일 경우 버퍼에 쌓음
                      //cout << "stack!" << endl;
                        v[index] = 'O';
                        index++;
                        return 999;
                    }
                    else if (input == '>')
                    { // shift + window 일 경우 버퍼에 쌓음
                      //cout << "stack!" << endl;
                        v[index] = '>';
                        index++;
                        return 999;
                    }
                }

                //cout << "clear" << endl;
                clear(v, 3);
                index = 0; //그냥 단일키로 눌렸을 경우 버퍼 초기화
            }

            //버퍼가 2개 차있을 경우
            else if (size(v, 3) == 2)
            {

                if (v[0] == 'O' && v[1] == 'P')
                { // 버퍼에 있는게 ctrl + alt 일 경우
                    if (input == '{')
                    { // ctrl + alt + tab
                        clear(v, 3);
                        index = 0;
                        return 0;
                    }
                }

                else if (v[0] == 'O' && v[1] == '>')
                { // 버퍼에 있는게 ctrl + window 일 경우
                    if (input == 'd')
                    { // ctrl + window + d
                        clear(v, 3);
                        index = 0;
                        return 1;
                    }
                    else if (input == 'L')
                    { // ctrl + window + left
                        clear(v, 3);
                        index = 0;
                        return 2;
                    }
                    else if (input == 'Z')
                    { // ctrl + window + right
                        clear(v, 3);
                        index = 0;
                        return 3;
                    }
                }

                else if (v[0] == 'O' && v[1] == 'I')
                { // 버퍼에 있는게 ctrl + shift 일 경우
                    if (input == '{')
                    { // ctrl + shift + tab
                        clear(v, 3);
                        index = 0;
                        return 16;
                    }
                    else if (input == 'D')
                    { // ctrl + shift + ESC
                        clear(v, 3);
                        index = 0;
                        return 24;
                    }
                }

                if (v[0] == 'P' && v[1] == 'O')
                { // 버퍼에 있는게 alt + ctrl 일 경우
                    if (input == '{')
                    { // alt + ctrl + tab
                        clear(v, 3);
                        index = 0;
                        return 8;
                    }
                }

                if (v[0] == '>' && v[1] == 'O')
                { // 버퍼에 있는게 window + ctrl 일 경우
                    if (input == 'd')
                    { // window + ctrl + d
                        clear(v, 3);
                        index = 0;
                        return 11;
                    }
                    else if (input == 'L')
                    { // window + ctrl + right
                        clear(v, 3);
                        index = 0;
                        return 13;
                    }
                    else if (input == 'Z')
                    { // window + ctrl + left
                        clear(v, 3);
                        index = 0;
                        return 12;
                    }
                }

                if (v[0] == '>' && v[1] == 'I')
                { // 버퍼에 있는게 window + shift 일 경우
                    if (input == 's')
                    { // window + shift + s
                        clear(v, 3);
                        index = 0;
                        return 27;
                    }
                    else if (input == 'L')
                    { // window + shift + right
                        clear(v, 3);
                        index = 0;
                        return 25;
                    }
                    else if (input == 'Z')
                    { // window + shift + left
                        clear(v, 3);
                        index = 0;
                        return 26;
                    }
                }

                if (v[0] == 'I' && v[1] == 'O')
                { // 버퍼에 있는게 shift + ctrl 일 경우
                    if (input == 'D')
                    { // shift + ctrl + ESC
                        clear(v, 3);
                        index = 0;
                        return 31;
                    }
                    else if (input == '{')
                    { // shift + ctrl + tab
                        clear(v, 3);
                        index = 0;
                        return 32;
                    }
                }

                if (v[0] == 'I' && v[1] == '>')
                { // 버퍼에 있는게 shift + window 일 경우
                    if (input == 'Z')
                    { // shift + window + right
                        clear(v, 3);
                        index = 0;
                        return 33;
                    }
                    else if (input == 'L')
                    { // shift + window + left
                        clear(v, 3);
                        index = 0;
                        return 34;
                    }
                    else if (input == 's')
                    { // shift + window + s
                        clear(v, 3);
                        index = 0;
                        return 35;
                    }
                }
                //cout << "clear" << endl;
                clear(v, 3);
                index = 0; //그냥 hotkey 조합이 아닐 경우 버퍼 초기화

            }
            //cout << "size : ";
            //cout << v.size() << endl << endl;
            return 999;
        }

    }
}
