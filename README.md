# AWS Textract Example

This console application demonstrate how to use the AWS Textract service in C# (.NET Core). I could not find any simple and clean working application, so I wrote it myself.

The application comes with two end-to-end examples that demonstrate how to use AWS Textract. In this example, the documents are uploaded to an S3 bucket, the Textract service is called and waits until the service returns the result. 

Just download, set your S3 Bucketname, and run it.


## Prerequisites

1. Make sure you setup a S3 bucket
2. Make sure you configured your IAM User and give it proper access right (TranslateFullAccess)
2. Run the sample


![image](https://user-images.githubusercontent.com/18400458/97287073-6e29ad80-1844-11eb-8fa5-cd06c80593a5.png)
