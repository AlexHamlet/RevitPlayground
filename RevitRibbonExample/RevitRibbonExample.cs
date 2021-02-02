using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RevitRibbonExample
{
    /// <summary>
    /// This is an example class used to demonstrate creating a custom Ribbon Tab/Panel/Button
    /// The button created in this example calls "ExampleCommand"
    /// </summary>
    class RevitRibbonExample : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                //Create a Ribbon tab with this command
                application.CreateRibbonTab("Example Tab");

                //Create a RibbonPanel and store it in a variable to use later
                RibbonPanel ribbonpanel1 = application.CreateRibbonPanel("Example Tab", "Example Panel");

                //Add a button to run our example command.  The localpath command gets the executing directory for the assembly.  The class name is in the form "NAMESPACE.CLASSNAME"
                PushButtonData examplecommandbutton = new PushButtonData("ExampleCommand", "Display Text", localpath("RevitRibbonExample.dll"), "RevitRibbonExample.ExampleCommand");
                //Use LargeImage for the Button on the ribbon (32px x 32px).  In the form "NAMESPACE.RESOURCES(folder).IMAGE"
                examplecommandbutton.LargeImage = PngImageSource("RevitRibbonExample.Resources.Example32.png");
                //Use Image for quick access toolbar (16px x 16px)
                examplecommandbutton.Image = PngImageSource("RevitRibbonExample.Resources.Example16.png");
                examplecommandbutton.ToolTip = "A tooltip for our example command";

                //Remember to add your button to you panel
                ribbonpanel1.AddItem(examplecommandbutton);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Something went wrong.", ex.Message);
                return Result.Failed;
            }
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            /*
             * Unregister all updaters here.
             * Because this example doesn't use any updaters,
             * we fill this method with return Result.Succeeded;
             */
            return Result.Succeeded;
        }

        /// <summary>
        /// Get an embedded resource file from the dll.
        /// </summary>
        /// <param name="embeddedPath"></param>
        /// <returns>An imagesource corresponding to the given path</returns>
        private ImageSource PngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            return decoder.Frames[0];
        }

        /// <summary>
        /// Get a dll relative to this one.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string localpath(string file)
        {
            return string.Format("{0}\\{1}", AssemblyDirectory, file);
        }

        /// <summary>
        /// Get the assembly directory for the addin.  Useful for finding dlls relative to this one.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

    }
}
