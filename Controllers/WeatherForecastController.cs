using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Code7248.word_reader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Net;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get(string path)
        {
            string Content;
            using (WebClient myWebClient = new WebClient())
            {
                // Download the Web resource and save it into a data buffer.
                byte[] bytes = myWebClient.DownloadData(path);
                MemoryStream memoryStream = new MemoryStream(bytes);
                
                // Open a WordprocessingDocument for read-only access based on a stream.
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(memoryStream, false))
                {
                    MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                    Content = mainPart.Document.Body.InnerText;
                }
            }
            return Content;
        }
    }
}
