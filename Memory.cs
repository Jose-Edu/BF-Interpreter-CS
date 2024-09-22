using System.Text;

public class Memory {

    public List<Byte> memo;
    public int acKey = 0;
    private string repeatBlock = "";
    public int commands = 0;
    private bool inLoop = false;

    public Memory() {

        memo.Add(0);

    }

    public void Add() {

        if (repeatBlock != "" || inLoop) {

            if (memo[acKey] != 255) {
                memo[acKey]++;
            }
            else {
                memo[acKey] = 0;
            };

            commands++;
        }
        else {

            repeatBlock += "+";

        };

    }

    public void Remove() {

        if (repeatBlock != "" || inLoop) {

            if (memo[acKey] != 0) {
                memo[acKey]--;
            }
            else {
                memo[acKey] = 255;
            };

            commands++;
        }
        else {

            repeatBlock += "-";

        };

    }

    public void MoveBack() {

        if (repeatBlock != "" || inLoop) {
            if (acKey != 0) {

                acKey--;

            };

            commands++;
        }
        else {

            repeatBlock += "<";

        };

    }

    public void MoveFront() {

        if (repeatBlock != "" || inLoop) {
            if (acKey == memo.Count()-1) {

                memo.Add(0);

            }

            acKey++;
            commands++;
        }
        else {

            repeatBlock += ">";

        };

    }

    public void OpenRepeat() {

        repeatBlock += "[";
        commands++;

    }

    public void CloseRepeat() {

        while (memo[acKey] != 0) {
            
            commands++;
            Execute(repeatBlock.Substring(repeatBlock.LastIndexOf('[')));

        };

        repeatBlock = repeatBlock.Remove(repeatBlock.LastIndexOf('['));

    }

    public void Write() {

        if (repeatBlock != "" || inLoop) {
            byte[] by = [memo[acKey]];
            Console.Write(
                Encoding.ASCII.GetChars(by)[0]
            );
            commands++;
        }
        else {

            repeatBlock += ".";

        };

    }

    public void Read() {

        if (repeatBlock != "" || inLoop) {

            char c = Convert.ToChar(Console.ReadKey(false).Key.ToString());
            char[] _c = [c];

            memo[acKey] = Encoding.ASCII.GetBytes(_c)[0];
            commands++;
        }
        else {

            repeatBlock += ",";

        };

    }

    public void Execute(string code) {

        foreach (var i in code) {

            if (",.+-<>[]".Contains(i)) {

                switch(i) {

                    case '+':
                        Add();
                        break;
                        
                    case '-':
                        Remove();
                        break;

                    case '<':
                        MoveBack();
                        break;

                    case '>':
                        MoveFront();
                        break;
                        
                    case '.':
                        Write();
                        break;
                        
                    case ',':
                        Read();
                        break;
                        
                    case '[':
                        OpenRepeat();
                        break;
                            
                    case ']':
                        CloseRepeat();
                        break;

                };

            };
            
        };

    }

};