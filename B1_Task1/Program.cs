using B1_Task1;

string path = Path.Combine(Directory.GetCurrentDirectory(), "resultFolder");

var fileService = new FileService(path);

var dbService = new DBService(path);

var complete = "Команда выполнена";

Console.WriteLine("Меню опций:");
Console.WriteLine("Введите 1, чтобы создать новые 100 файлов");
Console.WriteLine("Введите 2, чтобы объединить файлы в один");
Console.WriteLine("Введите 3, чтобы перенести данные с общего файла в таблицу SQL");
Console.WriteLine("Введите 4, чтобы вывести на экран сумму целых чисел и медиану дробных чисел");
Console.WriteLine("Введите 0, чтобы выйти");

string s;

while (true)
{
    s = Console.ReadLine();
    
    switch (s)
    {
        case "1":
            fileService.GenerateFiles(100, 100000);

            Console.WriteLine(complete);
            break;
        case "2":
            Console.WriteLine("Введите подстроку, которую необходимо удалить");

            var subs = Console.ReadLine();

            Console.WriteLine("Удалено строк " + fileService.MergeFiles(subs));
            Console.WriteLine(complete);
            break;
        case "3":
            dbService.ImportData();

            Console.WriteLine(complete);
            break;
        case "4":
            dbService.ExecuteProcedure();

            Console.WriteLine(complete);
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Введите команду");
            break;
    }
}
