using AwsS3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using AwsS3.Models;
using System.Diagnostics;
using Amazon.S3;
using Amazon.Runtime;
using Amazon;
using Amazon.S3.Model;
using System.Threading.Tasks;
using Amazon.S3.Transfer;

namespace AwsS3.Controllers
{
    public class HomeController : Controller
    {
        private const string V = "C://Users//Adil//source//repos//AwsS3//AwsS3//wwwroot//upload//";
        private readonly IAmazonS3 amazonS3;
        public HomeController (IAmazonS3 amazonS3)
        {
            this.amazonS3 = amazonS3;
        }


        [HttpPost]
        public async Task<IActionResult> SingleFile([FromForm] IFormFile file)
        {
            string AccessKeyId = "AccessKeyId";
            string SecretAccessKey = "SecretAccessKey";
            string AWS_bucketName = "testadil";
            BasicAWSCredentials awsCreds = new BasicAWSCredentials(AccessKeyId, SecretAccessKey);

            AmazonS3Config S3Config = new AmazonS3Config  // s3://testadil1405/testit/
            {
                ServiceURL = "http://testadil1405.s3-website-USEast1.amazonaws.com",
                RegionEndpoint = RegionEndpoint.USEast1,
                ForcePathStyle = true,
            };
            AmazonS3Client s3Client = new AmazonS3Client(awsCreds, S3Config);
          
            /*await using var newMemoryStream = new MemoryStream();
            file.CopyTo(newMemoryStream);*/

            var uploadRequest = new PutObjectRequest    //TransferUtilityUploadRequest
            {
                //InputStream = newMemoryStream,
                InputStream = file.OpenReadStream(),
                Key = file.FileName,
                BucketName = AWS_bucketName,
                CannedACL = S3CannedACL.PublicRead,
                ContentType = file.ContentType,
            };

            //var fileTransferUtility = new TransferUtility(s3Client);
            //await fileTransferUtility.UploadAsync(uploadRequest);

            String temp_ = "GG";
            var result = await s3Client.PutObjectAsync(uploadRequest);
            return Ok(result);



            //    /*var dir = V;
            //    string filename = Path.GetFileName(file.FileName);
            //    using (var fileStream = new FileStream(Path.Combine(dir, filename), FileMode.Create, FileAccess.Write))
            //    {
            //        file.CopyTo(fileStream);
            //    }

            //    return RedirectToAction("Index");*/
            //}
        }

        private readonly ILogger<HomeController> _logger;

        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
