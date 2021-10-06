using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFCommandPrompt;

namespace TerminalTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WPFPrompt prompt = new WPFPrompt();

        List<string> strings = new List<string>() {"This shit's bussin", "wow very cool\nI wish I knew ho to do that.", "I hope\nthis method works the way I think it should", " \nleedle"};
        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            
            prompt.Prompt = ">";
            prompt.ReadLine += Prompt_ReadLine;
            prompt.WelcomeMessage = "";
            prompt.Show();

            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.R)
            {
                Dispatcher.BeginInvoke((Action)(async () =>
                {
                    for(int i = 0; i < 100; i++)
                    {
                        prompt.WriteLine("sheesh");
                        await Task.Delay(10);
                        prompt.ClearConsole();
                        await Task.Delay(3);
                    }
                }));
            }
        }

        private void Prompt_ReadLine(object sender, ConsoleReadLineEventArgs e)
        {
            var string1 = strings.ElementAt(rand.Next(0, strings.Count));
            var string2 = strings.ElementAt(rand.Next(0, strings.Count));

            prompt.WriteLine("string #1:\n"+string1+"\n");
            prompt.WriteLine("string #2:\n" + string2 + "\n");
            prompt.WriteLine("string #2 overlayed on top of string #1:\n" + overlayString(string2, string1) + "\n");
        }


        private string overlayString(string newString, string oldString)
        {
            var newStringSplit = newString.Split("\n");
            var oldStringSplit = oldString.Split("\n");

            int maxLines = Math.Max(newStringSplit.Length, oldStringSplit.Length);

            string[] overlayedString = new string[maxLines];

            for(int i = 0; i < maxLines; i++)
            {
                if(i < newStringSplit.Length && i < oldStringSplit.Length)
                {
                    if(newStringSplit[i].Length > oldStringSplit[i].Length)
                    {
                        overlayedString[i] = newStringSplit[i];
                    }
                    else
                    {
                        overlayedString[i] = newStringSplit[i] + oldStringSplit[i].Substring(newStringSplit[i].Length);
                    }
                }
                else if(i < newStringSplit.Length)
                {
                    overlayedString[i] = newStringSplit[i];
                }
                else
                {
                    overlayedString[i] = oldStringSplit[i];
                }
            }

            return String.Join("\n", overlayedString.ToArray());
        }
    }

}
