
using System.Diagnostics;
using System.Drawing;
using System.IO;


namespace CS_Lesson_14;


// ScreenShot kodunu https://social.technet.microsoft.com/wiki/contents/articles/32012.saving-a-screenshot-using-c.aspx buradan goturdum
// Orda System.Drawing den Bitmap gelirdi. Amma diyesen yenilemeynen Bitmap Drawing de yox System.Drawing.Common dadi
// Ona gore onu Nugetden yukledim



class Program
{
    #region Helper_Func

    public static void OpenWithDefaultProgram(string path)
    {
        using Process fileopener = new Process();
        fileopener.StartInfo.FileName = "explorer";
        fileopener.StartInfo.Arguments = "\"" + path + "\"";
        fileopener.Start();
    }

    #endregion

    static void Main()
    {

        while (true)
        {

            Console.Clear();
            Console.Write($@"
1. Take Screenshot
2. See Screenshots
3. Open With Default Program
4. Delete with file name
5. Delete all Screenshots
0. Exit

Enter: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    if (!Directory.Exists(@$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots"))
                        Directory.CreateDirectory(@$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots");

                    Console.WriteLine("Starting the process...");
                    Console.WriteLine();
                    Bitmap memoryImage;
                    memoryImage = new Bitmap(1920, 1080);
                    Size s = new Size(memoryImage.Width, memoryImage.Height);

                    Graphics memoryGraphics = Graphics.FromImage(memoryImage);

                    memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
                    memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

                    //That's it! Save the image in the directory and this will work like charm.
                    string fileName = string.Format($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots" +
                              @"\Screenshot" + "_" +
                              DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png");

                    // save it
                    memoryImage.Save(fileName);

                    // Write the message,
                    Console.WriteLine("Picture has been saved...");
                    // Pause the program to show the message.
                    Console.ReadKey(false);

                    continue;
                case "2":
                    Console.Clear();
                    DirectoryInfo directoryInfo = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots");
                    short i = 1;

                    foreach (FileInfo file in directoryInfo.GetFiles())
                        Console.WriteLine($"{i++}. {file.Name}");


                    Console.Write("\nPress Any key for come back to menu ...");
                    Console.ReadKey(false);
                    continue;
                case "3":

                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Enter file name(Press 0 if you want come back to menu): ");
                        string path = Console.ReadLine();

                        if (path == "0") break;

                        try
                        {
                            if (string.IsNullOrEmpty(path))
                                throw new ArgumentNullException("File name");

                            if (File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots\{path}") is false)
                                throw new FileNotFoundException();

                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);

                            Thread.Sleep(1500);
                            continue;
                        }

                        OpenWithDefaultProgram($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots\{path}");

                    }
                    continue;
                case "4":

                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Enter file name(Press 0 if you want come back to menu): ");
                        string path = Console.ReadLine();

                        if (path == "0") break;

                        try
                        {
                            if (string.IsNullOrEmpty(path))
                                throw new ArgumentNullException("File name");

                            if (File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots\{path}") is false)
                                throw new FileNotFoundException();

                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);

                            Thread.Sleep(1500);
                            continue;
                        }

                        File.Delete($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots\{path}");
                        Console.WriteLine("Screenshot has been deleted...");
                        Console.ReadKey(false);
                    }
                    continue;

                case "5":
                    Console.Clear();
                    directoryInfo = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ScreenShots");
                    foreach (FileInfo file in directoryInfo.GetFiles())
                        File.Delete(file.FullName);

                    Console.WriteLine("All Screenshots has been deleted...");
                    Console.ReadKey(false);
                    continue;

                case "0":
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\t\t\t\t\t GOOD BYE!");
                    Console.ReadKey(false);
                    Environment.Exit(0);
                    break;
                default:
                    try
                    {
                        throw new ArgumentException("\n\n\n\n\t\t\t\tUnknown command !");
                    }
                    catch (Exception ex)
                    {

                        Console.Clear();
                        Console.WriteLine(ex.Message);

                        Thread.Sleep(1500);
                        continue;
                    }
            }
        }
    }
}
