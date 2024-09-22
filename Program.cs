StreamReader x;

string path = "./main.bf";
x = File.OpenText(path);
string? line = x.ReadLine();

var memory = new Memory();

memory.Execute(line);

x.Close();
Console.ReadKey();