using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using backend.business.AttachmentsController.Models;
using backend.business.Infrastructure;
using MediatR;

namespace backend.business.AttachmentsController.Queries.GetDetail
{
    public class GetAttachmentsDetailQueryHandler : IRequestHandler<GetAttachmentsDetailQuery, AttachmentDetailModel>
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly ApplicationSettings _settings;

        public GetAttachmentsDetailQueryHandler(IAmazonS3 amazonS3, ApplicationSettings settings)
        {
            _amazonS3 = amazonS3;
            _settings = settings;
        }
        public async Task<AttachmentDetailModel> Handle(GetAttachmentsDetailQuery request, CancellationToken cancellationToken)
        {
            var key = $"{request.EventId}/{request.ParticipantId}/attachment.jpg";
            bool imageExists;
            try
            {
                await _amazonS3.GetObjectAsync(new GetObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key
                }, cancellationToken);
                imageExists = true;
            }
            catch
            {
                imageExists = false;
            }


            return new AttachmentDetailModel
            {
                
                UploadUrl = _amazonS3.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key,
                    Verb = HttpVerb.PUT,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    
                }),
                Exists = imageExists,
                UploadUrlValidTill = DateTime.UtcNow.AddMinutes(15),
                DownloadUrl = _amazonS3.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key,
                    Expires = DateTime.UtcNow.AddDays(1),
                }),
                DownloadUrlValidTill = DateTime.UtcNow.AddDays(1)
            };
        }
    }
}