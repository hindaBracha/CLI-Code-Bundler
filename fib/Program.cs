using System.CommandLine;
using System.Text;
using System.Text.RegularExpressions;

#region ----property---

string[] codeFileExtensions = { ".cs", ".cln", ".cpp", ".css", ".html", ".java", ".js", ".jsx", ".py", ".sql", ".ts" };
string[] ending = { ".vscode", ".metadata", "public", "node_modules", "obj", "bin", ".vs", ".settings", "Debug", "assets", "build", "plugins" };
string[] Mylanguage;
var bundleOption = new Option<FileInfo>("--output", "File path and name");
var bundleNote = new Option<string>("--note", "if to show URL");
var bundleAuthor = new Option<string>("--author", "File path and name");
var bundleLanguage = new Option<string>("--language", "The names of the desired languages");
var bundleSort = new Option<string>("--sort", "The sort the File");
var bundleEmptyRow = new Option<string>("--emptyrow", "The sort the File");
var bundleCommand = new Command("bundle", "Bundle code files to a single file");
var createCommand = new Command("creatersp", "Create a response file");
var rootCommand = new RootCommand("Root command for File Bundler CLI");

#endregion

#region ----AddAlias && AddOption-----

bundleOption.AddAlias("-o");
bundleNote.AddAlias("-n");
bundleAuthor.AddAlias("-a");
bundleLanguage.AddAlias("-l");
bundleSort.AddAlias("-s");
bundleEmptyRow.AddAlias("-e");

bundleCommand.AddOption(bundleOption);
bundleCommand.AddOption(bundleEmptyRow);
bundleCommand.AddOption(bundleLanguage);
bundleCommand.AddOption(bundleSort);
bundleCommand.AddOption(bundleAuthor);
bundleCommand.AddOption(bundleNote);

#endregion

#region ----SetHandler-

bundleCommand.SetHandler(async (output, language, sort, author, note, emptyrow) =>
{

    StringBuilder codeBuilder = new StringBuilder();
    string SubOutput = output.FullName.Substring(0, output.FullName.LastIndexOf("\\"));
    string[] files;
    string Code = "";

    if (author != "")
        Code += "~~~~~~~~~~💐~~~~ Hi, the name of the authot : " + author + "   ©️ ~~~~💐~~~~~~~~~~";
    codeBuilder.AppendLine(Code);
    if (language == "all")
        // if language equal all or language_specific
        Mylanguage = codeFileExtensions;
    else Mylanguage = language.Split(" ")
     .Where(MyLanguage => codeFileExtensions.Contains(MyLanguage))
     .ToArray();

    if (sort == "extensions")
        // sort by extensions
        files = Directory.GetFiles(SubOutput, "*", SearchOption.AllDirectories)
           .Where(file => Mylanguage.Contains(Path.GetExtension(file))
           && !ending.Contains((file)))
           .OrderBy(file => Path.GetExtension(file))
           .ToArray();
    else
        // sort by FileName
        files = Directory.GetFiles(SubOutput, "*", SearchOption.AllDirectories)
         .Where(file => Mylanguage.Contains(Path.GetExtension(file))
           && !ending.Contains((file)))
         .OrderBy(file => Path.GetFileNameWithoutExtension(file))
         .ToArray();

    foreach (string file in files)
    //scan the file in order to Read Text and copy this
    {
        string code = "";
        code += "-------------<🏷️>--------" + file.Substring(file.LastIndexOf("\\") + 1) + "---------------------\n";
        if (note == "y")
            code += "----------📝->📩->📬----------" + file + "---------------------\n";
        code += File.ReadAllText(file);
        if (emptyrow == "y")
            //Remove empty lines
            code = Regex.Replace(code, @"^\s+$[\r\n]*", @" ", RegexOptions.Multiline);
        codeBuilder.AppendLine(code);
    }
    try
    {
        File.WriteAllText(output.FullName, codeBuilder.ToString());
        Console.WriteLine("Code bundle created successfully!");
    }
    catch
    {
        Console.WriteLine("Code bundle not created successfully!");
    }

}, bundleOption, bundleLanguage, bundleSort, bundleAuthor, bundleNote, bundleEmptyRow);

createCommand.SetHandler(() =>
{
    string resule = "bundle ";
    var temp = "";
    Console.WriteLine("What your output ? , and then press Enter");
    temp = Console.ReadLine();
    if (temp != null)
        resule += " -o " + temp;
    Console.WriteLine("What your languages Please detail , if your want all languages write all , and then press Enter");
    temp = Console.ReadLine();
    if (temp != null) resule += " -l " + temp;
    Console.WriteLine("If your sort by extensions Write extensions else detail  , and then press Enter");
    temp = Console.ReadLine();
    if (temp != null) resule += " -s " + temp;
    Console.WriteLine("If you don't like blank lines ? (y/n) , and then press Enter");
    temp = Console.ReadLine();
    if (temp != null) resule += " -e " + temp;
    Console.WriteLine("You love note (print the Path) ? (y/n) , and then press Enter");
    temp = Console.ReadLine();
    if (temp != null) resule += " -n " + temp;
    Console.WriteLine("Who is this crazy creator ?  , and then press Enter");
    temp = Console.ReadLine();
    if (temp != null) resule += " -a " + temp;
    try
    {
        File.WriteAllText("creatersp.rsp", resule);
        Console.WriteLine("Code createRsp created successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Code createRsp not created successfully !_! ");
    }

});

#endregion

rootCommand.AddCommand(bundleCommand);
rootCommand.AddCommand(createCommand);
rootCommand.InvokeAsync(args);