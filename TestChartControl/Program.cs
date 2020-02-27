using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestChartControl
{
    static class Program {


    static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)

    {

      return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);

    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
        static void Main()
        {
      //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve     = new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

      //string file = @"D:\Projects\Programming\WinformsControls\ChartControl\ChartControl\bin\Release\ChartControl.dll";

      //Assembly assemblyHardCoded = Assembly.ReflectionOnlyLoadFrom(file);
      //Console.WriteLine("\nGet Defined types for "      assemblyHardCoded.FullName);
      //foreach (var t in assemblyHardCoded.ExportedTypes) 
      //Console.WriteLine($"name {t.Name} namespace {t.Namespace} ");

      //Console.ReadKey();

      ////Console.WriteLine("\nGet Exported types");
      ////foreach (var expType in a.GetExportedTypes())
      ////    Console.WriteLine($"name {expType.Name} namespace {expType.Namespace} ");

   
      //Assembly reference;
      //Console.WriteLine("\nGet Exported types in references assemblies");
      //foreach (var refAss in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
      //  {
      //  try
      //  {
      //    Console.WriteLine("************References assemblies "      refAss      " total types "      Assembly.ReflectionOnlyLoad(refAss.FullName).GetExportedTypes().Count());

      //    reference = Assembly.ReflectionOnlyLoad(refAss.FullName);
      //    foreach (var expType in reference.ExportedTypes)
      //    {
      //      //Console.WriteLine($"name {expType.Name} namespace {expType.Namespace} ");

      //      if (expType.IsPublic == false)
      //      {
      //        //Console.WriteLine("This isnt public ");
      //        //Console.ReadKey();
      //      }

      //    }
      //  }
      //  catch (Exception e)
      //  {
      //    Console.WriteLine("This couldnt be loaded "      refAss.Name);
      //    //Console.ReadKey();
      //  }

      //  }
      //  ;

        Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
