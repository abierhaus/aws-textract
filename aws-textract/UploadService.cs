using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace aws_textract
{
    public interface IUploadService
    {
        /// <summary>
        ///     Upload file to S3 bucket
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="keyName"></param>
        /// <param name="bucketName"></param>
        /// <param name="bucketRegion"></param>
        /// <returns></returns>
        Task Upload(string filePath, string keyName, string bucketName, RegionEndpoint bucketRegion);
    }

    public class UploadService : IUploadService
    {

        /// <summary>
        ///     Upload file to S3 bucket
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="keyName"></param>
        /// <param name="bucketName"></param>
        /// <param name="bucketRegion"></param>
        /// <returns></returns>
        public  async Task Upload(string filePath, string keyName, string bucketName, RegionEndpoint bucketRegion)
        {
            var s3Client = new AmazonS3Client(bucketRegion);

            using var fileTransferUtility = new TransferUtility(s3Client);
            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                BucketName = bucketName,
                FilePath = filePath,
                StorageClass = S3StorageClass.StandardInfrequentAccess,
                PartSize = 6291456, // 6 MB.
                Key = keyName,
                CannedACL = S3CannedACL.PublicRead
            };

            //Provide additional meta data
            fileTransferUtilityRequest.Metadata.Add("Source", "aws-textract-demo");

            await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        }
    }
}