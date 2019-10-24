using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public abstract class App
    {
        //the main method that must be implemented
        protected abstract void DoWhatYouGottaDo();

        //properties and methods to override
        protected virtual void WriteAppTitle(){ Console.WriteLine("(Insert App Title)"); }
        protected virtual string ReadPrefix {get { return "[UnknownApp]->"; }}
        protected virtual ConsoleColor BackGroundColor {get { return ConsoleColor.Black; }}
        protected virtual ConsoleColor TextColor{get { return ConsoleColor.White; }}

        //public method called from outside
        public virtual bool Run()
        {
            ResetScreen();

            try
            {
                DoWhatYouGottaDo();
                return true;
            }
            catch (CloseAppException)
            {
                return true;
            }
            catch (CrashAppException)
            {
                return false;
            }
        }

        //helping methods (usually no need to override)
        protected ConsoleKeyInfo ReadKey()
        {
            Console.Write(ReadPrefix);
            return Console.ReadKey();
        }
        
        protected virtual void ResetScreen()
        {
            Console.BackgroundColor = BackGroundColor;
            Console.Clear();
            Console.ForegroundColor = TextColor;
            WriteAppTitle();
        }

        protected void CloseApp()
        {
            throw new CloseAppException();
        }

        protected void CrashApp()
        {
            throw new CrashAppException();
        }

    }

    public class CloseAppException : Exception
    {
    }

    public class CrashAppException : Exception
    {
    }
}
