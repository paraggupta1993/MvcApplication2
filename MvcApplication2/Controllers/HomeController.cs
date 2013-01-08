using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace MvcApplication2.Controllers
{
      public class S3Client
    {
        static string keyName;
        static AmazonS3 client;
        public S3Client() 
        {
            client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USWest2);
        }
        public ListBucketsResponse ListingBuckets() 
        {
            
            try
            {
                ListBucketsResponse response = client.ListBuckets();
                return response;  
                
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
                return new ListBucketsResponse();
            }
        }

    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        public ActionResult Mybuckets()
        {
            S3Client songsLib = new S3Client();
            ViewBag.buckets = songsLib.ListingBuckets().Buckets;  
        
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
