StreamReader x;

string path = "./main.bf";
x = File.OpenText(path);
string? line = x.ReadLine();

var memory = new Memory();

Console.Clear();
Console.WriteLine("-----BF START-----");
Console.WriteLine();

memory.Execute(line);

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("------BF END------");
Console.WriteLine();
Console.WriteLine("|DEBUG INFO|");
Console.WriteLine();
Console.WriteLine($"BF steps: {memory.commands}");

Console.Write("Final memory: (");
foreach(byte key in memory.memo) {
    Console.Write($" [ {key} ] ");
};
Console.WriteLine(")");

Console.WriteLine($"Final pointer key: {memory.acKey} (value: {memory.memo[memory.acKey]})");

x.Close();
Console.ReadKey(true);