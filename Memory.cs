using System.Text;

public class Memory {

    public List<Byte> memo = [0];
    public int acKey = 0;
    private string repeatBlock = "";
    public int commands = 0;
    private bool inLoop = false;

    public void Add() {

        if (repeatBlock == "" || inLoop) {

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

        if (repeatBlock == "" || inLoop) {

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

        if (repeatBlock == "" || inLoop) {
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

        if (repeatBlock == "" || inLoop) {
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
            inLoop = true;
            Execute(repeatBlock.Substring(repeatBlock.LastIndexOf('[')+1));

        };  

        inLoop = false;
        repeatBlock = repeatBlock.Remove(repeatBlock.LastIndexOf('['));

    }

    public void Write() {

        if (repeatBlock == "" || inLoop) {
            if (memo[acKey] == 13) {
            
                Console.WriteLine();

            }
            else {
                byte[] by = [memo[acKey]];
                Console.Write(
                    Encoding.ASCII.GetChars(by)[0]
                );
            };
            commands++;
        }
        else {

            repeatBlock += ".";

        };

    }

    public void Read() {

        if (repeatBlock == "" || inLoop) {

            char key = Console.ReadKey(true).KeyChar;

            memo[acKey] = (byte) key;
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