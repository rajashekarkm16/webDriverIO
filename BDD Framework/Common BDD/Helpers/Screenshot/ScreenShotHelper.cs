using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using ReportPortal.Shared;
using ReportPortal.Shared.Execution.Logging;
using TechTalk.SpecFlow;

namespace Dnata.Automation.BDDFramework.Helpers.Screenshot
{
    public  class ScreenShotHelper : IScreenShotHelper
    {
        ScenarioContext _scenarioContext;
        public ScreenShotHelper(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public string TakeScreenshot(IWebDriver driver, string outputDirectory, string filePath,bool isFullScreenShot = false)
        {
            if (isFullScreenShot)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");

                Bitmap stitchedImage = null;
                try
                {
                    long totalwidth1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.offsetWidth");//documentElement.scrollWidth");

                    long totalHeight1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return  document.body.parentNode.scrollHeight");

                    int totalWidth = (int)totalwidth1;
                    int totalHeight = (int)totalHeight1;

                    // Get the Size of the Viewport
                    long viewportWidth1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.clientWidth");//documentElement.scrollWidth");
                    long viewportHeight1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return window.innerHeight");//documentElement.scrollWidth");

                    int viewportWidth = (int)viewportWidth1;
                    int viewportHeight = (int)viewportHeight1;


                    // Split the Screen in multiple Rectangles
                    List<Rectangle> rectangles = new List<Rectangle>();
                    // Loop until the Total Height is reached
                    for (int i = 0; i < totalHeight; i += viewportHeight)
                    {
                        int newHeight = viewportHeight;
                        // Fix if the Height of the Element is too big
                        if (i + viewportHeight > totalHeight)
                        {
                            newHeight = totalHeight - i;
                        }
                        // Loop until the Total Width is reached
                        for (int ii = 0; ii < totalWidth; ii += viewportWidth)
                        {
                            int newWidth = viewportWidth;
                            // Fix if the Width of the Element is too big
                            if (ii + viewportWidth > totalWidth)
                            {
                                newWidth = totalWidth - ii;
                            }

                            // Create and add the Rectangle
                            Rectangle currRect = new Rectangle(ii, i, newWidth, newHeight);
                            rectangles.Add(currRect);
                        }
                    }

                    // Build the Image
                    stitchedImage = new Bitmap(totalWidth, totalHeight);
                    // Get all Screenshots and stitch them together
                    Rectangle previous = Rectangle.Empty;
                    foreach (var rectangle in rectangles)
                    {
                        // Calculate the Scrolling (if needed)
                        if (previous != Rectangle.Empty)
                        {
                            int xDiff = rectangle.Right - previous.Right;
                            int yDiff = rectangle.Bottom - previous.Bottom;
                            // Scroll
                            //selenium.RunScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                            ((IJavaScriptExecutor)driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                            System.Threading.Thread.Sleep(200);
                        }

                        // Take Screenshot
                        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                        // Build an Image out of the Screenshot
                        System.Drawing.Image screenshotImage;
                        using (MemoryStream memStream = new MemoryStream(screenshot.AsByteArray))
                        {
                            screenshotImage = System.Drawing.Image.FromStream(memStream);
                        }

                        // Calculate the Source Rectangle
                        Rectangle sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);

                        // Copy the Image
                        using (Graphics g = Graphics.FromImage(stitchedImage))
                        {
                            g.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                        }

                        // Set the Previous Rectangle
                        previous = rectangle;

                    }
                }
                catch (Exception ex)
                {
                    // handle
                }

                if (!Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);
                stitchedImage.Save(filePath, ImageFormat.Png);

                ILogMessage mess = new LogMessage(_scenarioContext.ScenarioInfo.Title + " Screenshot")
                {
                    Attachment = new LogMessageAttachment("image/png", File.ReadAllBytes(filePath))
                };
                Context.Launch.Log.Message(mess);
                return $"Screenshot: {filePath}";
            }
            else
            {
                var takesScreenshot = driver as ITakesScreenshot;
                try
                {
                    if (takesScreenshot == null)
                        return "No screenshot taken";

                    var screenshot = takesScreenshot.GetScreenshot();
                    if (!Directory.Exists(outputDirectory))
                        Directory.CreateDirectory(outputDirectory);
                    screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                    
                    ILogMessage mess = new LogMessage(_scenarioContext.ScenarioInfo.Title + " Screenshot")
                    {
                        Attachment = new LogMessageAttachment("image/png", File.ReadAllBytes(filePath))
                    };
                    Context.Launch.Log.Message(mess);

                    return $"Screenshot: {filePath}";
                }
                catch (Exception)
                {
                    return "No screenshot taken (Screenshot Exception Thrown!)";
                }
            }

        }

    }
}