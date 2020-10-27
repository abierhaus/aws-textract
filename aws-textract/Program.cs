using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Textract;

namespace aws_textract
{
    internal class Program
    {
        private const string BucketName = ""; //Set your S3 bucketname
        private static readonly RegionEndpoint Region = RegionEndpoint.EUCentral1; //Set your region

        private static async Task Main(string[] args)
        {
            //Set Logging 
            AWSConfigs.LoggingConfig.LogTo = LoggingOptions.Console;
            AWSConfigs.LoggingConfig.LogMetrics = false;
            AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.OnError;


            //Standard AWS example
            Console.WriteLine("Extracting Example 1");
            var example1 = await GetDocumentText("Files/Amazon-Textract-Pdf.pdf");
            Console.WriteLine(example1);


            //Old bookpage
            Console.WriteLine("Extracting Example 2");
            var example2 = await GetDocumentText("Files/TheLifeAndWorkOfFredsonBrowers.jpg");
            Console.WriteLine(example2);
        }

        /// <summary>
        ///     Get document text for provided file
        ///     1. Upload file to S3 bucket
        ///     2. Start Document Analyzation
        ///     3. Wait for AWS to complete analyzation
        ///     4. Return text word by word
        /// </summary>
        /// <param name="file">file (path) that you like to analyze</param>
        /// <returns></returns>
        private static async Task<string> GetDocumentText(string file)
        {
            var keyName = Path.GetFileName(file);


            //Upload document
            IUploadService uploadService = new UploadService();
            await uploadService.Upload(file, keyName,
                BucketName, Region);
            
            IDocumentService documentService = new DocumentService(new AmazonTextractClient());
            var blocks = await documentService.GetDocumentBlocks(BucketName, keyName);
            var output = new StringBuilder();

            //See documentation for blocktype reference https://docs.aws.amazon.com/textract/latest/dg/API_Block.html
            foreach (var block in blocks.Where(x => x.BlockType == "WORD")) //Where(x => x.BlockType == "LINE")
                output.Append($"{block.Text} ");

            return output.ToString();
        }
    }
}